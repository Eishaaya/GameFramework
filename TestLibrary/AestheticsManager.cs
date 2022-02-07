using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public class AestheticsManager : ICloneable<AestheticsManager>
    {
        List<IRunnable> prettyStuff;

        public AestheticsManager(params IRunnable<VisualObject>[] items)
        {
            prettyStuff = new List<IRunnable<VisualObject>>();

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

        public AestheticsManager Clone()
            => new AestheticsManager(prettyStuff.ToArray());

    }
}
