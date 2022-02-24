using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace BaseGameLibrary.Inputs
{
    public class ComplexControl<T> : InputControl where T : Enum
    {
        public enum ControlType
        {
            OR,
            AND,
            XOR,
            NOR,
            NAND,
            XNOR
        }
        public enum CompareType
        {
            LessThan,
            GreaterThan,
            EqualTo,
            Digital
        }
        ControlType myType;
        public static Dictionary<ControlType, Func<ParamFunc<T, BoolInt, bool>[], bool>> Combiners { get; }
            = new Dictionary<ControlType, Func<ParamFunc<T, BoolInt, bool>[], bool>>()
            {
                [ControlType.OR] = InputExtensions.OR,
                [ControlType.AND] = InputExtensions.AND,
                [ControlType.XOR] = InputExtensions.XOR,
                [ControlType.NAND] = InputExtensions.NAND,
                [ControlType.NOR] = InputExtensions.NOR,
                [ControlType.XNOR] = InputExtensions.XNOR
            };
        public static Dictionary<CompareType, Func<T, BoolInt, bool>> Comparers { get; }
    = new Dictionary<CompareType, Func<T, BoolInt, bool>>()
    {
        [CompareType.Digital] = InputExtensions.Digital,
        [CompareType.GreaterThan] = InputExtensions.Greater,
        [CompareType.LessThan] = InputExtensions.Less,
        [CompareType.EqualTo] = InputExtensions.Equal,
    };

        ParamFunc<T, BoolInt, bool>[] subComponents;
        public ComplexControl(ControlType myType, InputStateComponent inputStateComponent, params T[] subKeys)
            : this(myType, inputStateComponent)
        {
            subComponents = new ParamFunc<T, BoolInt, bool>[subKeys.Length];
            for (int i = 0; i < subKeys.Length; i++)
            {
                subComponents[i] = new ParamFunc<T, BoolInt, bool>(Comparers[CompareType.Digital], subKeys[i], true);
            }
        }
        public ComplexControl(ControlType myType, InputStateComponent inputStateComponent, CompareType compareType, params (T key, BoolInt modifier)[] subInfo)
            : this(myType, inputStateComponent)
        {
            subComponents = new ParamFunc<T, BoolInt, bool>[subInfo.Length];
            for (int i = 0; i < subInfo.Length; i++)
            {
                subComponents[i] = new ParamFunc<T, BoolInt, bool>(Comparers[compareType], subInfo[i].key, subInfo[i].modifier);
            }
        }
        public ComplexControl(ControlType myType, InputStateComponent inputStateComponent, params (T key, BoolInt modifier, Func<T, BoolInt, bool> func)[] subPairs)
            : this(myType, inputStateComponent)
        {
            subComponents = new ParamFunc<T, BoolInt, bool>[subPairs.Length];
            for (int i = 0; i < subComponents.Length; i++)
            {
                subComponents[i] = new ParamFunc<T, BoolInt, bool>(subPairs[i].func, subPairs[i].key, subPairs[i].modifier);
            }
        }
        public ComplexControl(ControlType myType, InputStateComponent inputStateComponent)
            :base(inputStateComponent)
        {
            this.myType = myType;
        }

        public override bool Pressed(KeyboardState ks, MouseState ms, JoystickState js)
        {
            var isDown = Combiners[myType](subComponents);
            return StateComponent.Press(isDown);
        }
        public override void Update(GameTime gameTime) { }
    }
}
