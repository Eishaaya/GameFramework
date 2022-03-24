using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseGameLibrary.Inputs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGameLibrary.Visual
{
    class InputButton<T, TInput> : ButtonBase
    {
        //Type type = (typeof)float;
        //public ButtonBase Button { get; set; }
        public LabelParser<T> Label { get; set; }
        bool writing;

        Dictionary<TInput, ParamAction<InputButton<T, TInput>>> inputs;

        public InputButton(ButtonBase button, LabelParser<T> label, Dictionary<TInput, ParamAction<InputButton<T, TInput>>> inputs)
        {
            Held = false;
            Label = label;
            writing = false;
            this.inputs = inputs;
        }//FIGURE OUT HOW YOU WANT TO CHECK INPUTS: COMPILE & SEND, OR LOOP & CHECK!

        public override bool Check(CursorRoot cursor, int threshold = 2)
        {            
            if(base.Check(cursor, threshold))
            {
                Label.Label.Text("");
                writing = true;
                return true;
            }
            return false;
        }
        public override void Draw(SpriteBatch SevenUpBatch)
        {
            base.Draw(SevenUpBatch);
            Label.Draw(SevenUpBatch);
        }

        public override ButtonBase Clone()
        {
            throw new NotImplementedException();
        }
    }
}
