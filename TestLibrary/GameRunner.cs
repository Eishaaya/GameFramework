using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    abstract class GameRunner
    {
        public abstract void Reset();

        public abstract void Update(GameTime gametime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void TakeBinds(List<Impact> binds);
    }
}
