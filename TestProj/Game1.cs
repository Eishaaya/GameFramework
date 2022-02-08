using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseGameLibrary;
using static BaseGameLibrary.Extensions;
using static BaseGameLibrary.Sequence;
using static BaseGameLibrary.VisualObject;
using System;

namespace TestProj
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Label test;
        //Timer timer;
        //int runner = 0;

        Indextionary<int, string> indextionary = new Indextionary<int, string>();
        Screenmanager manny;

        Screen screen;
        Screen horseyScreen;

        Sequence sequence;

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

            Screen testScreen = new Screen();

            //indextionary.Add(0, "hello");
            indextionary[0] = "amogus";

            test = new Label(Content.Load<SpriteFont>("File"), Color.Wheat, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), "Shid & fard", true);
           // timer = new Timer(5000);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sequence = new Sequence();
            sequence.AttachSequence(
                new ParamFunc<Label, ColorNums, bool>(FadeTo, test, ColorNums.Red),
                new ParamFunc<Label, Color, float, bool>(ChangeColor, test, Color.White, .1f),
                new ParamFunc<Label, ColorNums, bool>(FadeTo, test, ColorNums.Green),
                new ParamFunc<Label, int, float, bool, bool>(Pulsate, test, 150, .1f, false),
                new ParamFunc<Label, Color, float, bool>(ChangeColor, test, Color.White, .1f),
                new ParamFunc<Label, int, float, bool, bool>(Vibrate, test, 50, .1f, true),
                new ParamFunc<Sequence, int, bool>(Wait, sequence, 5000),
                new ParamFunc<Label, float, float, bool, bool>(Rotate, test, MathHelper.ToRadians(90), .1f, false),
                new ParamFunc<Label, Color, int, bool>(Fade, test, test.Color, 3));


            var eight = gIMMEeIGHT();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            sequence.RunSequence(gameTime);
        }

        int gIMMEeIGHT()
        {
            return 3 + 5;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            test.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
