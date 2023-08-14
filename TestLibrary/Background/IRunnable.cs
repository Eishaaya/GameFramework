using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{    
    public interface IGameObject : IRunnable, IPoolable { }
    public interface IRunnable
    {
        void Update();
        void Update(GameTime gameTime) => Update();
        void Draw(SpriteBatch spriteBatch);
        void Pause() { }
        void Begin() { }
    }

    public interface ICopyable<out T> where T : ICopyable<T>
    {
        T Clone();
    }

    public interface IGameObject<out T> : IGameObject, ICopyable<T> where T : IGameObject<T> { }
}
