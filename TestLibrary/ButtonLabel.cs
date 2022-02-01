﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGameLibrary
{
    class ButtonLabel
    {
        public bool Clicked { get; set; }
        public Button Button { get; set; }
        public Label Label { get; set; }

        public ButtonLabel(Button button, Label label)
        {
            Clicked = false;
            Button = button;
            Label = label;
        }

        public bool Check(Vector2 cursor, bool isClicked)
        {            
            if (Button.Check(cursor, isClicked))
            {
                Clicked = true;
                Label.Text("");
                return true;
            }
            return false;
        }
        public void Draw(SpriteBatch SevenUpBatch)
        {
            Button.Draw(SevenUpBatch);
            Label.Draw(SevenUpBatch);
        }
    }
}
