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

        public ClickStatus Clicked(Sprite button, Info condition)
        {
            if (this[Info.Left])
            {
                ; // this never hits
            }
            if (button.Hitbox.Contains(Location))
            {
                var prev = this[condition];
                if (this[condition])
                {
                    if (prev)
                    {
                        return ClickStatus.Held;
                    }
                    return ClickStatus.Clicked;
                }
                return ClickStatus.Hovering;
            }
            return ClickStatus.No;
        }

        public abstract InputStateComponent this[Info key] { get; }
        public abstract InputStateComponent CurrentState(Info key);
        public abstract bool Held { get; protected set; }
    }

    //Redundant after better inputs
    #region MouseComponent
    //public sealed class MouseComponent<TInput> where TInput : Enum
    //{
    //    public TInput key { get; }
    //    public InputStateComponent value { get; private set; }
    //    bool beenChecked;

    //    public static implicit operator InputStateComponent(MouseComponent<TInput> me)
    //    {
    //        if (me.beenChecked)
    //        {
    //            me.value = InputManager<TInput>.Instance[me.key];
    //        }
    //        return me.value;
    //    }

    //    public void Update()
    //    {
    //        beenChecked = false;
    //    }
    //}
    #endregion

    public sealed class Cursor<TInput> : ICursor where TInput : Enum
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

        public void Update()
        {
            var oldLocation = Location;
            Location = new Vector2((int)InputManager<TInput>.Instance[Inputs[Info.X]], (int)InputManager<TInput>.Instance[Inputs[Info.Y]]);
            Moved = oldLocation == Location;
            Held = LeftButton || RightButton || MiddleButton;
        }

        public override InputStateComponent CurrentState(Info key)
        {
            throw new NotImplementedException();
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

        public override bool Held { get; protected set; }

        public override InputStateComponent this[Info key]
        {
            get
            {
                var annoyed = InputManager<TInput>.Instance[Inputs[Info.Scroll]];
                if (annoyed)
                {
                    ;
                }
                return annoyed;
            }
        }

        public static explicit operator Vector2(Cursor<TInput> mouse) => mouse.Location;
    }
}
