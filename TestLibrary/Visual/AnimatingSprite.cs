using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary.Visual
{
    public abstract class AnimationFrame
    {
        public abstract void Draw(SpriteBatch batch, Texture2D Image, Vector2 Location, Color Color, float rotation, Vector2 Origin, float Scale, SpriteEffects effect, float Depth);
        public abstract Vector2 Dimensions { get; }
    }

    public class RectangleFrame : AnimationFrame
    {
        RectangleFrame(Rectangle rectangle)
        {
            rect = rectangle;
        }
        Rectangle rect;
        public override Vector2 Dimensions => new Vector2(rect.Width, rect.Height);

        public static implicit operator Rectangle(RectangleFrame frame) => frame.rect;
        public static implicit operator RectangleFrame(Rectangle frame) => new RectangleFrame(frame);

        public override void Draw(SpriteBatch batch, Texture2D Image, Vector2 Location, Color Color, float rotation, Vector2 Origin, float Scale, SpriteEffects effect, float Depth)
        {
            batch.Draw(Image, Location, this, Color, rotation, Origin, Scale, effect, Depth);
        }
    }

    public class TextureFrame : AnimationFrame
    {
        TextureFrame(Texture2D txtr)
        {
            texture = txtr;
        }
        Texture2D texture;

        public override Vector2 Dimensions => new Vector2(texture.Width, texture.Height);

        public static implicit operator Texture2D(TextureFrame frame) => frame.texture;
        public static implicit operator TextureFrame(Texture2D frame) => new TextureFrame(frame);

        public override void Draw(SpriteBatch batch, Texture2D Image, Vector2 Location, Color Color, float rotation, Vector2 Origin, float Scale, SpriteEffects effect, float Depth)
        {
            Image = this;
            batch.Draw(Image, Location, null, Color, rotation, Origin, Scale, effect, Depth);
        }
    }




    //containers

    public abstract class FrameContainer
    {
        public int CurrentFrame { get; set; }
        protected AnimationFrame[] frames; //clone????
        Vector2[] origins;

        protected FrameContainer() { }
        public FrameContainer(AnimationFrame[] frames, Vector2[] origins = null)
        {
            this.frames = frames;
            this.origins = origins;
            CurrentFrame = 0;
        }

        public abstract Texture2D Image { get; }

        public void Draw(SpriteBatch batch, Vector2 Location, Color Color, float rotation, Vector2 origin, float Scale, SpriteEffects effect, float Depth)
        {
            if (origins != null)
            {
                origin = origins[CurrentFrame];
            }
            frames[CurrentFrame].Draw(batch, Image, Location, Color, rotation, origin, Scale, effect, Depth);
        }
        public AnimationFrame Current
            => frames[CurrentFrame];
        public Vector2 CurrentOrigin(Vector2 origin)
            => origins == null ? origin : origins[CurrentFrame];
        public int Length
            => frames.Length;

        public void Restart(int frameSpeed)
        {
            CurrentFrame = frameSpeed > 0 ? 0 : Length - 1;
        }

        public bool OnLastFrame
        {
            get => CurrentFrame == Length - 1;
        }

        public abstract FrameContainer Clone();
        public void CloneLogic(FrameContainer copy)
        {
            copy.CurrentFrame = CurrentFrame;
            copy.frames = (AnimationFrame[])frames.Clone();
        }
    }

    public class RectangleContainer : FrameContainer
    {
        Texture2D image;
        private RectangleContainer() { }
        public RectangleContainer(Texture2D image, RectangleFrame[] frames, Vector2[] origins = null)
            : base(frames, origins)
        {
            this.image = image;
        }

        public override Texture2D Image => image;

        public override RectangleContainer Clone()
        {
            var copy = new RectangleContainer();
            CloneLogic(copy);
            return copy;
        }

        public void CloneLogic(RectangleContainer copy)
        {
            base.CloneLogic(copy);
            copy.image = image;
        }
    }
    public class TextureContainer : FrameContainer
    {
        private TextureContainer() { }
        public TextureContainer(TextureFrame[] frames, Vector2[] origins = null)
            : base(frames, origins) { }
        public override Texture2D Image => (TextureFrame)frames[CurrentFrame];

        public override TextureContainer Clone()
        {
            var copy = new TextureContainer();
            CloneLogic(copy);
            return copy;
        }
    }

    public abstract class AnimatingSpriteBase : SpriteBase
    {
        //public struct Animation
        //{
        //    List<Rectangle> frames;

        //    public Animation(List<Rectangle> list)
        //    {
        //        frames = list;
        //    }
        //}
        public FrameContainer Frames { get; set; }
        protected Vector2[] origins;
        public Timer FrameTime { get; protected set; }
        //    TimeSpan tick;
        protected bool autoLoop;
        protected bool autoInvert;

        int frameSpeed = 1;

        public void Restart()
        {
            Frames.Restart(frameSpeed);
        }
        public void InvertAnimation()
        {
            frameSpeed = -frameSpeed;
        }
        public bool LastFrame
        {
            get; private set;
        }

        public bool OnLastFrame
        {
            get => Frames.OnLastFrame;
        }

        public override Rectangle Hitbox
        {
            get
            {
                var origin = Frames.CurrentOrigin(Origin);

                return new Rectangle((int)(Location.X - origin.X * Scale), (int)(Location.Y - origin.Y * Scale),
                                                            (int)(Frames.Current.Dimensions.X * Scale), (int)(Frames.Current.Dimensions.Y * Scale));
            }
        }
        public AnimatingSpriteBase()
            : base() { }
        public AnimatingSpriteBase(Vector2 location, FrameContainer frames, int time, bool loop = true, bool invert = false, Vector2[] origins = null)
            : this(location, Color.White, Vector2.Zero, 1, frames, time, loop, invert, origins) { }
        public AnimatingSpriteBase(Vector2 location, Color color, Vector2 origin, float scale, FrameContainer frames, int time, bool loop, bool invert, Vector2[] origins = null)
            : this(location, color, 0, SpriteEffects.None, origin, scale, 1, frames, time, invert, loop, origins) { }
        public AnimatingSpriteBase(Vector2 location, Color color, float rotation, SpriteEffects effects, Vector2 origin, float scale, float depth, FrameContainer frames, int time, bool invert, bool loop, Vector2[] Origins = null)
            : base(frames.Image, location, color, rotation, effects, origin, scale, depth)
        {
            Frames = frames;
            FrameTime = new TimeSpan(0, 0, 0, 0, time);
            origins = Origins;
            autoLoop = loop;
            autoInvert = invert;

            if (origins != null && origins.Length < frames.Length)
            {
                origins = null;
            }
        }
        #region
        public void CloneLogic(AnimatingSpriteBase copy)
        {
            base.CloneLogic(copy);
            copy.Frames = Frames.Clone();
            copy.origins = origins;
            copy.FrameTime = FrameTime.Clone();
            copy.autoLoop = autoLoop;
            copy.autoInvert = autoInvert;
            copy.frameSpeed = frameSpeed;
            copy.LastFrame = LastFrame;
        }
        #endregion
        public void SetAnimatingSprite(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, Vector2 origin, float scale, float depth, FrameContainer frames, int time, Vector2[] Origins = null)
        {
            Frames = frames;
            FrameTime = new TimeSpan(0, 0, 0, 0, time);
            origins = Origins;


            if (origins != null && origins.Length < frames.Length)
            {
                origins = null;
            }

            Location = location;
            Color = color;
            Origin = origin;
            Scale = scale;
            Depth = depth;
            Effect = effects;
            this.Rotation = rotation;
            OriginalColor = color;
            oldScale = Scale;
            oldRotation = rotation;
            random = new Random();
            Image = image;

            offset = Vector2.Zero;
            moved = false;
            bigger = false;
            sizeSet = float.NaN;
            degreeSet = float.NaN;
            spotSet = new Vector2(float.NaN, float.NaN);

        }
        public void Animate(GameTime gametime)
        {
            LastFrame = false;
            FrameTime.Tick(gametime);
            if (FrameTime.Ready())
            {
                Frames.CurrentFrame += frameSpeed;
            }

            var tooBig = Frames.CurrentFrame >= Frames.Length;
            if (tooBig || Frames.CurrentFrame < 0)
            {
                if (autoInvert)
                {
                    InvertAnimation();
                }
                if (autoLoop)
                {
                    Restart();
                }
                else if (tooBig)
                {
                    Frames.CurrentFrame = Frames.Length - 1;
                }
                else
                {
                    Frames.CurrentFrame = 0;
                }
                LastFrame = true;
            }
        }
        public override void Update(GameTime gameTime)
        {
            Animate(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            Frames.Draw(batch, Location, Color, Rotation, Origin, Scale, Effect, Depth);
            // DrawHitBox(batch);
        }
    }

    public class AnimatingSprite : AnimatingSpriteBase, IGameObject<AnimatingSprite> {
        private AnimatingSprite() { }
        public AnimatingSprite(Vector2 location, FrameContainer frames, int time, bool loop = true, bool invert = false, Vector2[] origins = null)
            : this(location, Color.White, Vector2.Zero, 1, frames, time, loop, invert, origins) { }
        public AnimatingSprite(Vector2 location, Color color, Vector2 origin, float scale, FrameContainer frames, int time, bool loop, bool invert, Vector2[] origins = null)
            : this(location, color, 0, SpriteEffects.None, origin, scale, 1, frames, time, invert, loop, origins) { }
        public AnimatingSprite(Vector2 location, Color color, float rotation, SpriteEffects effects, Vector2 origin, float scale, float depth, FrameContainer frames, int time, bool invert, bool loop, Vector2[] origins = null)
            : base(location, color, rotation, effects, origin, scale, depth, frames, time, invert, loop, origins) { }
        public override AnimatingSprite Clone()
        {
            var newSprite = new AnimatingSprite();
            CloneLogic(newSprite);
            return newSprite;
        }
    }
}