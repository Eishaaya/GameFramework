using BaseGameLibrary.Inputs;
using BaseGameLibrary.Visual;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGameLibrary.Visual
{
    public class ButtonManager<TScreenum> : IRunnable where TScreenum : Enum
    {
        List<ActionButton> buttons;
        //public bool Running { get; set; }
        // Screen parent;
        public ButtonManager(params ActionButton[] buttons) => this.buttons = new(buttons);
          
        public ButtonManager(IEnumerable<ActionButton> buttons)
        {
           // this.parent = parent;            
            this.buttons = buttons.ToList();
            //Running = true;
        }

        void IRunnable.Update(GameTime gameTime) => Update();
        public void Update()
        {
            //if (!Running) return;

            for (int i = 0; i < buttons.Count; i++)
            {
                var currentButton = buttons[i];

                currentButton.Click(Screenmanager<TScreenum>.Instance.Cursor);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }



    }
}
