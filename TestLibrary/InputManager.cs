using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public class InputManager<T> where T : Enum
    {
        Action GetState;

        [Flags]
        public enum InputSources
        {
            Keyboard = 1,
            Mouse = 2,
            HappyShaft = 4,
            All = Keyboard | Mouse | HappyShaft
        }
        InputSources sources;

        KeyboardState ks;
        MouseState ms;
        JoystickState js;



        public Dictionary<T, IPressable> Buttons { get; private set; }

        public InputManager()
            : this(new Dictionary<T, IPressable>()) { }
        public InputManager(Dictionary<T, IPressable> buttons)
            : this(buttons, GetSources(buttons)) { }
        public InputManager(Dictionary<T, IPressable> buttons, InputSources sources)
        {
            this.Buttons = buttons;
            this.sources = sources;

            ks = new KeyboardState();
            ms = new MouseState();
            js = new JoystickState();
        }

        private static Dictionary<Type, InputSources> defaultMap = new Dictionary<Type, InputSources>()
        {
            [typeof(KeyControl)] = InputSources.Keyboard,
            [typeof(MouseControl)] = InputSources.Mouse,
            [typeof(StickControl)] = InputSources.HappyShaft
        };

        public static InputSources GetSources(Dictionary<T, IPressable> buttons)
        {
            InputSources sources = 0;
            foreach (var butt in buttons)
            {
                sources |= defaultMap[butt.GetType()];
            }
            return sources;
        }

        //private static Dictionary<InputSources, Action<> defaultMap = new Dictionary<Type, InputSources>()
        //{
        //    [InputSources.Keyboard] = 
        //};

        void PrepareStates()
        {
            Action myAction = () => ks = Keyboard.GetState();
            myAction();

            if (sources.HasFlag(InputSources.Keyboard))
            {
                GetState += () => ks = Keyboard.GetState();
            }
        }


        public void Update()
        {
            GetState();
        }
    }




    //subclasses below



    public interface IPressable
    {
        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js, ref bool held);
    }

    public struct KeyControl : IPressable
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

    public struct MouseControl : IPressable
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

    public struct StickControl : IPressable
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
