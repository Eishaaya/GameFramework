using BaseGameLibrary.Inputs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGameLibrary
{
    public class ButtonManager
    {
        List<ActionButton> buttons;
        public bool Running { get; set; }
        // Screen parent;
        public ButtonManager(params ActionButton[] buttons)
            : this(buttons.AsEnumerable()) { }
        public ButtonManager(IEnumerable<ActionButton> buttons)
        {
           // this.parent = parent;
            this.buttons = buttons == null? new List<ActionButton>() : buttons.ToList();
            Running = true;
        }

        public void Update(Vector2 mousePos, bool heldMouse, params Click[] clicks)
        {
            if (!Running) return;

            for (int i = 0; i < buttons.Count; i++)
            {
                var currentButton = buttons[i];

                currentButton.Click(clicks, heldMouse, mousePos);
            }
        }
        public void Update(Vector2 mousePos, bool heldMouse, CursorRoot cursor)
        {
            if (!Running) return;

            for (int i = 0; i < buttons.Count; i++)
            {
                var currentButton = buttons[i];

                currentButton.Click(cursor);
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
