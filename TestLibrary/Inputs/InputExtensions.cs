using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace BaseGameLibrary.Inputs
{

    public static class InputExtensions
    {
        //Combiner Funcs
        [Flags]
        public enum LayoutType
        {
            Restrict = 1, 
            ScaleToLabel = 2,
            AutoNewLine = 4,
        }

        public enum ControlType
        {
            OR,
            AND,
            XOR,
            NOR,
            NAND,
            XNOR,
            Add,
            Subtract,
            Multiply,
            Divide
        }
        public enum CompareType
        {
            Analog,
            Digital,
            LessThan,
            GreaterThan,
            EqualTo,
        }

        public enum DefaultSelector
        {
            Integer,
            Boolean
        }

        public static BoolInt OR<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result |= control.Call();
            }
            return result;
        }
        public static BoolInt AND<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            bool result = true;
            foreach (var control in inputControls)
            {
                result &= control.Call();
            }
            return result;
        }
        public static BoolInt XOR<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result ^= control.Call();
            }
            return result;
        }
        public static BoolInt NOR<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result |= control.Call();
            }
            return !result;
        }
        public static BoolInt NAND<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            bool result = true;
            foreach (var control in inputControls)
            {
                result &= control.Call();
            }
            return !result;
        }
        public static BoolInt XNOR<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            bool result = inputControls[0].Call();
            for (int i = 1; i < inputControls.Length; i++)
            {
                result = result == inputControls[i].Call();
            }
            return !result;
        }

        public static BoolInt Add<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            int result = inputControls[0].Call();
            for (int i = 1; i < inputControls.Length; i++)
            {
                result += inputControls[i].Call();
            }
            return result;
        }
        public static BoolInt Subtract<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            int result = inputControls[0].Call();
            for (int i = 1; i < inputControls.Length; i++)
            {
                result -= inputControls[i].Call();
            }
            return result;
        }
        public static BoolInt Multiply<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            int result = inputControls[0].Call();
            for (int i = 1; i < inputControls.Length; i++)
            {
                result *= inputControls[i].Call();
            }
            return result;
        }
        public static BoolInt Divide<T>(ParamFunc<T, BoolInt, BoolInt>[] inputControls) where T : Enum
        {
            int result = inputControls[0].Call();
            for (int i = 1; i < inputControls.Length; i++)
            {
                result /= inputControls[i].Call();
            }
            return result;
        }

        //Compare Funcs
        public static BoolInt Digital<T>(T key, BoolInt invert) where T : Enum
            => !(InputManager<T>.Instance[key] ^ invert); //I have created equal
        public static BoolInt Greater<T>(T key, BoolInt threshold) where T : Enum
            => InputManager<T>.Instance[key] > threshold;
        public static BoolInt Less<T>(T key, BoolInt threshold) where T : Enum
            => InputManager<T>.Instance[key] < threshold;
        public static BoolInt Equal<T>(T key, BoolInt threshold) where T : Enum
            => (int)InputManager<T>.Instance[key] == threshold;
        public static BoolInt Analog<T>(T key, BoolInt multiplyer) where T : Enum
            => (int)InputManager<T>.Instance[key] * multiplyer;


        //PressLogic modifiers
        public static BoolInt Value<T>(ComplexControl<T> victim, BoolInt value) where T : Enum
            => value;
        public static BoolInt AddState<T>(ComplexControl<T> victim, BoolInt value) where T : Enum
            => ((int)victim.StateComponent) + value;
        public static BoolInt SubtractState<T>(ComplexControl<T> victim, BoolInt value) where T : Enum
            => ((int)victim.StateComponent) - value;
        public static BoolInt MultiplyState<T>(ComplexControl<T> victim, BoolInt value) where T : Enum
            => ((int)victim.StateComponent) * value;
        public static BoolInt DivideState<T>(ComplexControl<T> victim, BoolInt value) where T : Enum
            => ((int)victim.StateComponent) / value;
        public static BoolInt PowerState<T>(ComplexControl<T> victim, BoolInt value) where T : Enum
            => (int)Math.Pow((int)victim.StateComponent, value);

        //StateComponent yoinkers
        public static InputStateComponent DigitalState
            => new DigitalStateComponent();
        public static InputStateComponent AnalogState
            => new AnalogStateComponent();

        public static Dictionary<TInput, Action<InputButton<T, TInput>>> GenerateInputs<T, TInput>(TInput[] cancel, TInput[] ready, params (TInput input, char value)[] keys) where TInput : Enum
        {
            var inputs = new Dictionary<TInput, Action<InputButton<T, TInput>>>();
            foreach (var command in cancel)
            {
                inputs.Add(command, InputButton<T, TInput>.Cancel);
            }
            foreach (var command in ready)
            {
                inputs.Add(command, InputButton<T, TInput>.Ready);
            }
            foreach (var key in keys)
            {
                inputs.Add(key.input, m => m.InputCandidates.Add(
                          (key.value, (int)InputManager<TInput>.Instance[key.input])));
            }
            return inputs;
        }

    }
}
