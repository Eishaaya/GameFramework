using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public class ButtonManager
    {
        List<ActionButton> buttons;
        Screen parent;

        public ButtonManager(Screen parent, List<ActionButton> buttons)
        {
            this.parent = parent;
            this.buttons = buttons;
        }

        public void Update(Vector2 mousePos, params bool[] clicks)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                var currentButton = buttons[i];

                currentButton.Click(clicks, mousePos);
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
