using BaseGameLibrary.Inputs;
using BaseGameLibrary.Visual;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using static BaseGameLibrary.Visual.ActionButton;

namespace BaseGameLibrary
{



    enum MyGameInputs
    {
        MoveLeft,
        MoveRight,
        Jump,
        Fire,
        Horsey
    }

    //class InputManager<T> where T: Enum
    //{
    //    private Dictionary<T, List<(Func<KeyboardState, object[], bool> predicate, object[] parameters)>> keyMappings = new Dictionary<T, List<(Func<KeyboardState, object[], bool> predicate, object[] parameters)>>();

    //    public bool UseKeyboard { get; private set; } = false;
    //    public bool UseMouse { get; private set; } = false;
    //    public bool UseJoystick { get; private set; } = false;

    //    private KeyboardState ks;

    //    public void AddMapping(T input, Func<KeyboardState, object[], bool> mapping, params object[] parameters)
    //    {
    //        UseKeyboard = true;

    //        if(!keyMappings.ContainsKey(input))
    //        {
    //            keyMappings[input] = new List<(Func<KeyboardState, object[], bool> predicate, object[] parameters)>();
    //        }

    //        keyMappings[input].Add((mapping, parameters));
    //    }

    //    public void Update()
    //    {
    //        if(UseKeyboard)
    //        {
    //            // update KeyboardState
    //        }
    //    }

    //    public bool this[T index]
    //    {
    //        get
    //        {
    //            foreach (var (predicate, parameters) in keyMappings[index])
    //            {
    //                if (predicate(ks, parameters)) return true;
    //            }

    //            return false;
    //        }
    //    }
    //}

    //class MyGame
    //{
    //    void Init()
    //    {
    //        InputManager<MyGameInputs> inputManager = new InputManager<MyGameInputs>();

    //        inputManager.AddMapping(MyGameInputs.MoveLeft, (ks, key) => ks.IsKeyDown((Keys)key[0]), Keys.Left);
    //        inputManager.AddMapping(MyGameInputs.Horsey, (ks, keys) => ks.IsKeyDown((Keys)keys[0]) && ks.IsKeyDown((Keys)keys[1]), Keys.H, Keys.LeftControl);

    //        bool isHorsey = inputManager[MyGameInputs.Horsey];
    //    }
    //}

    public interface IScreen<TScreenum> where TScreenum : Enum
    {

        /// <summary>
        /// Enum ID representing this specific screen
        /// </summary>
        public TScreenum ID { get; }

        /// <summary>
        /// Whether other screens are allowed to update below this one
        /// </summary>
        public bool DrawBelow { get; }
        /// <summary>
        /// Whether other screens are allowed to update below this one
        /// </summary>
        public bool UpdateBelow { get; }

        /// <summary>
        /// screen is opened
        /// </summary>
        public abstract void Start();
        /// <summary>
        /// screen about to change to the next
        /// </summary>
        public abstract void Pass();
        /// <summary>
        /// Screen being returned to after being passed
        /// </summary>
        public abstract void Resume();
        /// <summary>
        /// Screen being fully closed
        /// </summary>
        public abstract void Stop();
        /// <summary>
        /// Update runs every frame
        /// </summary>
        public abstract void Update();
        /// <summary>
        /// Draw runs after update when monogame allows
        /// </summary>
        public abstract void Draw();
    }



    public abstract class ScreenBase<TScreenum> : IScreen<TScreenum> where TScreenum : Enum//: IRunnable
    {
        public readonly List<IRunnable> objects;

        readonly SpriteBatch spriteBatch;

        //Music management
        public abstract bool DrawBelow { get; }
        public abstract bool UpdateBelow { get; }

        public abstract TScreenum ID { get; }
        public ScreenBase(SpriteBatch spriteBatch, params IRunnable[] objects)
        {
            this.spriteBatch = spriteBatch;
            this.objects = new(objects);
        }

        protected virtual void DrawSetup() => spriteBatch.Begin();
        protected virtual void DrawConclusion() => spriteBatch.End();
        protected virtual void DrawBody()
        {
            foreach (var item in objects)
            {
                item.Draw(spriteBatch);
            }
        }
        public virtual void Draw()
        {
            DrawSetup();
            DrawBody();
            DrawConclusion();

        }

        public abstract void End();
        public virtual void Update()
        {
            foreach (var item in objects)
            {
                item.Update();
            }
        }
        public abstract void Start();
        public abstract void Pass();
        public abstract void Resume();
        public abstract void Stop();
    }
}
