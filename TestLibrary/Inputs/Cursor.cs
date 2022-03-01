using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Text;

using static BaseGameLibrary.Inputs.ICursor;

namespace BaseGameLibrary.Inputs
{
    public abstract class ICursor 
    {
        public enum Info : int
        {
            Left,
            Right,
            Middle,
            Scroll,
            X,
            Y
        }//TEST X & Y
        public enum ClickStatus : int
        {
            No,
            Hovering,
            Held,
            Clicked
        }

        public Vector2 Location { get; private set; }
        public bool Moved { get; private set; }

        public bool Clicked(Sprite button, Info condition)
        {
            if (button.Hitbox.Contains(Location))
            {
                if () //Seems derpy, ask stan
            }
        }
    }

    public sealed class Cursor<TInput> where TInput : Enum
    {

        public static Cursor<TInput> Mouse { get; } = new Cursor<TInput>();

        private Cursor()
        {
            Inputs = new Dictionary<Info, TInput>();
        }
        Dictionary<Info, TInput> Inputs;

        public void AttachClicks(TInput left, TInput right, TInput middle, TInput scroll, TInput X, TInput Y)
            => AttachClickArr(left, right, middle, scroll, X, Y);
        private void AttachClickArr(params TInput[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                Inputs[(Info)i] = inputs[i];
            }
        }



        public Vector2 Location { get; private set; }
        public bool Moved { get; private set; }

        public void UpdateLocation()
        {
            var oldLocation = Location;
            Location = new Vector2((int)InputManager<TInput>.Instance[Inputs[Info.X]], (int)InputManager<TInput>.Instance[Inputs[Info.Y]]);
            Moved = oldLocation == Location;
        }

        public InputStateComponent LeftButton
        {
            get
                => InputManager<TInput>.Instance[Inputs[Info.Left]];
        }
        public InputStateComponent RightButton
        {
            get
                => InputManager<TInput>.Instance[Inputs[Info.Right]];
        }
        public InputStateComponent MiddleButton
        {
            get
                => InputManager<TInput>.Instance[Inputs[Info.Middle]];
        }
        public InputStateComponent Scroll
        {
            get
                => InputManager<TInput>.Instance[Inputs[Info.Scroll]];
        }

        public static explicit operator Vector2(Cursor<TInput> mouse) => mouse.Location;
    }
}
