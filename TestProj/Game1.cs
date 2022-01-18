using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseGameLibrary;
using static BaseGameLibrary.Extensions;
using static BaseGameLibrary.VisualObject;

namespace TestProj
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Label test;
        Timer timer;
        int runner = 0;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            test = new Label(Content.Load<SpriteFont>("File"), Color.Wheat, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), "Shid & fard", true);
            timer = new Timer(5000);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            RunSequence(ref runner,
                new ParamFunc<Label, ColorNums, bool>(FadeTo, test, ColorNums.Red),
                new ParamFunc<Label, Color, float, bool>(ChangeColor, test, Color.White, .1f),
                new ParamFunc<Label, ColorNums, bool>(FadeTo, test, ColorNums.Green),
                new ParamFunc<Label, int, float, bool, bool>(Pulsate, test, 150, .1f, false),
                new ParamFunc<Label, Color, float, bool>(ChangeColor, test, Color.White, .1f),
                new ParamFunc<Label, int, float, bool, bool>(Vibrate, test, 50, .1f, true),
                new ParamFunc<Timer, GameTime, bool>(Wait, timer, gameTime),
                new ParamFunc<Label, float, float, bool, bool>(Rotate, test, MathHelper.ToRadians(90), .1f, false),
                new ParamFunc<Label, Color, int, bool>(Fade, test, test.Color, 3));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            test.Print(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
