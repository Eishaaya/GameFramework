﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseGameLibrary;
using static BaseGameLibrary.Extensions;
using static BaseGameLibrary.Sequence;
using static BaseGameLibrary.VisualObject;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using BaseGameLibrary.Inputs;

namespace TestProj
{
    public class Game1 : Game //COMMENT TO LET ME COMMIT
    {
        //InputManager<Binds> manager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Label test;
        //Timer timer;
        //int runner = 0;

        Indextionary<int, string> indextionary = new Indextionary<int, string>();
        Screenmanager manny;

        Screen screen;
        Screen horseyScreen;

        enum Binds
        {
            Left,
            Right,
            LClick,
            RClick,
            MClick,
            Scroll,
            MouseX,
            MouseY,
            Help,
            Group
        }
        KeyControl left = new KeyControl(Keys.A, new DigitalStateComponent());
        KeyControl right = new KeyControl(Keys.D, new DigitalStateComponent());

        MouseControl clicked = new MouseControl(new ParamFunc<MouseState, BoolInt>(m => m.LeftButton == ButtonState.Pressed, Mouse.GetState()), new DigitalStateComponent());



        Sequence sequence;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            // Form related tomfoolery

            //var form = System.Windows.Forms.Form.FromHandle(Window.Handle) as System.Windows.Forms.Form;
            //var button = new System.Windows.Forms.Button()
            //{
            //    Location = System.Drawing.Point.Empty,
            //    AutoSize = true,
            //    Text = "Hello"
            //};

            //button.Click += (s, e) => System.Windows.Forms.MessageBox.Show("Hacks!\0Hi!");

            //form.Controls.Add(button);
            //form.MouseMove += (s, e) => form.Text = $"{e.X}, {e.Y}";

            //form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //form.AllowTransparency = true;
            //form.TransparencyKey = System.Drawing.Color.CornflowerBlue;


        }

        protected override void LoadContent()
        {
            var ticky = new Ticker();

            Screen testScreen = new Screen();

            //indextionary.Add(0, "hello");
            indextionary[0] = "amogus";

            //DIEU TIER CODING
            BoolInt bob = new BoolInt(720);
            var bobby = (bool)bob;
            bob = bobby;
            ;

            Dictionary<Binds, InputControl> idkName = new Dictionary<Binds, InputControl>()
            {
                [Binds.Left] = left,
                [Binds.Right] = right,
                [Binds.LClick] = clicked,
                [Binds.RClick] = new MouseControl(new ParamFunc<MouseState, BoolInt>(m => m.RightButton == ButtonState.Pressed, Mouse.GetState()), new DigitalStateComponent()),
                [Binds.MClick] = new MouseControl(new ParamFunc<MouseState, BoolInt>(m => m.MiddleButton == ButtonState.Pressed, Mouse.GetState()), new DigitalStateComponent()),
                [Binds.Scroll] = new MouseControl(new ParamFunc<MouseState, BoolInt>(m => m.ScrollWheelValue, Mouse.GetState()), new DigitalStateComponent()),
                [Binds.MouseX] = new MouseControl(new ParamFunc<MouseState, BoolInt>(m => m.Position.X, Mouse.GetState()), new AnalogStateComponent()),
                [Binds.MouseY] = new MouseControl(new ParamFunc<MouseState, BoolInt>(m => m.Position.Y, Mouse.GetState()), new AnalogStateComponent()),
                [Binds.Help] = new StickControl(0, new DigitalStateComponent()),
                [Binds.Group] = new ComplexControl<Binds>(ComplexControl<Binds>.ControlType.AND, new DigitalStateComponent(), Binds.Left, Binds.Right, Binds.LClick)
            };
            InputManager<Binds>.Instance.Fill(idkName);

            VisualCursor<Binds>.Mouse.AttachClicks(Binds.LClick, Binds.RClick, Binds.MClick, Binds.Scroll, Binds.MouseX, Binds.MouseY);
            VisualCursor<Binds>.Mouse.AttachSprite(new Sprite(Content.Load<Texture2D>("unknown"), Vector2.Zero, Vector2.Zero, .030f));

            test = new Label(Content.Load<SpriteFont>("File"), Color.Wheat, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), "Shid & fard", true);
            // timer = new Timer(5000);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var karan = new Sprite(Content.Load<Texture2D>("unknown"), new Vector2(400), new Vector2(100), .05f);

