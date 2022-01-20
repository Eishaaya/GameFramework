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
            clickActions.Add(ClickType.Left, leftAction);
            clickActions.Add(ClickType.Right, rightAction);
            clickActions.Add(ClickType.Middle, middleAction);
        }

        public ActionButton(Button button, Dictionary<ClickType, IParamAction> clickActions)
        {
            this.button = button;
            this.clickActions = clickActions;
        }
    }
}
