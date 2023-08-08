using BaseGameLibrary.Inputs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary.Visual
{
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
                Held = wasClicked & (isClicked = value);
            }
        }

        bool isClicked;
        bool wasClicked;
        public bool Held { get; private set; }

        public static implicit operator bool(Click click) => click.isClicked;
    }

    public class ActionButton
    {
        public enum ClickType : byte
        {
            Left,
            Right,
            Middle
        }

        IParamAction[] clickActions;
        public ButtonBase Button { get; private set; }

        public ActionButton(ButtonBase button, IParamAction? leftAction = null, IParamAction rightAction = null, IParamAction middleAction = null)
            : this(button, new Dictionary<ClickType, IParamAction>())
        {

            clickActions[(int)ClickType.Left] = leftAction;
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
                    if (Button.Check(cursor, 2))
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
