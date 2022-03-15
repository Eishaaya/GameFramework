using BaseGameLibrary.Inputs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary
{
    public abstract class ButtonBase : SpriteBase
    {
        public CursorRoot.Info ChosenClick { get; set; }
        public Color NormalColor { get; set; }
        public Color HoverColor { get; set; }
        public Color ClickedColor { get; set; }
        //public bool Hold { get; set; } //<-
        public bool Held { get; set; } //<- Not a fan of this group, this data makes more sense in the mouse, or in another structure for a hotkey situation
        public bool PrevDown { get; set; }
        public ButtonBase(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effect, Vector2 origin, float scale, float depth, Color hovercolor, Color clickedcolor)
            : base(image, location, color, rotation, effect, origin, scale, depth)
        {
            HoverColor = hovercolor;
            ClickedColor = clickedcolor;
            NormalColor = color;
           // Hold = false;
            PrevDown = false;
        }

        #region clone
        protected void CloneLogic(ButtonBase copy)
        {
            base.CloneLogic(copy);
            copy.NormalColor = NormalColor;
            // copy.Hold = Hold;
            copy.Held = Held;
            copy.PrevDown = PrevDown;
        }
        public abstract override ButtonBase Clone();

        #endregion

        public virtual bool Check(CursorRoot cursor, int threshold = 2)
        {
            var click = (int)cursor.Clicked(this, ChosenClick);
            if (click > 0)
            {
                var good = click > threshold;
                if (good || (PrevDown && cursor.Held))
                {
                    Color = ClickedColor;
                    PrevDown = true;
                    return good;
                }
                PrevDown = cursor.Held;
                Color = HoverColor;
                return false;
            }
            Color = NormalColor;
            return false;
        }

        public virtual bool Check(Vector2 cursor, bool isclicked)
        {
            if (Hitbox.Contains(cursor))
            {
                if (isclicked || PrevDown)
                {
                    Color = ClickedColor;
                    //Hold = PrevDown;
                    PrevDown = true;
                    return isclicked;
                }
                Color = HoverColor;
                //Hold = false;
                PrevDown = false;
                return false;
            }
            else
            {
                if (!Held)
                {
                    Color = NormalColor;
                }
                else
                {
                    Color = ClickedColor;
                }
            }
            return false;
        }
    }

    public class Button : ButtonBase, IGameObject<Button>
    {
        public Button(Texture2D image, Vector2 location)
           : this(image, location, Vector2.Zero) { }
        public Button(Texture2D image, Vector2 location, Vector2 origin)
            : this(image, location, origin, 1) { }
        public Button(Texture2D image, Vector2 location, Vector2 origin, float scale)
            : this(image, location, Color.White, origin, scale, Color.DarkGray, Color.Gray) { }
        public Button(Texture2D image, Vector2 location, Color color, Vector2 origin, float scale, Color hoverColor, Color clickedColor)
            : this(image, location, color, 0, SpriteEffects.None, origin, scale, 1, hoverColor, clickedColor) { }
        public Button(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effect, Vector2 origin, float scale, float depth, Color hovercolor, Color clickedcolor)
            : base(image, location, color, rotation, effect, origin, scale, depth, hovercolor, clickedcolor) { }

        #region clone

        public override Button Clone()
        {
            var copy = new Button(Image, Location, Color, Rotation, Effect, Origin, Scale, Depth, HoverColor, ClickedColor);
            CloneLogic(copy);
            return copy;
        }

        #endregion
    }
}
