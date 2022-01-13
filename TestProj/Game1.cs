using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseGameLibrary;
using static BaseGameLibrary.Extensions;

namespace TestProj
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Label test;
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
            test = new Label(Content.Load<SpriteFont>("File"), Color.Wheat, Vector2.Zero, "Shid & fard");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            RunSequence(ref runner,
                //new ParamFunc<bool>(FadeTo, test, ColorNums.Blue),
                new ParamFunc<Label, Color, float, bool>(ChangeColor, test, Color.Blue, .001f),
                new ParamFunc<Label, Color, float, bool>(ChangeColor, test, Color.Red, .001f));

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
