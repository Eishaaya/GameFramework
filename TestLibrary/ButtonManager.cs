using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    class ButtonManager
    {
        List<IParamAction> actions;
        List<Button> buttons;
        Screen parent;

        public ButtonManager(Screen parent, List<IParamAction> actions, List<Button> buttons)
        {
            this.parent = parent;
            this.actions = actions;
            this.buttons = buttons;
        }

        public void Update(Vector2 mousePos, bool leftDown, bool rightDown)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                var currentButton = buttons[i];

                if ()
            }
        }

        public void Draw()
        {

        }
    }
}
