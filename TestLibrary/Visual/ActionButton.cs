using BaseGameLibrary.Inputs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    //public delegate void SettingChangedDelegate<T>(Screen screen, Setting<T> setting);

    //public abstract class Setting<T>
    //{
    //    public event Action<Screen, Setting<T>> SomethingHappened;
    //   // public event SettingChangedDelegate<T> SomethingElseHappened;

    //    public T Value
    //    {
    //        get => Value;
    //        set
    //        {
    //            OldValue = Value;
    //            Value = value;                
    //        }
    //    }
    //    public T OldValue { get; private set; }

    //    public void Revert()
    //    {
    //        Value = OldValue;
    //    }
    //}

    interface ButtonEffect
    {
        void Call();
    }

    public struct Click
    {
        public bool IsClicked
        {
            get => isClicked;
            set
            {
                wasClicked = isClicked;
                isClicked = value;
                Held = wasClicked && isClicked;
            }
        }

        bool isClicked;
        bool wasClicked;
        public bool Held { get; private set; }

        public static implicit operator bool(Click click) => click.isClicked;
    }

    public class ActionButton
    {
        public enum ClickType : int
        {
            Left,
            Right,
            Middle
        }

        Dictionary<ClickType, IParamAction> clickActions;
        public ButtonBase Button { get; private set; }

        public ActionButton(ButtonBase button, IParamAction leftAction = null, IParamAction rightAction = null, IParamAction middleAction = null)
            : this(button, new Dictionary<ClickType, IParamAction>())
        {
            if (leftAction != null)
            {
                clickActions.Add(ClickType.Left, leftAction);
            }
            if (rightAction != null)
            {
                clickActions.Add(ClickType.Right, rightAction);
            }
            if (middleAction != null)
            {
                clickActions.Add(ClickType.Middle, middleAction);
            }
        }

        public ActionButton(ButtonBase button, Dictionary<ClickType, IParamAction> clickActions)
        {
            this.Button = button;
            this.clickActions = clickActions;
        }

        public void Click(Click[] presses, bool heldMouse, Vector2 mousePos)
        {
            for (int i = 0; i < presses.Length; i++)
            {
                var checkedClick = (ClickType)i;
                if (clickActions.ContainsKey(checkedClick) && !heldMouse)
                {
                    if (Button.Check(mousePos, presses[i]))
                    {
                        clickActions[checkedClick].Call();
                    }
                }
            }
        }

        public void Click(CursorRoot cursor)
        {
            for (int i = 0; i < clickActions.Count; i++)
            {
                var checkedClick = (ClickType)i;
                if (clickActions.ContainsKey(checkedClick))
                {
                    Button.ChosenClick = (CursorRoot.Info)checkedClick;
                    if (Button.Check(cursor))
                    {
                        clickActions[checkedClick].Call();
                    }
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Button.Draw(spriteBatch);
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

        public static void SendInput<TEnum>(GameRunner<TEnum> runner, TEnum input)
            where TEnum : Enum
        {
            runner.ReceiveInput(input);
        }

        public static void BroadcastSetting(Setting setting, Screenmanager manny)
        {
            manny.ChangeSetting(setting);
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
