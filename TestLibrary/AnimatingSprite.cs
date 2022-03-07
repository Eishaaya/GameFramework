using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary
{
    public abstract class AnimationFrame
    {
        public abstract void Draw(SpriteBatch batch, Texture2D Image, Vector2 Location, Color Color, float rotation, Vector2[] origins, Vector2 Origin, float Scale, SpriteEffects effect, float Depth, int currentframe);
    }

    public class RectangleFrame : AnimationFrame
    {
        RectangleFrame(Rectangle rectangle)
        {
            rect = rectangle;
        }
        Rectangle rect;

        public static implicit operator Rectangle(RectangleFrame frame) => frame.rect;
        public static implicit operator RectangleFrame(Rectangle frame) => new RectangleFrame(frame);

        public override void Draw(SpriteBatch batch, Texture2D Image, Vector2 Location, Color Color, float rotation, Vector2[] origins, Vector2 Origin, float Scale, SpriteEffects effect, float Depth, int currentframe)
        {
            batch.Draw(Image, Location, this, Color, rotation, origins == null ? Origin : origins[currentframe], Scale, effect, Depth);
        }
    }

    public class TextureFrame : AnimationFrame
    {
        TextureFrame(Texture2D txtr)
        {
            texture = txtr;
        }
        Texture2D texture;

        public static implicit operator Texture2D(TextureFrame frame) => frame.texture;
        public static implicit operator TextureFrame(Texture2D frame) => new TextureFrame(frame);

        public override void Draw(SpriteBatch batch, Texture2D Image, Vector2 Location, Color Color, float rotation, Vector2[] origins, Vector2 Origin, float Scale, SpriteEffects effect, float Depth, int currentframe)
        {
            Image = this;
            batch.Draw(Image, Location, null, Color, rotation, origins == null ? Origin : origins[currentframe], Scale, effect, Depth);
        }
    }


    public class AnimatingSprite : Sprite, IGameObject<AnimatingSprite>
    {
        //public struct Animation
        //{
        //    List<Rectangle> frames;

        //    public Animation(List<Rectangle> list)
        //    {
        //        frames = list;
        //    }
        //}
        public AnimationFrame[] Frames { get; set; }
        Vector2[] origins;
        public Timer FrameTime { get; protected set; }
        //    TimeSpan tick;
        public int CurrentFrame { get; private set; }
        bool autoLoop;
        bool autoInvert;

        int frameSpeed = 1;

        public void Restart()
        {
            CurrentFrame = frameSpeed > 0? 0 : Frames.Length - 1;
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
            get => CurrentFrame == Frames.Length - 1;
        }

        public AnimatingSprite(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, Vector2 origin, float scale, float depth, AnimationFrame[] frames, bool loop, bool invert, int time, Vector2[] Origins = null)
            : base(image, location, color, rotation, effects, origin, scale, depth)
        {
            Frames = frames;
            FrameTime = new TimeSpan(0, 0, 0, 0, time);
            origins = Origins;
            CurrentFrame = 0;
            autoLoop = loop;
            autoInvert = invert;

            if (origins != null && origins.Length < frames.Length)
            {
                origins = null;
            }

        }
        public void SetAnimatingSprite(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, Rectangle hitbox, Vector2 origin, float scale, float depth, AnimationFrame[] frames, int time, Vector2[] Origins = null)
        {
            Frames = frames;
            FrameTime = new TimeSpan(0, 0, 0, 0, time);
            origins = Origins;
            CurrentFrame = 0;


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
                CurrentFrame += frameSpeed;
            }

            var tooBig = CurrentFrame >= Frames.Length;
            if (tooBig || CurrentFrame < 0)
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
                    CurrentFrame = Frames.Length - 1;
                }
                else
                {
                    CurrentFrame = 0;
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
            Frames[CurrentFrame].Draw(batch, Image, Location, Color, Rotation, origins, Origin, Scale, Effect, Depth, CurrentFrame);
        }

        AnimatingSprite ICopyable<AnimatingSprite>.Clone()
        {
            var newSprite = new AnimatingSprite(Image, Location, Color, Rotation, Effect, Origin, Scale, Depth, Frames, autoLoop, autoInvert, FrameTime.TotalMillies, origins);
            newSprite.CurrentFrame = CurrentFrame;
            return newSprite;
        }
    }
}