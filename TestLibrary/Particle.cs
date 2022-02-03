using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    class Particle : ScalableSprite
    {
        public bool going;
        public Vector2 speed { get; set; }
        Timer fadeTime;
        Timer startTime;
        public bool faded;
        bool noScale;
        float maxScale;
        float rotationSpeed;
        Color newColor;
        int fadeSpeed;
        int changeSpeed;

        public Particle(Texture2D image, Vector2 location, Color color, Color nColor, float rotation, SpriteEffects effects, Vector2 origin, Vector2 Speed, int time, Vector2 scale, float depth = 1, float plebScale = 1, int timeTillStart = 0, bool willScale = true, float rSpeed = 0, int fadespeed = 3, int changespeed = 3, float mScale = float.PositiveInfinity)
            : this(image, location, color, rotation, effects, origin, Speed, time, scale, depth, plebScale, timeTillStart, willScale, rSpeed, fadespeed, changespeed, mScale)
        {
            newColor = nColor;
        }
        public Particle(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, Vector2 origin, Vector2 Speed, int time, Vector2 scale, float depth = 1, float plebScale = 1, int timeTillStart = 0, bool willScale = true, float rSpeed = 0, int fadespeed = 3, int changespeed = 3, float mScale = float.PositiveInfinity)
            : base(image, location, color, rotation, effects, origin, scale, depth, plebScale)
        {
            changeSpeed = changespeed;
            newColor = Color;
            fadeSpeed = fadespeed;
            noScale = !willScale;
            maxScale = mScale;
            speed = Speed;
            rotationSpeed = rSpeed;
            fadeTime = new Timer(time);
            startTime = new Timer(timeTillStart);
            if (timeTillStart == 0)
            {
                going = true;
            }
            else
            {
                going = false;
            }
            if (!noScale)
            {
                Scale = Scale / 50;
            }
        }

        public void SetParticle(Texture2D image, Vector2 location, Color color, Color nColor, float rotation, SpriteEffects effects, Vector2 origin, Vector2 Speed, int time, Vector2 scale, float depth = 1, float plebScale = 1, int timeTillStart = 0, bool willScale = true, float rSpeed = 0, int fadespeed = 3, int changespeed = 3, float mScale = float.PositiveInfinity)
        {
            SetParticle(image, location, color, rotation, effects, origin, Speed, time, scale, depth, plebScale, timeTillStart, willScale, rSpeed, fadespeed, changespeed, mScale);
            newColor = nColor;
        }

        public void SetParticle(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, Vector2 origin, Vector2 Speed, int time, Vector2 scale, float depth = 1, float plebScale = 1, int timeTillStart = 0, bool willScale = true, float rSpeed = 0, int fadespeed = 3, int changespeed = 3, float mScale = float.PositiveInfinity)
        {
            changeSpeed = changespeed;
            fadeSpeed = fadespeed;
            newColor = color;
            Image = image;
            Location = location;
            rotationSpeed = rSpeed;
            maxScale = mScale;
            Color = color;
            OriginalColor = color;
            this.Rotation = rotation;
            Effect = effects;
            Origin = origin;
            noScale = !willScale;
            Scale2D = scale;
            Depth = depth;
            Scale = plebScale;
            speed = Speed;
            fadeTime = new Timer(time);
            startTime = new Timer(timeTillStart);
            if (timeTillStart == 0)
            {
                going = true;
            }
            else
            {
                going = false;
            }
            if (!noScale)
            {
                Scale = Scale / 50;
            }
        }
        public override void Update(GameTime gameTime)
        {
            Rotation += rotationSpeed;
            if (!going)
            {
                startTime.Tick(gameTime);
                if (startTime.Ready())
                {
                    going = true;
                }
            }
            else
            {
                fadeTime.Tick(gameTime);
                if (!noScale && Scale2D.X < maxScale && Scale2D.Y < maxScale)
                {
                    Scale2D += new Vector2(Math.Abs(speed.X), Math.Abs(speed.Y)) * Scale;
                }
                Location += speed;
                if (fadeTime.Ready(false))
                {
                    if (Fade(Color, fadeSpeed))
                    {
                        faded = true;
                    }
                    if (fadeTime.GetMillies() < 0)
                    {
                        for (int i = 0; i < -fadeTime.GetMillies(); i++)
                        {
                            Fade();
                        }
                    }
                }
                else
                {
                    this.ChangeColor(newColor, .1f + (float)(changeSpeed / 100));
                }
            }
        }
    }
}
