﻿using BaseGameLibrary.Inputs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BaseGameLibrary
{
    public sealed class InputManager<T> where T : Enum
    {
        private readonly static Dictionary<InputSources, Action<InputManager<T>>> defaultStates = new ()
        {
            [InputSources.Keyboard] = m => m.ks = Keyboard.GetState(),
            [InputSources.Mouse] = m => m.ms = Mouse.GetState(),
            [InputSources.HappyShaft] = m => m.js = Joystick.GetState(0) //TODO: figure out what exactly this index really means
        };


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

        public InputStateComponent InputState(T wantedInput)
            => buttons[wantedInput].StateComponent;
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


        public static InputManager<T> Instance { get; } = new ();
        Dictionary<T, InputControl> buttons; //{ get; private set; }

        public void Fill(Dictionary<T, InputControl> buttons) { Fill(buttons, GetSources(buttons)); }
        public void Fill (Dictionary<T, InputControl> buttons, InputSources sources) 
        {
            this.buttons = buttons;
            this.sources = sources;

            ks = new KeyboardState();
            ms = new MouseState();
            js = new JoystickState();

            PrepareStates();
        }

        private InputManager()
            : this(new Dictionary<T, InputControl>()) { }
        private InputManager(Dictionary<T, InputControl> buttons)
            : this(buttons, GetSources(buttons)) { }
#nullable disable
        private InputManager(Dictionary<T, InputControl> buttons, InputSources sources)
        {
            this.buttons = buttons;
            this.sources = sources;

            ks = new KeyboardState();
            ms = new MouseState();
            js = new JoystickState();

            PrepareStates();
        }
#nullable enable

        private readonly static Dictionary<Type, InputSources> defaultMap = new ()
        {
            [typeof(KeyControl)] = InputSources.Keyboard,
            [typeof(MouseControl)] = InputSources.Mouse,
            [typeof(StickControl)] = InputSources.HappyShaft,
            [typeof(ComplexControl<T>)] = InputSources.All
        };

        public static InputSources GetSources(Dictionary<T, InputControl> buttons)
        {
            InputSources sources = 0;
            foreach (var butt in buttons)
            {
                sources |= defaultMap[butt.Value.GetType()];
            }
            return sources;
        }

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
        readonly bool digital;
        [FieldOffset(0)]
        readonly int analog;

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
        public static implicit operator BoolInt(bool mine) => new (mine);

        public static implicit operator int(BoolInt me) => me.analog;
        public static implicit operator BoolInt(int mine) => new (mine);
    }
}
#endregion