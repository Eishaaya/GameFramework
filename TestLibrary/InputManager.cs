using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BaseGameLibrary
{
    public class InputManager<T> where T : Enum
    {

        Action<InputManager<T>> GetState;

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

        public InputStateComponent this[T wantedInput]
        {
            get
            {
                // maybe make the T enum use flags?
                buttons[wantedInput].Pressed(ks, ms, js);
                    // : buttons[wantedInput].GetValue(ks, ms, js); //if statement probably can die
                return buttons[wantedInput].StateComponent;
            }
        }

        public Dictionary<T, IInput> buttons; //{ get; private set; }

        public InputManager()
            : this(new Dictionary<T, IInput>()) { }
        public InputManager(Dictionary<T, IInput> buttons)
            : this(buttons, GetSources(buttons)) { }
        public InputManager(Dictionary<T, IInput> buttons, InputSources sources)
        {
            this.buttons = buttons;
            this.sources = sources;

            ks = new KeyboardState();
            ms = new MouseState();
            js = new JoystickState();

            PrepareStates();
        }

        private static Dictionary<Type, InputSources> defaultMap = new Dictionary<Type, InputSources>()
        {
            [typeof(KeyControl)] = InputSources.Keyboard,
            [typeof(MouseControl)] = InputSources.Mouse,
            [typeof(StickControl)] = InputSources.HappyShaft
        };

        public static InputSources GetSources(Dictionary<T, IInput> buttons)
        {
            InputSources sources = 0;
            foreach (var butt in buttons)
            {
                sources |= defaultMap[butt.Value.GetType()];
            }
            return sources;
        }

        private static Dictionary<InputSources, Action<InputManager<T>>> defaultStates = new Dictionary<InputSources, Action<InputManager<T>>>()
        {
            [InputSources.Keyboard] = m => m.ks = Keyboard.GetState(),
            [InputSources.Mouse] = m => m.ms = Mouse.GetState(),
            [InputSources.HappyShaft] = m => m.js = Joystick.GetState(0) //TODO: figure out what exactly this index really means
        };

        void PrepareStates()
        {

            foreach (var state in defaultStates)
            {
                if (sources.HasFlag(state.Key))
                {
                    GetState += state.Value;
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            GetState(this);
            foreach (var input in buttons)
            {
                input.Value.Update(gameTime);
            }
        }
    }




    //subclasses below
    #region subclasses

    [StructLayout(LayoutKind.Explicit)]
    public struct BoolInt
    {
        [FieldOffset(0)]
        bool digital;
        [FieldOffset(0)]
        int analog;

        public BoolInt(bool digital)
        {
            analog = 0;
            this.digital = digital;
        }
        public BoolInt(int analog)
        {
            digital = false;
            this.analog = analog;
        }

        public static implicit operator bool(BoolInt me) => me.digital;
        public static implicit operator BoolInt(bool mine) => new BoolInt(mine);

        public static implicit operator int(BoolInt me) => me.analog;
        public static implicit operator BoolInt(int mine) => new BoolInt(mine);
    }

    public interface IPressable
    {
        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js);
    }
    public interface IAnalog
    {
        public int GetValue(KeyboardState ks, MouseState ms, JoystickState js);
    }
    public interface IInput : IPressable, IAnalog
    {
        public InputStateComponent StateComponent { get; }
        public void Update(GameTime gameTime);
    }

    public class InputStateComponent
    {
        bool state;
        Timer heldTimer;

        public static InputStateComponent Start
        {
            get
                => new InputStateComponent()
                {
                    state = false,
                    heldTimer = new Timer(0)
                };
        }

        public void Update(GameTime gameTime)
        {
            heldTimer.Tick(gameTime);
        }
        public bool Press(bool isDown)
        {
            var held = isDown && state;
            if (!held)
            {
                heldTimer.Reset();
            }
            state = isDown;
            return isDown;
        }
        public int GetValue()
            => heldTimer.TotalMillies;

        public static implicit operator bool(InputStateComponent me) => me.state;
        public static explicit operator int(InputStateComponent me) => me.heldTimer.TotalMillies;
        public static bool operator >(InputStateComponent me, int other)
        { return me.heldTimer.Millies > other; }
        public static bool operator <(InputStateComponent me, int other)
            => me.heldTimer.TotalMillies > other;
    }

    public struct KeyControl : IInput
    {
        public Keys myKey;
        public InputStateComponent StateComponent { get; }

        public KeyControl(Keys key, bool pressed = false)
        {
            myKey = key;
            StateComponent = InputStateComponent.Start;
        }

        public int GetValue(KeyboardState ks, MouseState ms, JoystickState js)
            => StateComponent.GetValue();

        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js)
        {
            var isDown = ks.IsKeyDown(myKey);
            return StateComponent.Press(isDown);
        }

        public void Update(GameTime gameTime)
        {
            StateComponent.Update(gameTime);
        }
    }

    public struct MouseControl : IInput
    {
        ParamFunc<MouseState, bool> myClick;
        public InputStateComponent StateComponent { get; }

        public MouseControl(ParamFunc<MouseState, bool> click, bool pressed = false)
        {
            myClick = click;
            StateComponent = InputStateComponent.Start;
        }

        public int GetValue(KeyboardState ks, MouseState ms, JoystickState js)
        {
            return StateComponent.GetValue();
        }

        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js)
        {
            myClick.Parameter1 = ms;
            var isDown = myClick.Call();
            return StateComponent.Press(isDown);
        }

        public void Update(GameTime gameTime)
        {
            StateComponent.Update(gameTime);
        }
    }

    public struct StickControl : IInput
    {
        int buttonIndex;
        public InputStateComponent StateComponent { get; }

        public StickControl(int index, bool pressed = false)
        {
            buttonIndex = index;
            StateComponent = InputStateComponent.Start;
        }

        public int GetValue(KeyboardState ks, MouseState ms, JoystickState js)
        {
            return StateComponent.GetValue();
        }

        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js)
        {
            var isDown = js.Buttons[buttonIndex] == ButtonState.Pressed;
            return StateComponent.Press(isDown);
        }

        public void Update(GameTime gameTime)
        {
            StateComponent.Update(gameTime);
        }
    }
}
#endregion