﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BaseGameLibrary
{
    using DelayFunc = ParamFunc<Timer, bool>;
    public class Sequence : IRunnable    {
        /// <summary>
        /// the operations that make up the sequence
        /// </summary>
        public List<IParamFunc<bool>> Tasks { get; private set; }
        int current;

        public Sequence(params IParamFunc<bool>[] funcs)
        {
            current = 0;
            Tasks = new(funcs);
        }
        ///// <summary>
        ///// Assigns the tasks the sequence will run
        ///// </summary>
        ///// <param name="funcs">the tasks</param>
        //public void AssignTasks(params IParamFunc<bool>[] funcs)
        //{
        //    Tasks = new List<IParamFunc<bool>>(funcs);
        //}        

        public static DelayFunc Delay(int time) => new (m => { m.Tick(ScreenManager.Instance.DeltaTime); return m.Ready(); }, new Timer(time));

        //private bool Wait(int time)
        //{
        //    ticking = true;

        //    delay.SetLength(time);

        //    return delay.Ready();
        //}

        public int RunSequence()
        {
            if (Tasks[current].Call())
            {
                current = (current + 1) % Tasks.Count;            
            }

            return current;
        }

        void IRunnable.Update(GameTime gameTime) => RunSequence();
        void IRunnable.Update() => RunSequence();

        public void Draw(SpriteBatch spriteBatch) { }

        public void Pause() { }

        public void Begin() => current = 0;
    }
}
