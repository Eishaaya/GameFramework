using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseGameLibrary.Inputs;
using BaseGameLibrary.Visual;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGameLibrary.Inputs
{
    public sealed class InputButton<T, TInput> : ButtonBase where TInput : Enum
    {
        [Flags]
        public enum LayoutType
        {
            Restrict = 1,
            ScaleToLabel = 2,
            AutoNewLine = 4,
        }
        //Type type = (typeof)float;
        //public ButtonBase Button { get; set; }
        public LabelParser<T> Label { get; set; }
        bool writing;
        public List<(char letter, int heldTime)> InputCandidates { get; private set; }
        int heldDelay;
        int previousDelayMod;

        Dictionary<TInput, Action<InputButton<T, TInput>>> inputs;

        private InputButton() { }

        public InputButton(Texture2D image, Vector2 location, Vector2 origin, float scale, LabelParser<T> label,
                           Dictionary<TInput, Action<InputButton<T, TInput>>> inputs, int heldDelay = 100)
            : this(image, location, Color.White, 0, SpriteEffects.None, origin, scale, 1, Color.DarkGray, Color.Gray, label, inputs, heldDelay) { }
        public InputButton(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effect, Vector2 origin, float scale, float depth, 
                           Color hovercolor, Color clickedcolor, LabelParser<T> label, Dictionary<TInput, Action<InputButton<T, TInput>>> inputs, int heldDelay = 100)
            :base(image, location, color, rotation, effect, origin, scale, depth, hovercolor, clickedcolor)
        {
            Held = false;
            Label = label;
            writing = false;
            this.inputs = inputs;
            this.heldDelay = heldDelay;
            InputCandidates = new List<(char, int)>(5);
        }
        //FIGURE OUT HOW YOU WANT TO CHECK INPUTS: COMPILE & SEND, OR LOOP & CHECK!
         //Answer: I'm fucking stupid

        ///<summary>
        ///Generates a dictionary to be used for mapping inputs to commands/keys
        ///</summary>

        public static void Ready(InputButton<T, TInput> inputButton)
        {
            inputButton.writing = false;
            inputButton.Label.Parse();
        }
        public static void Cancel(InputButton<T, TInput> inputButton)
        {
            inputButton.writing = false;
            inputButton.Label.Revert();
        }

        public void Add(string text)
        {
            Label.Label.Add(text);
        }

        ///<summary>
        ///Runs a standard button check and begins taking inputs if pressed, and cancels if something else is clicked
        ///</summary>
        public override bool Check(CursorRoot cursor, CursorRoot.ClickStatus threshold = CursorRoot.ClickStatus.Held)
        {
            bool pressed;
            if (base.Check(cursor, out pressed, threshold))
            {
                Label.Label.Text("");
                writing = true;
                return true;
            }
            else if (pressed)
            {
                Cancel(this);
            }
            return false;
        }

        ///<summary>
        ///Checks inputs and writes to the label
        ///</summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var input in inputs)
            {
                if (InputManager<TInput>.Instance[input.Key])
                {
                    input.Value(this);
                }
            }

            int lowestTime = int.MaxValue;
            char charToAdd = '#';
            foreach (var character in InputCandidates)
            {
                if (character.heldTime < lowestTime)
                {
                    charToAdd = character.letter;
                    lowestTime = character.heldTime;
                }
            }

            if (lowestTime != int.MaxValue)
            {
                if (lowestTime == 0)
                {
                    Label.Label.Add(charToAdd);
                    return;
                }
                if (lowestTime / heldDelay > previousDelayMod)
                {
                    Label.Label.Add(charToAdd);
                    previousDelayMod = lowestTime / heldDelay;
                    return;
                }
                previousDelayMod = 1;
            }
        }

        ///<summary>
        ///Draws the label and its button
        ///</summary>
        public override void Draw(SpriteBatch SevenUpBatch)
        {
            base.Draw(SevenUpBatch);
            Label.Draw(SevenUpBatch);
        }
        ///<summary>
        ///Provides a shallow copy of the InputButton
        ///</summary>
        public override InputButton<T, TInput> Clone()
        {
            var copy = new InputButton<T, TInput>();
            CloneLogic(copy);
            return copy;
        }
        public void CloneLogic(InputButton<T, TInput> copy)
        {
            base.CloneLogic(copy);
            copy.Label = Label.Clone();
            copy.writing = writing;
            copy.heldDelay = heldDelay;
            copy.previousDelayMod = previousDelayMod;
            copy.inputs = inputs;            
        }
    }
}