            sequence = new Sequence();
            sequence.AttachSequence(
                new ParamFunc<Label, ColorNums, bool>(FadeTo, test, ColorNums.Red),
                new ParamFunc<Label, Color, float, bool>(ChangeColor, test, Color.White, .1f),
                new ParamFunc<Label, ColorNums, bool>(FadeTo, test, ColorNums.Green),
                new ParamFunc<Label, int, float, bool, bool>(Pulsate, test, 150, .1f, false),
                new ParamFunc<Label, Color, float, bool>(ChangeColor, test, Color.White, .1f),
                new ParamFunc<Sprite, int, float, bool, bool>(Vibrate, karan, 50, .1f, true),
                new ParamFunc<Sequence, int, bool>(Wait, sequence, 5000),
                new ParamFunc<Label, float, float, bool, bool>(Rotate, test, MathHelper.ToRadians(90), .1f, false),
                new ParamFunc<Label, Color, int, bool>(Fade, test, test.Color, 3));


            //  var eight = gIMMEeIGHT();

            var bottomScroll = new AnimatingSprite(Content.Load<Texture2D>("bottom"), new Vector2(0, 50), Color.White, 0, SpriteEffects.None, new Vector2(0, 0), 1, .025f,
            new RectangleFrame[]
            {
                new Rectangle(0, 0, 600, 90),
                new Rectangle(0, 91, 600, 90),
                new Rectangle(0, 182, 600, 90),
                new Rectangle(0, 273, 600, 90),
                new Rectangle(0, 364, 600, 90),
                new Rectangle(0, 455, 600, 90),
                new Rectangle(0, 546, 600, 90),
                new Rectangle(0, 637, 600, 90),
                new Rectangle(0, 728, 600, 90),
                new Rectangle(0, 819, 600, 90),
                new Rectangle(0, 910, 600, 90),
                new Rectangle(0, 1001, 600, 90),
                new Rectangle(0, 1092, 600, 90),
                new Rectangle(0, 1183, 600, 90),
                new Rectangle(0, 1274, 600, 90)
            }, false, true, 80);


            var pretty = new AestheticsManager(bottomScroll, karan, sequence, test);

            ActionButton button = new ActionButton(new Button(Content.Load<Texture2D>("Toggle Ball"), new Vector2(200)), new ParamAction<AestheticsManager>(m => m.Running = !m.Running, pretty));
            ButtonManager buttons = new ButtonManager(button);

            screen = new Screen(Content.Load<SoundEffect>("UnlimitedIntro"), Content.Load<SoundEffect>("UnlimitedMusic"), buttons, pretty);
        }

        protected override void Update(GameTime gameTime)
        {
            // sequence.RunSequence(gameTime);            
            var manager = InputManager<Binds>.Instance;
            manager.Update(gameTime);

            var isLeft = manager[Binds.Left] == true;
            var isRight = manager[Binds.Right] == true;
            var no = VisualCursor<Binds>.Mouse[CursorRoot.Info.Left];
            var isHeld = no == true;


            if (true || manager[Binds.Group])
            {
                screen.Update(gameTime, manny, VisualCursor<Binds>.Mouse);
            }
            VisualCursor<Binds>.Mouse.Update();

            var mouseX = manager[Binds.MouseX];
            var mouseY = manager[Binds.MouseY];

            test.Text($"Left: {isLeft}, Right: {isRight}, Held: {isHeld}\nMouse Moved: {mouseX || mouseY}, ({(int)mouseX}, {(int)mouseY})");
        }

        //int gIMMEeIGHT()
        //{
        //    return 3 + 5;
        //}

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();


            // test.Draw(spriteBatch);
            screen.Draw(spriteBatch);
            VisualCursor<Binds>.Mouse.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
