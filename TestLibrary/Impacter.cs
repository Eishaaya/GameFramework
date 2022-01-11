using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    class Impacter
    {
        Impact effect;
        Button toggle;
        Keys hotKey;

        public void check (KeyboardState ks, MouseState ms)
        {
            effect = ks.IsKeyDown(hotKey) || toggle.Check(new Vector2(ms.X, ms.Y), ms.LeftButton == ButtonState.Pressed);
        }
        public static implicit operator Impact (Impacter impacter) => impacter.effect;
    }
}