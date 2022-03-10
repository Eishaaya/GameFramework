using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGameLibrary
{
    public class AestheticsManager : IRunnable
    {
        List<IRunnable> prettyStuff;
        public bool Running { get; set; }

        public AestheticsManager(params IRunnable[] items)
            : this(items.AsEnumerable()) { }
        public AestheticsManager(IEnumerable<IRunnable> items)
        {
            Running = true;
            prettyStuff = new List<IRunnable>();

            if (items != null)
            {
                foreach (var item in items)
                {
                    prettyStuff.Add(item);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!Running) return;

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
