using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    class Spaceship : Sprite, IFollowable, IMovable
    {
        public enum TurnState
        {
            None,
            Left,
            Right
        }

        #region properties

        public TurnState CurrentTurn { get; set; }
        public Vector2 Position { get; private set; }
        public float Health { get; set; }
        public MoveComponent Movement { get; set; }

        #endregion

        #region variables

        float rotationLimit;
        List<Projectile> shots;
        KeyboardState ks;
        Keys leftKey;
        Keys rightKey;
        Keys upKey;
        Keys downKey;
        Keys shootKey;
        LoopedFloat gunIndex;

        List<Vector2> gunSpots;
        List<Vector2> engineSpots;

        float shotDamage;
        float shotSpeed;
        float shotAcc;
        float shotScale;
        int shotLife;
        Texture2D shotImage;
        Timer reloadTime;
        Timer stallTime;
        List<AnimatingSprite> laserFlashes;
        Texture2D flashImage;
        RectangleFrame[] flashFrames;
        Vector2[] flashOrigins;
        float flashScale;
        int flashTime;
        ParticleEffect exhaust;
        Texture2D exhaustImage;
        float exhaustSpeed;
        int exhaustTime;
        Vector2 exhaustScale;
        Timer exhaustTimer;
        int exhaustChange;
        
        #endregion

        //ship
        #region constructor
        public Spaceship(Texture2D image, Vector2 location, Color color, float Engine, float health, float RotationLimit, float rotation, List<Vector2> GunSpots, List<Vector2> EngineSpots, int reload, int resetTime,
            //shots
            float sDamage, float sSpeed, float sAcc, float sScale, int sLife, Texture2D sImage,
            //flash
            Texture2D fImage, RectangleFrame[] fFrames, Vector2[] fOrigins, int fTime, float fScale,
            //Exhaust
            Texture2D eImage, float eSpeed, int eTime, int eChange, Vector2 eScale,
            //basic
            SpriteEffects effects, Vector2 origin, float scale, float depth,
            //defaults
            int defaultETime = 0,
            Keys LeftKey = Keys.A, Keys RightKey = Keys.D, Keys DownKey = Keys.S, Keys UpKey = Keys.W, Keys ShootKey = Keys.Space)

        : base(image, location, color, rotation, effects, origin, scale, depth)
        {
            Movement = new MoveComponent(Engine, .03f); //make this a var at some point
            exhaust = new ParticleEffect();
            gunIndex = new LoopedFloat(0, GunSpots.Count - 1, 0);
            Health = health;

            shotLife = sLife;
            shotDamage = sDamage;
            shotSpeed = sSpeed;
            shotImage = sImage;
            shotAcc = sAcc;
            shotScale = sScale;

            flashFrames = fFrames;
            flashImage = fImage;
            flashScale = fScale;
            flashTime = fTime;
            flashOrigins = fOrigins;
            laserFlashes = new List<AnimatingSprite>();

            exhaustImage = eImage;
            exhaustSpeed = eSpeed;
            exhaustTime = eTime;
            exhaustChange = eChange;
            exhaustScale = eScale;

            gunSpots = GunSpots;
            engineSpots = EngineSpots;
            Position = location;
            shots = new List<Projectile>();
            rotationLimit = RotationLimit;
            leftKey = LeftKey;
            rightKey = RightKey;
            upKey = UpKey;
            downKey = DownKey;
            shootKey = ShootKey;
            reloadTime = new Timer(reload);
            stallTime = new Timer(resetTime);

            if (defaultETime == 0)
            {
                exhaustTimer = new Timer(30);
            }
            else
            {
                exhaustTimer = new Timer(defaultETime);
            }
        }
        #endregion

        public void MakeExhaust()
        {
            foreach (Vector2 engineSpot in engineSpots)
            {
                var tempLocation = Vector2.Transform(engineSpot - Origin, Matrix.CreateRotationZ(rotation)) + Location;
                var newPart = ObjectPool<Particle>.Instance.Borrow<Particle>();
                newPart.SetParticle(exhaustImage, tempLocation, Color.Gold, Color.Red, rotation, effect, new Vector2(exhaustImage.Width / 2, exhaustImage.Height / 2), new Vector2((float)-Math.Sin(rotation) * exhaustSpeed, (float)Math.Cos(rotation) * exhaustSpeed) + new Vector2(0, Movement.Momentum.Y), exhaustTime, exhaustScale, 1, 1, 0, false, random.Next((int)-exhaustSpeed, (int)exhaustSpeed), (int)exhaustSpeed, exhaustChange);
                exhaust.AddParticle(newPart);
            }
        }
        public void Shoot()
        {
            var newShot = ObjectPool<Projectile>.Instance.Borrow<Laser>();
            var tempLocation = Vector2.Transform(gunSpots[(int)gunIndex] - Origin, Matrix.CreateRotationZ(rotation)) + Location;
            newShot.SetProjectile(shotImage, tempLocation, Color.White, rotation, shotDamage, shotSpeed, shotAcc, shotLife, effect, new Vector2(shotImage.Width / 2, shotImage.Height), shotScale, 1);
            shots.Add(newShot);

            var newFlash = ObjectPool<AnimatingSprite>.Instance.Borrow<AnimatingSprite>();
            newFlash.SetAnimatingSprite(flashImage, tempLocation, Color.White, rotation, effect, Rectangle.Empty, flashOrigins[0], flashScale, Depth, flashFrames, flashTime, flashOrigins);
            laserFlashes.Add(newFlash);
            gunIndex++;
        }

        protected void CheckKeys ()
        {
            ks = Keyboard.GetState();

            if (ks.IsKeyDown(shootKey))
            {
                if (reloadTime.Ready())
                {
                    Shoot();
                }
            }

            if (ks.IsKeyDown(upKey))
            {
                Movement.CurrentState = MoveComponent.MoveState.Forwards;
            }
            else if (ks.IsKeyDown(downKey))
            {
                Movement.CurrentState = MoveComponent.MoveState.Backwards;
            }
            else
            {
                Movement.CurrentState = MoveComponent.MoveState.Stalled;
            }

            if (ks.IsKeyDown(leftKey))
            {
                rotation = MathHelper.Lerp(rotation, -rotationLimit, .05f);
                CurrentTurn = TurnState.Left;
                stallTime.Reset();
            }
            else if (ks.IsKeyDown(rightKey))
            {
                rotation = MathHelper.Lerp(rotation, rotationLimit, .05f);
                CurrentTurn = TurnState.Right;
                stallTime.Reset();
            }
            else
            {
                rotation = MathHelper.Lerp(rotation, 0, .1f);
                CurrentTurn = TurnState.None;
            }
        }
        protected void UpdateExhaust (GameTime gameTime)

        {
            exhaustTimer.Tick(gameTime);
            if (exhaustTimer.Ready())
            {
                MakeExhaust();
            }
            exhaust.Update(gameTime);
        }
        protected void MoveRemoveShots(GameTime gameTime)
        {
            reloadTime.Tick(gameTime);
            var deadPews = new List<Laser>();
            foreach (Laser pewpew in shots)
            {
                pewpew.move(gameTime);
                if (pewpew.LifeTimer.Ready())
                {
                    ObjectPool<Projectile>.Instance.Return<Laser>(pewpew);
                    deadPews.Add(pewpew);
                }
            }
            foreach (Laser pooPew in deadPews)
            {
                shots.Remove(pooPew);
            }
        }
        protected void FadeUpdateFlashes(GameTime gameTime)
        {
            var DoneFrames = new List<AnimatingSprite>();
            foreach (AnimatingSprite flash in laserFlashes)
            {
                if (flash.OnLastFrame)
                {
                    DoneFrames.Add(flash);
                }
                else
                {
                    flash.Animate(gameTime);
                }
            }
            foreach (AnimatingSprite doneFlash in DoneFrames)
            {
                if (doneFlash.Fade(60))
                {
                    laserFlashes.Remove(doneFlash);
                    ObjectPool<AnimatingSprite>.Instance.Return<AnimatingSprite>(doneFlash);
                }
            }
        }
        protected void MoveAndMoveCam(GameTime gameTime)
        {
            if (Movement.CurrentState == MoveComponent.MoveState.Stalled)
            {
                if (CurrentTurn == TurnState.None && Location != Position)
                {
                    stallTime.Tick(gameTime);
                    if (stallTime.Ready(false))
                    {
                        Position = Vector2.Lerp(Position, new Vector2(Position.X, Location.Y), .01f);
                    }
                    Movement.Force = Vector2.Zero;
                }
                else
                {
                    Movement.Force = new Vector2(Movement.Speed * (float)Math.Sin((double)rotation), 0);
                }
            }
            else
            {
                Movement.SetSpeed(rotation);
                stallTime.Reset();
            }
            Movement.Update();
            Location += Movement.Momentum;
        }



        public void Update(GameTime gameTime)
        {
            CheckKeys();

            UpdateExhaust(gameTime);

            MoveRemoveShots(gameTime);

            FadeUpdateFlashes(gameTime);

            MoveAndMoveCam(gameTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            exhaust.Draw(batch);
            base.Draw(batch);
            foreach (Laser pewpew in shots)
            {
                pewpew.Draw(batch);
            }
            foreach (AnimatingSprite sprite in laserFlashes)
            {
                sprite.Draw(batch);
            }
        }
    }
}
