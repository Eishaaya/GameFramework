using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    abstract class Projectile : SpriteBase, IPoolable
    {
        public float Damage { get; set; }
        float acceleration;
        Vector2 speed;
        public Timer LifeTimer { get; set; }

        public Projectile(Texture2D image, Vector2 location, Color color,float rotation, float sDamage, float sSpeed, float sAcc, int lifeTime, SpriteEffects effects, Vector2 origin, float scale, float depth)
        : base(image, location, color, rotation, effects, origin, scale, depth)
        {
            speed = new Vector2(sSpeed * (float)Math.Sin((double)rotation), sSpeed * (float)Math.Cos((double)rotation));
            Damage = sDamage;
            acceleration = sAcc;
            LifeTimer = new Timer(lifeTime);
        }
        public void SetProjectile (Texture2D image, Vector2 location, Color color, float rotation, float sDamage, float sSpeed, float sAcc, int lifeTime, SpriteEffects effects, Vector2 origin, float scale, float depth)
        {
            Image = image;
            Location = location;
            Color = color;
            this.Rotation = rotation;
            Damage = sDamage;
            speed = new Vector2(sSpeed * (float)Math.Sin((double)rotation), sSpeed * -(float)Math.Cos((double)rotation));
            acceleration = sAcc;
            Effect = effects;
            Origin = origin;
            Scale = scale;
            Depth = depth;
            LifeTimer = new Timer(lifeTime);
        }

        public virtual void move(GameTime gameTime)
        {
            Location += speed;
            speed *= acceleration;
            LifeTimer.Tick(gameTime);
        }
    }

    class Laser : Projectile
    {
        public enum LaserTypes
        {
            FighterLaser,
            PowerLaser,
            WeakLaser
        };

        public LaserTypes LaserType; 
        public Laser(Texture2D image, Vector2 location, Color color, float rotation, float sDamage, float sSpeed, float sAcc, int lifeTime, SpriteEffects effects, Vector2 origin, float scale, float depth)
        : base (image, location, color, rotation, sDamage, sSpeed, sAcc, lifeTime, effects, origin, scale, depth)
        {

        }
        public override void move(GameTime gameTime)
        {
            base.move(gameTime);
        }

        public override VisualObject Clone()
        {
            throw new NotImplementedException();
        }
    }
}
