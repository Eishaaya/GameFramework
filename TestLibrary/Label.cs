using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary
{
    public class FadingLabel : Label
    {
        Timer lifeTimer;
        bool fading;

        public FadingLabel(SpriteFont font, Color color, Vector2 location, string text, int lifeTime, bool middle = false)
            : this(font, color, location, text, middle ? font.MeasureString(text) / 2 : Vector2.Zero, lifeTime) { }
        public FadingLabel(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, int lifeTime)
            : this(font, color, location, text, Origin, 1, lifeTime) { }
        public FadingLabel(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, float Scale, int lifeTime)
            : this(font, color, location, text, Origin, 0, SpriteEffects.None, Scale, 1, lifeTime) { }

        public FadingLabel(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, float Rotation, SpriteEffects Effect, float Scale, float Depth, int lifeTime)
        : base(font, color, location, text, Origin, Rotation, Effect, Scale, Depth)
        {
            lifeTimer = new Timer(lifeTime);
            fading = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!fading)
            {
                lifeTimer.Tick(gameTime);
                if (lifeTimer.Ready())
                {
                    fading = true;
                }
            }
            else
            {
                Fade();
            }
        }
    }
    public class Label : VisualObject
    {
        protected string text;
        public SpriteFont Font { get; }


        public Label(SpriteFont font, Color color, Vector2 location, string text, bool middle = false)
            : this(font, color, location, text, middle ? font.MeasureString(text) / 2 : Vector2.Zero) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin)
            : this(font, color, location, text, Origin, 1) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, float Scale)
            : this(font, color, location, text, Origin, 0, SpriteEffects.None, Scale, 1) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, float Rotation, SpriteEffects Effect, float Scale, float Depth)
        : base(location, color, Origin, Rotation, Effect, Scale, Depth)
        {
            this.text = text;
            Font = font;
        }


        public void Text(string text)
        {
            this.text = text;
        }
        public void Text(double number, int maxDigits = 0)
        {
            text = Math.Round(number, maxDigits).ToString();
        }

        public void Text(Vector2 coordinate, int maxDigits = 0)
        {
            text = $"({Math.Round(coordinate.X, maxDigits)}, {Math.Round(coordinate.Y, maxDigits)})";
        }

        public void Text(Rectangle box)
        {
            text = $"({box.X}, {box.Y}, {box.Width}, {box.Height})";
        }

        public string Text()
        {
            return text;
        }

        public static Label operator +(Label me, string newWord)
        {
            me.text += newWord;
            return me;
        }

        public void Print(SpriteBatch batch)
        {
            batch.DrawString(Font, text, Location + offset, Color, rotation, Origin, Scale, effect, Depth);
        }
    }
}
