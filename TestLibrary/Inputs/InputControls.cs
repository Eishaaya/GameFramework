using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System.Text;

namespace BaseGameLibrary.Inputs
{
    public abstract class InputControl
    {
        public InputStateComponent StateComponent { get; }

        public InputControl(InputStateComponent inputStateComponent)
        {
            StateComponent = inputStateComponent;
        }
        public virtual void Update(GameTime gameTime)
        {
            StateComponent.Update(gameTime);
        }
        public int GetValue(KeyboardState ks, MouseState ms, JoystickState js)
            => StateComponent.GetValue;
        public abstract bool Pressed(KeyboardState ks, MouseState ms, JoystickState js);
    }

    public class KeyControl : InputControl
    {
        public Keys myKey;

        public KeyControl(Keys key, InputStateComponent inputStateComponent)
            : base(inputStateComponent)
        {
            myKey = key;
        }
        public override bool Pressed(KeyboardState ks, MouseState ms, JoystickState js)
        {
            var isDown = ks.IsKeyDown(myKey);
            return StateComponent.Press(isDown);
        }
    }

    public class MouseControl : InputControl
    {
        ParamFunc<MouseState, BoolInt> myClick;

        public MouseControl(ParamFunc<MouseState, BoolInt> click, InputStateComponent inputStateComponent)
            : base(inputStateComponent)
        {
            myClick = click;
        }

        public override bool Pressed(KeyboardState ks, MouseState ms, JoystickState js)
        {
            myClick.Parameter1 = ms;
            var isDown = myClick.Call();
            return StateComponent.Press(isDown);
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

        public override bool Pressed(KeyboardState ks, MouseState ms, JoystickState js)
        {
            var isDown = js.Buttons[buttonIndex] == ButtonState.Pressed;
            return StateComponent.Press(isDown);
        }
    }
}
