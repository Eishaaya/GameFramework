using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGameLibrary
{
    public abstract class ScreeenManagerBase
    {


        static Func<ScreeenManagerBase>? instance;
        protected static Func<ScreeenManagerBase> InstanceFunc
        {
            get => instance?? throw new InvalidOperationException("Access the instance of an initialized concrete Screenmanager before using this!");
            set => instance = instance == null ? value : throw new InvalidOperationException("You should not have two screenmanagers active at the same time!");
        }

        public static ScreeenManagerBase Instance => InstanceFunc();

#nullable disable
        public CursorRoot Cursor { get; protected set; }
        GameTime gameTime;
#nullable enable

        public TimeSpan DeltaTime => gameTime.ElapsedGameTime;
        public TimeSpan TotalTime => gameTime.TotalGameTime;

        public abstract void Back();
        public virtual void Update(GameTime time)
        {
            gameTime = time;
        }
        public abstract void Draw();
    }
    public sealed class ScreenManager<TScreenum> :ScreeenManagerBase where TScreenum : Enum
    {
        Action? exitAction;

        public IScreen<TScreenum> CurrentScreen => activeScreens.Peek();

        Stack<IScreen<TScreenum>> activeScreens;
        Dictionary<TScreenum, IScreen<TScreenum>> allScreens;
        public static new ScreenManager<TScreenum> Instance { get; } = new();

#nullable disable
        private ScreenManager() { ScreeenManagerBase.InstanceFunc = () => Instance; }
#nullable enable

        public void Init(CursorRoot mouse, Action? exitAction = null, params (TScreenum key, IScreen<TScreenum> screen)[] screens)
        {
            Cursor = mouse;
            activeScreens = new();
            allScreens = screens.ToDictionary(m => m.key, m => m.screen);
            activeScreens.Push(screens[0].screen);
            CurrentScreen.Start();
            this.exitAction = exitAction;
        }



        public override void Back()
        {
            activeScreens.Pop().Stop();
            if (activeScreens.Count > 0)
            {
                CurrentScreen.Resume();
            }
            else
            {
                exitAction?.Invoke();
            }
        }
        public void ReplaceCurrent(TScreenum choice)
        {
            CurrentScreen.Pass();
            activeScreens.Push(allScreens[choice]);
            CurrentScreen.Start();
        }
        public void Next(TScreenum choice)
        {
            activeScreens.Push(allScreens[choice]);
            CurrentScreen.Start();
        }
        public override void Update(GameTime time)
        {
            base.Update(time);
            foreach (var screen in activeScreens)
            {
                screen.Update();
                if (!screen.UpdateBelow) break;
            }
        }

        public override void Draw() => Draw(activeScreens.GetEnumerator());
        private void Draw(Stack<IScreen<TScreenum>>.Enumerator screenEnumerator)
        {
            if (!screenEnumerator.MoveNext()) return;

            var current = screenEnumerator.Current;

            if (current.DrawBelow)
            {
                Draw(screenEnumerator);
            }

            current.Draw();
        }


    }
}
