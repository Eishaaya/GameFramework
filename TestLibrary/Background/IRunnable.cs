using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public interface IGameObject : IRunnable
    {
        void Die();
    }
    public interface IRunnable
    {
        void Update();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        
    }

    public interface ICopyable<out T> where T : ICopyable<T>
    {
        T Clone();
    }

    public interface IGameObject<out T> : IRunnable, ICopyable<T> where T : IGameObject<T> { }
}
