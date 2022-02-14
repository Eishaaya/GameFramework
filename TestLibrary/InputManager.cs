using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public class InputManager <T> where T : Enum
    {
        [Flags]
        enum InputSources
        {
            Keyboard = 1,
            Mouse = 2,
            HappyShaft = 4
        }

        int sources;  

        Dictionary<T, iPressable> buttons;

        public InputManager()
            : this(new Dictionary<T, iPressable>()) { }
        public InputManager(Dictionary<T, iPressable> buttons)
            : this(buttons, 0) 
        {
            foreach (var butt in buttons)
            {
                if (butt.Value is KeyControl)
                {
                    sources |= (int)InputSources.Keyboard;
                }
                else if (butt.Value is MouseControl)
                {
                    sources |= (int)InputSources.Mouse;
                }
                else if (butt.Value is JoystickState)
                {
                    sources |= (int)InputSources.HappyShaft;
                }
            }
        }
        public InputManager(Dictionary<T, iPressable> buttons, int sources)
        {
            this.buttons = buttons;
            this.sources = sources;
        }
    }

    public interface iPressable
    {
        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js, ref bool held);
    }

    public struct KeyControl : iPressable
    {
        Keys myKey;
        bool prevState;

        public KeyControl(Keys key, bool pressed = false)
        {
            myKey = key;
            prevState = pressed;
        }

        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js, ref bool held)
        {            
            var isDown = ks.IsKeyDown(myKey);
             
            held = isDown && prevState;
            prevState = isDown;
            return isDown;
        }
    }

    public struct MouseControl : iPressable
    {
        ParamFunc<MouseState, bool> myClick;
        bool prevState;

        public MouseControl(ParamFunc<MouseState, bool> click, bool pressed = false)
        {
            myClick = click;
            prevState = pressed;
        }

        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js, ref bool held)
        {
            myClick.Parameter1 = ms;
            var isDown = myClick.Call();

            held = isDown && prevState;
            prevState = isDown;
            return isDown;
        }
    }

    public struct StickControl : iPressable
    {
        int buttonIndex;
        bool prevState;

        public StickControl(int index, bool pressed = false)
        {
            buttonIndex = index;
            prevState = pressed;
        }

        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js, ref bool held)
        {
            var isDown = js.Buttons[buttonIndex] == ButtonState.Pressed;

            held = isDown && prevState;
            prevState = isDown;
            return isDown;
        }
    }
}
