using BaseGameLibrary.Inputs;
using BaseGameLibrary.Visual;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;
using System.Text;

using static BaseGameLibrary.Visual.ActionButton;

namespace BaseGameLibrary
{
    class MenuScreen : ScreenBase
    {
        ButtonBase chrisIsAPoopyHead;
        ButtonBase Unlimited;
        ButtonBase setting;
        public MenuScreen(ButtonBase poopMakerOfff, ButtonBase infinite, SoundEffect music, ButtonBase sett)
            :base(music)
        {
            chrisIsAPoopyHead = poopMakerOfff;            
            Unlimited = infinite;
            setting = sett;
        }
        public override void Update(GameTime time, Screenmanager manny, CursorRoot cursor)
        {
            base.Update(time, manny, cursor);
            Play(time);
            if (heldMouse)
            {
                return;
            }
            if (Unlimited.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
            {
                manny.Next(1, true);
                return;
            }
            else if (chrisIsAPoopyHead.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
            {
                manny.Next(2, true);
                return;
            }
            else if (setting.Check(mousy.Position.ToVector2(), mouseClicks[(int)ClickType.Left]))
            {
                manny.Next(5, true);
                return;
            }
        }
        public override void Draw(SpriteBatch batch)
        {
            chrisIsAPoopyHead.Draw(batch);
            Unlimited.Draw(batch);
            setting.Draw(batch);
        }

    }
}
