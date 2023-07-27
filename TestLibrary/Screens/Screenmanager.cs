using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGameLibrary
{
    public class Screenmanager<TScreenum> where TScreenum : Enum
    {
        public CursorRoot Cursor { get; set; }

        GameTime gameTime;
        public TimeSpan DeltaTime => gameTime.ElapsedGameTime;
        public TimeSpan TotalTime => gameTime.TotalGameTime;
        
        public IScreen CurrentScreen => activeScreens.Peek();

        Stack<IScreen> activeScreens;
        Dictionary<TScreenum, IScreen> allScreens;
        public Stack<IScreen> PreviousScreens { get; private set; }
        public static Screenmanager<TScreenum> Instance { get; } = new();
        private Screenmanager() { }
        //public Screen Peek()
        //{
        //    return CurrentScreen;
        //}
        public void Init(CursorRoot mouse, params (TScreenum key, IScreen)[] screens)
        {
            Cursor = mouse;
            activeScreens = new Stack<IScreen>();
            allScreens = screens.ToDictionary(m => m.Item1, m => m.Item2);
            activeScreens.Push(screens[0].screen);
            PreviousScreens = new Stack<IScreen>();
            CurrentScreen.Start();
        }



    public void Back()
        {
            activeScreens.Pop().StopMusic();
            if (activeScreens.Count > 0)
            {
                if (CurrentScreen != PreviousScreens.Peek())
                {
                    activeScreens.Pop().StopMusic();
                }
                else
                {
                    PreviousScreens.Pop();
                    CurrentScreen.heldMouse = true;
                    CurrentScreen.Start();
                    return;
                }
            }
            activeScreens.Push(PreviousScreens.Pop());
            CurrentScreen.heldMouse = true;
            CurrentScreen.Start();
        }
        public void Next(TScreenum choice, bool replace)
        {
            if (replace)
            {
                CurrentScreen.StopMusic();
                PreviousScreens.Push(activeScreens.Pop());
                if (activeScreens.Count > 0)
                {
                    CurrentScreen.StopMusic();
                    activeScreens.Clear();
                }
                activeScreens.Push(allScreens[choice]);
            }
            else
            {
                PreviousScreens.Push(CurrentScreen);
                activeScreens.Push(allScreens[choice]);
            }
            CurrentScreen.heldMouse = true;
            CurrentScreen.Start();
        }
        public void ClearMemory()
        {
            PreviousScreens.Clear();
        }
        public void Update(GameTime time)
        {
            gameTime = time;
            Stack<IScreen> drawScreens = new();
            while (activeScreens.Count > 0)
            {
                drawScreens.Push(activeScreens.Pop());
            }
            drawScreens.Peek().Play(time);
            while (drawScreens.Count > 0)
            {
                activeScreens.Push(drawScreens.Pop());
            }
            CurrentScreen.Update(time);
        }

        public void Draw(SpriteBatch batch)
        {
            Stack<IScreen> drawScreens = new ();
            while (activeScreens.Count > 0)
            {
                drawScreens.Push(activeScreens.Pop());
            }
            while (drawScreens.Count > 0)
            {
                var current = drawScreens.Pop();
                current.Draw(batch);
                activeScreens.Push(current);
            }
        }
    }
}
