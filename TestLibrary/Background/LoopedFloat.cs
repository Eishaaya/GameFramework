using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    struct LoopedFloat
    {
        public float Max;
        public float Min;

        float privValue;
        public float Value 
        {
            get => privValue;

            set
            {
                if (value < Min)
                {
                    privValue = Max + (value - Min) + 1;
                }
                else if (value > Max)
                {
                    privValue = Min + value - Max - 1;
                }
                privValue = value;
            }
        }
        public LoopedFloat(float value, float max = float.PositiveInfinity, float min = float.NegativeInfinity)
        {
            privValue = value;
            Max = max;
            Min = min;
        }

        public static implicit operator float(LoopedFloat t)
        {
            return t.Value;
        }

        public static implicit operator LoopedFloat(float t)
        {
            return new LoopedFloat(t);
        }

        public static LoopedFloat operator +(LoopedFloat a, float b)
        {
            a.Value = a.Value + b;
            while (true)
            {
                if (a.Value < a.Min)
                {
                    a.Value = a.Max + (a.Value - a.Min) + 1;
                }
                else if (a.Value > a.Max)
                {
                    a.Value = a.Min + a.Value - a.Max -1;
                }
                else
                {
                    return a;
                }
            }
        }
        public static LoopedFloat operator ++(LoopedFloat a)
        {
            var blah = (a + 1);
            return blah;
        }
    }
}
