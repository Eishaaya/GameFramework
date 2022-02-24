using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary.Inputs
{
    sealed class Cursor<T> where T : Enum
    {        
        public static Cursor<T> Mouse { get; } = new Cursor<T>();

        private Cursor()
        {
            Clicks = new Dictionary<T, bool>();
        }
        Dictionary<T, bool> Clicks;
        public Point Location { get; private set; }

        public static explicit operator Vector2(Cursor<T> mouse) => mouse.Location.ToVector2();
    }
}
