using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

using static BaseGameLibrary.Inputs.CursorRoot;

namespace BaseGameLibrary.Inputs
{
    public abstract class CursorRoot
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

        public Vector2 Location { get; protected set; }
        public bool Moved { get; protected set; }

        protected virtual bool Touching(SpriteBase button)
            => button.Hitbox.Contains(Location);

        public ClickStatus Clicked(SpriteBase button, Info condition)
        {
            if (Touching(button))
            {
                if (this[condition])
                {
                    if (Held)
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

    public abstract class CursorBase<TInput> : CursorRoot where TInput : Enum
    {
        protected CursorBase()
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

        public virtual void Update(GameTime gameTime)
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
            => InputManager<TInput>.Instance[Inputs[key]];

        public static explicit operator Vector2(CursorBase<TInput> mouse) => mouse.Location;
    }

    public class Cursor<TInput> : CursorBase<TInput> where TInput : Enum
    {
        public static Cursor<TInput> Mouse { get; } = new Cursor<TInput>();
    }

    public abstract class VisualCursorBase<TInput> : CursorBase<TInput> where TInput : Enum
    {
        protected SpriteBase cursor;

        protected override bool Touching(SpriteBase button)
            => button.Hitbox.Intersects(cursor.Hitbox);

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            cursor.Location = Location;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            cursor.Draw(spriteBatch);
        }
    }
    public class VisualCursor<TInput> : VisualCursorBase<TInput> where TInput : Enum
    {
        public static VisualCursor<TInput> Instance { get; } = new VisualCursor<TInput>();

        public void AttachSprite(Sprite sprite)
        {
            cursor = sprite;
            cursor.Location = Location;
        }
    }

    public class AnimatedCursor<TInput> : VisualCursorBase<TInput> where TInput : Enum
    {
        public static AnimatedCursor<TInput> Instance { get; } = new AnimatedCursor<TInput>();

        public void AttachSprite(AnimatingSprite sprite)
        {
            if (!Held)
            {
                sprite.InvertAnimation();
            }
            cursor = sprite;
            cursor.Location = Location;
        }
        public override void Update(GameTime gameTime)
        {
            var held = Held;
            base.Update(gameTime);
            if (Held != held)
            {
                ((AnimatingSprite)cursor).InvertAnimation();
            }
            cursor.Update(gameTime);
            cursor.Location = Location;
        }
    }
}
