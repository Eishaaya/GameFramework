using Microsoft.Xna.Framework.Input;

using System;

namespace BaseGameLibrary.Inputs
{
    public static class InputControlCombiners
    {
        public static bool OR<T> (T[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result |= InputManager<T>.Instance[control];
            }
            return result;
        }
        public static bool AND<T>(T[] inputControls) where T : Enum
        {
            bool result = true;
            foreach (var control in inputControls)
            {
                result &= InputManager<T>.Instance[control];
            }
            return result;
        }
        public static bool XOR<T>(T[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result ^= InputManager<T>.Instance[control];
            }
            return result;
        }
        public static bool NOR<T>(T[] inputControls) where T : Enum
        {
            bool result = false;
            foreach (var control in inputControls)
            {
                result |= InputManager<T>.Instance[control];
            }
            return !result;
        }
        public static bool NAND<T>(T[] inputControls) where T : Enum
        {
            bool result = true;
            foreach (var control in inputControls)
            {
                result &= InputManager<T>.Instance[control];
            }
            return !result;
        }
    }
}
