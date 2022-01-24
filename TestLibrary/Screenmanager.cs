using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public class Screenmanager
    {
        public Screen CurrentScreen => activeScreens.Peek();

        Stack<Screen> activeScreens; 
        List<Screen> allScreens;
        public Stack<Screen> PreviousScreens { get; private set; }
        public bool BindsChanged { get; set; }
        public Screenmanager(List<Screen> screens)
        {
            activeScreens = new Stack<Screen>();
            allScreens = screens;
            activeScreens.Push(allScreens[0]);
            PreviousScreens = new Stack<Screen>();
            CurrentScreen.Start();
        }
        //public Screen Peek()
        //{
        //    return CurrentScreen;
        //}
        public void Back()
        {
            if (BindsChanged)
            {
                for (int i = 0; i < allScreens.Count; i++)
                {
                    allScreens[i].changeBinds(CurrentScreen.binds, CurrentScreen.GetBools());
                    BindsChanged = false;
                }
            }
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
        public void Next(int index, bool replace)
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
                activeScreens.Push(allScreens[index]);
            }
            else
            {
                PreviousScreens.Push(CurrentScreen);
                activeScreens.Push(allScreens[index]);
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
            Stack<Screen> drawScreens = new Stack<Screen>();
            while (activeScreens.Count > 0)
            {
                drawScreens.Push(activeScreens.Pop());
            }
            drawScreens.Peek().Play(time);
            while (drawScreens.Count > 0)
            {
                activeScreens.Push(drawScreens.Pop());
            }
            CurrentScreen.Update(time, this);
        }

        public void Draw(SpriteBatch batch)
        {
            Stack<Screen> drawScreens = new Stack<Screen>();
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
