using BaseGameLibrary.Inputs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGameLibrary
{
    public sealed class Screenmanager<TScreenum> where TScreenum : Enum
    {
        Action? exitAction;
        public CursorRoot Cursor { get; set; }

        GameTime gameTime;
        public TimeSpan DeltaTime => gameTime.ElapsedGameTime;
        public TimeSpan TotalTime => gameTime.TotalGameTime;

        public IScreen<TScreenum> CurrentScreen => activeScreens.Peek();

        Stack<IScreen<TScreenum>> activeScreens;
        Dictionary<TScreenum, IScreen<TScreenum>> allScreens;
        public static Screenmanager<TScreenum> Instance { get; } = new();

    #nullable disable
        private Screenmanager() { }
    #nullable enable

        public void Init<TCursor>(TCursor mouse, Action? exitAction = null, params (TScreenum key, IScreen<TScreenum> screen)[] screens) where TCursor : CursorRoot
        {
            Cursor = mouse;
            activeScreens = new();
            allScreens = screens.ToDictionary(m => m.key, m => m.screen);
            activeScreens.Push(screens[0].screen);
            CurrentScreen.Start();
            this.exitAction = exitAction;
        }



        public void Back()
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
        public void Update(GameTime time)
        {
            gameTime = time;
            foreach (var screen in activeScreens)
            {
                screen.Update();
                if (!screen.UpdateBelow) break;
            }
        }

        public void Draw() => Draw(activeScreens.GetEnumerator());
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
