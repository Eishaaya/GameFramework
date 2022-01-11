using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary
{
    public class Label : VisualObject
    {
        string text;
        public SpriteFont Font { get; }
        TimeSpan time;
        TimeSpan tick;


        public Label(SpriteFont font, Color color, Vector2 location, string text)
            : this(font, color, location, text, TimeSpan.Zero) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, TimeSpan lifetime)
            : this(font, color, location, text, lifetime, new Vector2(0, 0), 0, SpriteEffects.None, 1, 1) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, TimeSpan lifetime, float Scale)
            : this(font, color, location, text, lifetime, new Vector2(0, 0), 0, SpriteEffects.None, Scale, 1) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, TimeSpan lifetime, Vector2 Origin, float Rotation, SpriteEffects Effect, float Scale, float Depth)
        : base (location, color, Origin, Rotation, Effect, Scale, Depth)
        {
            this.text = text;
            Font = font;
            time = lifetime;            
        }
        public void Update(GameTime timetick)
        {
            tick += timetick.ElapsedGameTime;
            if (tick > time)
            {
                Fade();
            }
        }

        public void Text (string text)
        {
            this.text = text;
        }
        public void Text (double number, int maxDigits = 0)
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

        public static Label operator + (Label me, string newWord)          
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
