using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace BaseGameLibrary.Visual
{
    public sealed class FadingLabel : LabelBase, IGameObject<FadingLabel>
    {
        Timer lifeTimer;
        bool fading;

        private FadingLabel() { }
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

        public override FadingLabel Clone()
        {
            var copy = new FadingLabel();
            CloneLogic(copy);
            return copy;
        }

        public void CloneLogic(FadingLabel copy)
        {
            copy.lifeTimer = lifeTimer;
            copy.fading = fading;
            base.CloneLogic(copy);
        }

        public override void Update(GameTime gameTime)
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

        public static FadingLabel operator +(FadingLabel me, string newWord)
            => (FadingLabel)me.Add(newWord);
        public static FadingLabel operator +(FadingLabel me, LabelBase newWord)
            => (FadingLabel)me.Add(newWord);
    }
    public abstract class LabelBase : VisualObject
    {
        protected string text;
        public SpriteFont Font { get; private set; }

        protected LabelBase() { }
        public LabelBase(SpriteFont font, Color color, Vector2 location, string text, bool middle = false)
            : this(font, color, location, text, middle ? font.MeasureString(text) / 2 : Vector2.Zero) { }
        public LabelBase(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin)
            : this(font, color, location, text, Origin, 1) { }
        public LabelBase(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, float Scale)
            : this(font, color, location, text, Origin, 0, SpriteEffects.None, Scale, 1) { }
        public LabelBase(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, float Rotation, SpriteEffects Effect, float Scale, float Depth)
        : base(location, color, Origin, Rotation, Effect, Scale, Depth)
        {
            this.text = text;
            Font = font;
        }

        public override abstract LabelBase Clone();

        public void CloneLogic(LabelBase copy)
        {
            copy.text = text;
            copy.Font = Font;
        }

        public virtual void Text(string text)
        {
            this.text = text;
        }
        public void Text(double number, int maxDigits = 0)
        {
            Text(Math.Round(number, maxDigits).ToString());
        }

        public void Text(Vector2 coordinate, int maxDigits = 0)
        {
            Text($"({Math.Round(coordinate.X, maxDigits)}, {Math.Round(coordinate.Y, maxDigits)})");
        }

        public void Text(Rectangle box)
        {
            Text($"({box.X}, {box.Y}, {box.Width}, {box.Height})");
        }

        public void Text(Keys key)
        {
            Text($"{key}");
        }

        public string Text()
        {
            return text;
        }

        public virtual string ActualText()
        {
            return text;
        }

        public virtual LabelBase Add(string newWord)
        {
            text += newWord;
            return this;
        }
        public LabelBase Add(LabelBase newWord)
            => Add(newWord.Text());

        public static LabelBase operator +(LabelBase me, string newWord)
            => me.Add(newWord);
        public static LabelBase operator +(LabelBase me, LabelBase newWord)
            => me.Add(newWord);

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch batch)
        {
            if (!Visible) return;
            batch.DrawString(Font, text, Location + offset, Color, Rotation, Origin, Scale, Effect, Depth);
        }

    }

    public sealed class Label : LabelBase, IGameObject<Label>
    {
        private Label() { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, bool middle = false)
            : this(font, color, location, text, middle ? font.MeasureString(text) / 2 : Vector2.Zero) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin)
            : this(font, color, location, text, Origin, 1) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, float Scale)
            : this(font, color, location, text, Origin, 0, SpriteEffects.None, Scale, 1) { }
        public Label(SpriteFont font, Color color, Vector2 location, string text, Vector2 Origin, float Rotation, SpriteEffects Effect, float Scale, float Depth)
            : base(font, color, location, text, Origin, Rotation, Effect, Scale, Depth) { }

        public override Label Clone()
        {
            var copy = new Label();
            CloneLogic(copy);
            return copy;
        }

        public static Label operator +(Label me, string newWord)
            => (Label)me.Add(newWord);
        public static Label operator +(Label me, LabelBase newWord)
            => (Label)me.Add(newWord);
    }

    public abstract class ValueLabelBase : LabelBase, IGameObject<ValueLabelBase>
    {
        string infoText;

        protected ValueLabelBase() { }
        public ValueLabelBase(SpriteFont font, Color color, Vector2 location, string text, string infoText, Vector2 Origin, float Rotation, SpriteEffects Effect, float Scale, float Depth)
        : base(font, color, location, text, Origin, Rotation, Effect, Scale, Depth)
        {
            this.infoText = infoText;
        }

        public override void Text(string newText)
        {
            text = infoText + newText;
        }
        public override string ActualText()
        {
            var returnString = "";
            for (int i = infoText.Length; i < text.Length; i++)
            {
                returnString += text[i];
            }
            return returnString;
        }

        public abstract override ValueLabelBase Clone();
        
        public void CloneLogic(ValueLabelBase copy)
        {
            copy.infoText = infoText;
            base.CloneLogic(copy);
        }

        public static ValueLabelBase operator +(ValueLabelBase me, string newWord)
            => (ValueLabelBase)me.Add(newWord);
        public static ValueLabelBase operator +(ValueLabelBase me, LabelBase newWord)
            => (ValueLabelBase)me.Add(newWord);
    }

    public sealed class ValueLabel : ValueLabelBase
    {
        private ValueLabel() { }
        public ValueLabel(SpriteFont font, Color color, Vector2 location, string text, string infoText, bool middle = false)
            : this(font, color, location, text, infoText, middle ? font.MeasureString(text) / 2 : Vector2.Zero) { }
        public ValueLabel(SpriteFont font, Color color, Vector2 location, string text, string infoText, Vector2 Origin)
            : this(font, color, location, text, infoText, Origin, 1) { }
        public ValueLabel(SpriteFont font, Color color, Vector2 location, string text, string infoText, Vector2 Origin, float Scale)
            : this(font, color, location, text, infoText, Origin, 0, SpriteEffects.None, Scale, 1) { }
        public ValueLabel(SpriteFont font, Color color, Vector2 location, string text, string infoText, Vector2 Origin, float Rotation, SpriteEffects Effect, float Scale, float Depth)
            : base(font, color, location, text, infoText, Origin, Rotation, Effect, Scale, Depth) { }

        public override ValueLabel Clone()
        {
            var copy = new ValueLabel();
            CloneLogic(copy);
            Type billy = typeof(float);
            return copy;
        }
    }

    public class LabelParser<T> : IGameObject<LabelParser<T>>
    {
        public LabelBase Label { get; private set; }

        public ParserBase<T> Parser { get; private set; }
        string oldText;

        private LabelParser() { }
        public LabelParser(Label label, ParserBase<T> parser)
        {
            Label = label;            
            Parser = parser;
            oldText = parser.value.ToString();
            if (Parser.Parse(label.ActualText()))
            {
                oldText = label.ActualText();
            }
            label.Text(oldText);
        }

        #region clone

        public LabelParser<T> Clone()
        {
            var copy = new LabelParser<T>();
            CloneLogic(copy);
            return copy;
        }

        public void CloneLogic(LabelParser<T> copy)
        {
            copy.Parser = Parser.Clone();
            copy.Label = Label.Clone();
            copy.oldText = (string)oldText.Clone();
        }

        #endregion

        public void Parse()
        {
            var text = Label.ActualText();
            if (Parser.Parse(text))
            {
                oldText = text;
            }
            Revert();
        }

        public void Revert()
        {
            Label.Text(oldText);
        }

        public void Update(GameTime gameTime)
        {
            Label.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Label.Draw(spriteBatch);
        }

        
    }
}
