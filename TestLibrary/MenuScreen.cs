using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    class MenuScreen : Screen
    {
        Button chrisIsAPoopyHead;
        Button Unlimited;
        Button setting;
        public MenuScreen(Button poopMakerOfff, Button infinite, SoundEffect music, Button sett)
            :base(music)
        {
            chrisIsAPoopyHead = poopMakerOfff;            
            Unlimited = infinite;
            setting = sett;
        }
        public override void Update(GameTime time, Screenmanager manny)
        {
            base.Update(time, manny);
            Play(time);
            if (heldMouse)
            {
                return;
            }
            if (Unlimited.Check(mousy.Position.ToVector2(), mouseRightClick))
            {
                manny.Next(1, true);
                return;
            }
            else if (chrisIsAPoopyHead.Check(mousy.Position.ToVector2(), mouseRightClick))
            {
                manny.Next(2, true);
                return;
            }
            else if (setting.Check(mousy.Position.ToVector2(), mouseRightClick))
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
