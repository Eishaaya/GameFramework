using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    class AestheticsManager : IRunnable
    {
        List<IRunnable> prettyStuff;

        public AestheticsManager(params IRunnable[] items)
        {
            prettyStuff = new List<IRunnable>();

            foreach (var item in items)
            {
                prettyStuff.Add(item);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var item in prettyStuff)
            {
                item.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in prettyStuff)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
