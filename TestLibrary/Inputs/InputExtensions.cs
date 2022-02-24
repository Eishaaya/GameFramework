using Microsoft.Xna.Framework.Input;

using System;

namespace BaseGameLibrary.Inputs
{
    public static class InputExtensions
    {
        public static bool OR<T> (ParamFunc<T, BoolInt, bool>[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result |= control.Call();
            }
            return result;
        }
        public static bool AND<T>(ParamFunc<T, BoolInt, bool>[] inputControls) where T : Enum
        {
            bool result = true;
            foreach (var control in inputControls)
            {
                result &= control.Call();
            }
            return result;
        }
        public static bool XOR<T>(ParamFunc<T, BoolInt, bool>[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result ^= control.Call();
            }
            return result;
        }
        public static bool NOR<T>(ParamFunc<T, BoolInt, bool>[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result |= control.Call();
            }
            return !result;
        }
        public static bool NAND<T>(ParamFunc<T, BoolInt, bool>[] inputControls) where T : Enum
        {
            bool result = true;
            foreach (var control in inputControls)
            {
                result &= control.Call();
            }
            return !result;
        }

        public static bool Digital<T>(T key, BoolInt invert) where T : Enum
            => !(InputManager<T>.Instance[key] ^ invert); //I have created equal
        public static bool Greater<T>(T key, BoolInt threshold) where T : Enum
            => InputManager<T>.Instance[key] > threshold;
        public static bool Less<T>(T key, BoolInt threshold) where T : Enum
            => InputManager<T>.Instance[key] < threshold;
        public static bool Equal<T>(T key, BoolInt threshold) where T : Enum
            => (int)InputManager<T>.Instance[key] == threshold;
    }
}
