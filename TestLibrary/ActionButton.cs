using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    class ActionButton
    {
        public enum ClickType
        {
            Left,
            Right,
            Middle
        }

        Dictionary<ClickType, IParamAction> clickActions;
        Button button;

        public ActionButton(Button button, IParamAction leftAction = null, IParamAction rightAction = null, IParamAction middleAction = null)
            : this(button, new Dictionary<ClickType, IParamAction>())
        {
            if (leftAction == null)
            {
                clickActions.Add(ClickType.Left, leftAction);
            }
            if (rightAction == null)
            {
                clickActions.Add(ClickType.Right, rightAction);
            }
            if (middleAction == null)
            {
                clickActions.Add(ClickType.Middle, middleAction);
            }
        }

        public ActionButton(Button button, Dictionary<ClickType, IParamAction> clickActions)
        {
            this.button = button;
            this.clickActions = clickActions;
        }

        public void Click(bool[] presses, Vector2 mousePos)
        {
            for (int i = 0; i < presses.Length; i++)
            {
                var checkedClick = (ClickType)i;
                if (clickActions.ContainsKey(checkedClick) && button.Check(mousePos, presses[i]))
                {
                    clickActions[checkedClick].Call();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            button.Draw(spriteBatch);
        }

    }

    public static class ManagementActions
    {
        public static void Exit(Game game)
        {
            game.Exit();
        }
        public static void Reset(Screen screen)
        {
            screen.Reset();
        }
        public static void DeleteHistory(Screenmanager manny, int clearLimit, bool reset)
        {
            if (!reset && clearLimit >= manny.PreviousScreens.Count)
            {
                manny.ClearMemory();
            }
            else
            {
                clearLimit = Math.Min(clearLimit, manny.PreviousScreens.Count);
                for (int i = 0; i < clearLimit; i++)
                {
                    manny.PreviousScreens.Pop();
                }

                if (reset)
                {
                    for (int i = 0; i < clearLimit; i++)
                    {
                        manny.PreviousScreens.Pop().Reset();
                    }
                }
            }
        }

        public static void Back(Screenmanager manny, bool reset)
        {
            manny.Back();
            if (reset)
            {
                manny.CurrentScreen.Reset();
            }
        }
        
        public static void Next(Screenmanager manny, int destination, bool replace, bool clear, int clearLimit, bool reset)
        {
            manny.Next(destination, replace);
            if (clear)
            {
                ManagementActions.DeleteHistory(manny, clearLimit, reset);
            }
        }

        public static void SendInput<TEnum> (GameRunner<TEnum> runner, TEnum input)
            where TEnum: Enum
        {
            runner.ReceiveInput(input);
        }
    }

    public struct InputValue
    {
        public static InputValue<TData, TType> Create<TData, TType>(TData data, TType type)
            where TType : Enum
            => new InputValue<TData, TType>() { Data = data, ValueType = type };
    }

    public struct InputValue<TData, TType>
        where TType : Enum
    {

        public TData Data { get; init; }
        public TType ValueType { get; init; }
    }


    public enum InputValueTypes
    {
        Int,
        Enum,
    }
}


namespace System.Runtime.CompilerServices // CS0518 ErrorCode workaround
{
    internal static class IsExternalInit { }
}
