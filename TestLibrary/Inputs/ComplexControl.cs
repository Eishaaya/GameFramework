using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

using static BaseGameLibrary.Inputs.InputExtensions;

namespace BaseGameLibrary.Inputs
{
    public class ComplexControl<T> : InputControl where T : Enum
    {
        ControlType myType;
        public static Dictionary<ControlType, Func<ParamFunc<T, BoolInt, BoolInt>[], BoolInt>> Combiners { get; }
            = new Dictionary<ControlType, Func<ParamFunc<T, BoolInt, BoolInt>[], BoolInt>>()
            {
                [ControlType.OR] = OR,
                [ControlType.AND] = AND,
                [ControlType.XOR] = XOR,
                [ControlType.NAND] = NAND,
                [ControlType.NOR] = NOR,
                [ControlType.XNOR] = XNOR,
                [ControlType.Add] = Add,
                [ControlType.Subtract] = Subtract,
                [ControlType.Multiply] = Multiply,
                [ControlType.Divide] = Divide,
            };
        public static Dictionary<CompareType, Func<T, BoolInt, BoolInt>> Comparers { get; }
            = new Dictionary<CompareType, Func<T, BoolInt, BoolInt>>()
            {
                [CompareType.Digital] = Digital,
                [CompareType.GreaterThan] = Greater,
                [CompareType.LessThan] = Less,
                [CompareType.EqualTo] = Equal,
                [CompareType.Value] = Value
            };

        Func<ComplexControl<T>, BoolInt, BoolInt> pressModifier;

        ParamFunc<T, BoolInt, BoolInt>[] subComponents;
        public ComplexControl(ControlType myType, Func<ComplexControl<T>, BoolInt, BoolInt> pressModifier, InputStateComponent inputStateComponent)
            : base(inputStateComponent)
        {
            this.myType = myType;
            this.pressModifier = pressModifier;
        }
        public ComplexControl(ControlType myType, Func<ComplexControl<T>, BoolInt, BoolInt> pressModifier, InputStateComponent inputStateComponent, DefaultSelector selector, params T[] subKeys)
            : this(myType, pressModifier, inputStateComponent)
        {
            subComponents = new ParamFunc<T, BoolInt, BoolInt>[subKeys.Length];
            for (int i = 0; i < subKeys.Length; i++)
            {
                subComponents[i] = new ParamFunc<T, BoolInt, BoolInt>(Comparers[(CompareType)selector], subKeys[i], true);
            }
        }
        public ComplexControl(ControlType myType, Func<ComplexControl<T>, BoolInt, BoolInt> pressModifier, InputStateComponent inputStateComponent, CompareType compareType, params (T key, BoolInt modifier)[] subInfo)
            : this(myType, pressModifier, inputStateComponent)
        {
            subComponents = new ParamFunc<T, BoolInt, BoolInt>[subInfo.Length];
            for (int i = 0; i < subInfo.Length; i++)
            {
                subComponents[i] = new ParamFunc<T, BoolInt, BoolInt>(Comparers[compareType], subInfo[i].key, subInfo[i].modifier);
            }
        }
        public ComplexControl(ControlType myType, Func<ComplexControl<T>, BoolInt, BoolInt> pressModifier, InputStateComponent inputStateComponent, params (T key, BoolInt modifier, Func<T, BoolInt, BoolInt> func)[] subPairs)
            : this(myType, pressModifier, inputStateComponent)
        {
            subComponents = new ParamFunc<T, BoolInt, BoolInt>[subPairs.Length];
            for (int i = 0; i < subComponents.Length; i++)
            {
                subComponents[i] = new ParamFunc<T, BoolInt, BoolInt>(subPairs[i].func, subPairs[i].key, subPairs[i].modifier);
            }
        }

        public override BoolInt PressLogic (KeyboardState ks, MouseState ms, JoystickState js)
        {
            return pressModifier(this, Combiners[myType](subComponents));
        }
    }
}
