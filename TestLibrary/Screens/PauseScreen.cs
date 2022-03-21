using BaseGameLibrary.Inputs;
using BaseGameLibrary.Visual;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

using static BaseGameLibrary.Visual.ActionButton;

namespace BaseGameLibrary
{
    class PauseScreen : Screen
    {
        ButtonBase menu;
        ButtonBase back;
        ButtonBase restart;
        SpriteBase tint;
        Keys exit;
        public PauseScreen(SpriteBase dark, ButtonBase menuButt, ButtonBase ReturnButt, ButtonBase restartButt, Keys Exit = Keys.Escape)
            : base()
        {
            exit = Exit;
            tint = dark;
            menu = menuButt;
            restart = restartButt;
            back = ReturnButt;
        }

        public override void Update(GameTime time, Screenmanager manny, CursorRoot cursor)
        {
            base.Update(time, manny, cursor);
            if (heldMouse || keysDown)
            {
                return;
            }
            if (menu.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
            {
                manny.Next(0, true);
                manny.PreviousScreens.Pop();
                manny.PreviousScreens.Pop().Reset();
                manny.ClearMemory();
                return;
            }
            if (restart.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
            {
                manny.Back();
                manny.CurrentScreen.Reset();
                return;
            }
            if (back.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]) || Idaho.IsKeyDown(exit) || mouseClicks[(int)ClickType.Left])
            {
                manny.Back();
                return;
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            tint.Draw(batch);
            back.Draw(batch);
            menu.Draw(batch);
            restart.Draw(batch);
        }
    }
}