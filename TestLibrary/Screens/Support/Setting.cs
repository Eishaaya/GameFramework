using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary.Screens.Support
{
    //Magic stan APPROVED
    [StructLayout(LayoutKind.Explicit)]
    public struct Setting
    {
        public enum Types : byte
        {
            BoolValue,
            FloatValue,
            KeyValue,
            IntValue
        }

        [FieldOffset(0)]
        public int ID;

        [FieldOffset(4)]
        public Types Type;

        [FieldOffset(5)]
        public bool BoolValue;

        [FieldOffset(5)]
        public float FloatValue;

        [FieldOffset(5)]
        public Keys KeyValue;

        [FieldOffset(5)]
        public int IntValue;

        [FieldOffset(9)]
        public int oldValue;


        #region bob the builder
        public Setting(int ID, Types myType, int value)
            : this(ID, myType)
        {
            IntValue = value;
            oldValue = IntValue;
        }

        public Setting(int ID, Types myType, float value)
            : this(ID, myType)
        {
            FloatValue = value;
            oldValue = IntValue;
        }

        public Setting(int ID, Types myType, bool value)
            : this(ID, myType)
        {
            BoolValue = value;
            oldValue = IntValue;
        }

        public Setting(int ID, Types myType, Keys value)
            : this(ID, myType)
        {
            KeyValue = value;
            oldValue = IntValue;
        }

        public Setting(int ID, Types myType)
        {
            this.ID = ID;
            Type = myType;

            KeyValue = Keys.None;
            BoolValue = false;
            FloatValue = 0;
            IntValue = 0;
            oldValue = 0;
        }
        #endregion

        public void Revert()
        {
            IntValue = oldValue;
        }
        public void UpdateValue()
        {
            oldValue = IntValue;
        }

        public static explicit operator bool(Setting setting) => setting.BoolValue;
        public static explicit operator float(Setting setting) => setting.FloatValue;
        public static explicit operator Keys(Setting setting) => setting.KeyValue;
        public static explicit operator int(Setting setting) => setting.IntValue;
    }
}
