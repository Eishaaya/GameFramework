using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary.Visual
{
    public abstract class VisualObject : IGameObject<VisualObject>
    {
        public enum ColorNums
        {
            Red,
            Green,
            Blue,
            Black,
        };
        //ColorChange variables
        protected float changeFactor = 0;
        protected float totalColorDistance = -1;

        //Vibrate variables
        protected bool moved;
        protected Vector2 spotSet;
        protected Vector2 offset;

        //Pulsate variables
        protected bool bigger;
        protected float sizeSet;
        protected float oldScale;

        //Rotate variables
        protected bool rotated;
        protected float degreeSet;
        protected float oldRotation;
       

        //Standard properties   
        public Color Color { get; set; }
        public Vector2 Location { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public float Depth { get; set; }
        public SpriteEffects Effect { get; set; }
        public Vector2 Origin { get; set; }
        public Color OriginalColor { get; set; }
        public bool Visible { get; set; }

        public abstract Rectangle Hitbox { get; }

        protected VisualObject() { }
        public VisualObject(Vector2 location, Color color, Vector2 origin, float Rotation, SpriteEffects Effect, float scale, float depth, bool visible = true)
        {
            Location = location;
            Color = color;
            Origin = origin;
            Scale = scale;
            Depth = depth;
            this.Effect = Effect;
            this.Rotation = Rotation;
            OriginalColor = color;
            oldScale = Scale;
            oldRotation = this.Rotation;            

            offset = Vector2.Zero;
            moved = false;
            bigger = false;
            sizeSet = float.NaN;
            degreeSet = float.NaN;
            spotSet = new Vector2(float.NaN, float.NaN);
            Visible = visible;
        }

        #region clone

        ///<summary>
        ///Copies over all the internal data over to the clone
        ///</summary>
        protected void CloneLogic(VisualObject copy)
        {
            copy.Location = Location;
            copy.Color = Color;
            copy.Effect = Effect;
            copy.Origin = Origin;
            copy.Rotation = Rotation;
            copy.Depth = Depth;
            copy.Scale = Scale;
            copy.Visible = Visible;
            copy.bigger = bigger;
            copy.rotated = rotated;
            copy.spotSet = spotSet;
            copy.sizeSet = sizeSet;
            copy.degreeSet = degreeSet;
            copy.offset = offset;
            copy.oldScale = oldScale;
            copy.OriginalColor = OriginalColor;
            copy.oldRotation = oldRotation;

            copy.changeFactor = changeFactor;
            copy.totalColorDistance = totalColorDistance;
            copy.moved = moved;
        }
        ///<summary>
        ///Returns a shallow copy of the Visual Object
        ///</summary>
        public abstract VisualObject Clone();

        #endregion

        #region visualFunctions

        ///<summary>
        ///Vibrates the object between a point and its original position
        ///</summary>
        ///<param name="distance">How far away the VisualObject vibrates to</param>
        ///<param name="sped">How fast the VisualObject moves</param>
        ///<param name="rando">Determines if the point the VisualObject vibrates to is Random.Sharedly generated</param>

        public bool Vibrate(int distance, float sped, bool rando = true)
        {
            if (float.IsNaN(spotSet.X))
            {
                if (rando)
                {
                    spotSet = new Vector2(Random.Shared.Next(-distance, distance), Random.Shared.Next(-distance, distance));
                }
                else
                {
                    spotSet = new Vector2(distance, distance);
                }
                offset = Vector2.Zero;
            }
            if (!moved)
            {
                offset = Vector2.Lerp(offset, spotSet, sped);
                if (Vector2.Distance(offset, spotSet) <= .5f)
                {
                    offset = spotSet;
                    moved = true;
                }
            }
            else
            {
                offset = Vector2.Lerp(offset, Vector2.Zero, sped);
                if (Vector2.Distance(offset, Vector2.Zero) <= .5f)
                {
                    spotSet = new Vector2(float.NaN, float.NaN);
                    moved = false;
                    offset = Vector2.Zero;
                    return true;
                }
            }
            return false;
        }
        ///<summary>
        ///Pulsates the VisualObject between a point and its original position
        ///</summary>
        ///<param name="size">How large the VisualObject grows/shrinks to</param>
        ///<param name="sped">How fast the VisualObject scales</param>
        ///<param name="rando">Determines if the size the VisualObject scales to is Random.Sharedly generated</param>
        public bool Pulsate(int size, float sped, bool rando = true)
        {
            if (float.IsNaN(sizeSet))
            {
                if (rando)
                {
                    sizeSet = Random.Shared.Next(size) + oldScale * 100;
                }
                else
                {
                    sizeSet = size;
                }
                sizeSet /= 100;
            }
            if (!bigger)
            {
                var temp = MathHelper.Lerp(Scale, sizeSet, sped);
                if (MathHelper.Distance(temp, sizeSet) <= .01f)
                {
                    Scale = sizeSet;
                    bigger = true;
                }
                else
                {
                    Scale = temp;
                }
            }
            else
            {
                var temp = MathHelper.Lerp(Scale, oldScale, sped);
                if (MathHelper.Distance(temp, oldScale) <= .01f)
                {
                    sizeSet = float.NaN;
                    Scale = oldScale;
                    bigger = false;
                    return true;
                }
                else
                {
                    Scale = temp;                   
                }
            }
            return false;
        }
        public bool Rotate(float target, float sped, bool rando = true)
        {
            if (float.IsNaN(degreeSet))
            {
                if (rando)
                {
                    degreeSet = Random.Shared.Next((int)-target * 1000, (int)target * 1000);
                    degreeSet /= 1000;
                }
                else
                {
                    degreeSet = target;
                }
            }
            if (rotated)
            {
                var temp = MathHelper.Lerp(Rotation, oldRotation, sped);
                if (MathHelper.Distance(temp, oldRotation) <= .01f)
                {
                    Rotation = oldRotation;
                    rotated = false;
                    return true;
                }
                else
                {
                    Rotation = temp;
                }
            }
            else
            {
                var temp = MathHelper.Lerp(Rotation, degreeSet, sped);
                if (MathHelper.Distance(temp, degreeSet) <= .01f)
                {
                    degreeSet = float.NaN;
                    Rotation = temp;
                    rotated = true;
                }
                else
                {
                    Rotation = temp;
                }
            }
            return false;
        }
        public bool ChangeColor(Color newColor, float sped = .1f)
        {
            if (totalColorDistance == -1)
            {
                totalColorDistance = Vector4.Distance(Color.ToVector4(), newColor.ToVector4());
            }
            var temp = Color;
            changeFactor += sped;
            Color =                                                 // new Color(Vector4.LerpPrecise(Color.ToVector4(), newColor.ToVector4(), changeFactor);
                    Color.FromNonPremultiplied(
            (int)MathHelper.LerpPrecise(Color.R, newColor.R, changeFactor),
            (int)MathHelper.LerpPrecise(Color.G, newColor.G, changeFactor),
            (int)MathHelper.LerpPrecise(Color.B, newColor.B, changeFactor),
            (int)MathHelper.LerpPrecise(Color.A, newColor.A, changeFactor));


            if (temp != Color)
            {
                var DistanceLeft = Vector4.Distance(Color.ToVector4(), newColor.ToVector4());
                if (Vector4.Distance(temp.ToVector4(), newColor.ToVector4()) > DistanceLeft)
                {
                    changeFactor = 0;
                }
                else
                {
                    Color = temp;
                    changeFactor *= 30 - DistanceLeft / totalColorDistance * 30;
                }
            }
            if (newColor == Color)
            {
                totalColorDistance = -1;
                return true;                
            }
            return false;
        }
        public bool Fade(int speed = 3)
        {
            return Fade(OriginalColor, speed);
        }
        public bool Fade(Color tint, int speed = 3)
        {
            if (Color.A <= 0)
            {
                Color = tint;
                Color = Color.FromNonPremultiplied(tint.R, tint.G, tint.B, 0);
                return true;
            }
            Color = Color.FromNonPremultiplied(tint.R, tint.G, tint.B, Color.A - speed);
            return false;
        }
        public bool FadeTo(ColorNums colorChoice, int fadeSpeed = 3)
        {
            var tint = Color;
            #region old garbage
            //if (cChoice == ColorNums)
            //{
            //    //red?
            //    tint = Color.FromNonPremultiplied(Color.A, Color.G - Color.A, Color.B - Color.A, Color.A);
            //}
            //else if (cChoice == 2)
            //{
            //    //green?
            //    tint = Color.FromNonPremultiplied(Color.R - Color.A, Color.A, Color.B - Color.A, Color.A);
            //}
            //else if (cChoice == 3)
            //{
            //    //blue?
            //    tint = Color.FromNonPremultiplied(Color.R - Color.A, Color.G - Color.A, Color.A, Color.A);
            //}
            #endregion
            if (colorChoice != ColorNums.Black)
            {
                byte[] colorBytes = new byte[4];

                for (int i = 0; i < 4; i++)
                {
                    colorBytes[i] = (byte)(tint.PackedValue >> i * 8);
                }

                for (int i = 0; i < 3; i++)
                {
                    colorBytes[i] = (byte)Math.Max(colorBytes[i] - tint.A, 0);
                }
                colorBytes[(int)colorChoice] = tint.A;

                uint newColor = 0;
                for (int i = 0; i < 4; i++)
                {
                    newColor += (uint)colorBytes[i] << i * 8;
                }

                tint = new Color(newColor);                
            }

            return Fade(tint, fadeSpeed);
        }
        public bool Fill()
        {
            return Fill(OriginalColor);
        }
        public bool Fill(Color tint)
        {
            Color = Color.FromNonPremultiplied(tint.R, tint.G, tint.B, Color.A + 3);
            if (Color.A >= 255)
            {
                Color = Color.FromNonPremultiplied(tint.R, tint.G, tint.B, 255);
                return true;
            }
            return false;
        }
        ///<summary>
        ///Updates the VisualObject
        ///</summary>
        public abstract void Update(GameTime gameTime);
        ///<summary>
        ///Draws the visual object to the screen
        ///</summary>
        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Die();
        
        #endregion
    }
}
