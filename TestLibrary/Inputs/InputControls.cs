using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System.Text;

namespace BaseGameLibrary.Inputs
{
    public abstract class InputControl
    {
        protected bool beenChecked;
        public InputStateComponent StateComponent { get; }

        public InputControl(InputStateComponent inputStateComponent)
        {
            StateComponent = inputStateComponent;
        }
        public virtual void Update(GameTime gameTime)
        {
            StateComponent.Update(gameTime);
            beenChecked = false;
        }
        public int GetValue(KeyboardState ks, MouseState ms, JoystickState js)
            => StateComponent.GetValue;
        public bool Pressed(KeyboardState ks, MouseState ms, JoystickState js)
        {
            if (!beenChecked)
            {
                beenChecked = true;
                var isDown = PressLogic(ks, ms, js);
                return StateComponent.Press(isDown);
            }
            return StateComponent;
        }
        public abstract BoolInt PressLogic(KeyboardState ks, MouseState ms, JoystickState js);

    }

    public class KeyControl : InputControl
    {
        public Keys myKey;

        public KeyControl(Keys key, InputStateComponent inputStateComponent)
            : base(inputStateComponent)
        {
            myKey = key;
        }
        public override BoolInt PressLogic(KeyboardState ks, MouseState ms, JoystickState js)
            => ks.IsKeyDown(myKey);
    }

    public class MouseControl : InputControl
    {
        ParamFunc<MouseState, BoolInt> myClick;

        public MouseControl(ParamFunc<MouseState, BoolInt> click, InputStateComponent inputStateComponent)
            : base(inputStateComponent)
        {
            myClick = click;
        }

        public override BoolInt PressLogic(KeyboardState ks, MouseState ms, JoystickState js)
        {
            return myClick.Call(ms);
        }
    }

    public class StickControl : InputControl
    {
        int buttonIndex;

        public StickControl(int index, InputStateComponent inputStateComponent)
            : base(inputStateComponent)
        {
            buttonIndex = index;
        }

        public override BoolInt PressLogic(KeyboardState ks, MouseState ms, JoystickState js)
            => js.Buttons[buttonIndex] == ButtonState.Pressed;
        
    }
}
