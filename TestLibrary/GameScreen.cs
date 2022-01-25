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
    class GameScreen<TEnum> : Screen where TEnum : Enum 
    {
        GameRunner<TEnum> main;
        Random random;
        Label[] labels;
        Sprite[] sprites;
        AnimatingSprite[] animatingSprites;
        Button[] buttons;
        Keys[] keys;
        public Impact[] Effects { get; set; }

        public bool Lost {get; set;}
        Keys pauseKey;

        public GameScreen(SoundEffect mus, SoundEffect intro, Label[] labels, Sprite[] sprites, AnimatingSprite[] animatingSprites, Keys[] keys, Button[] buttons, Impact[] effects, Random random)
            : base(mus, intro)
        {
            this.labels = labels;
            this.sprites = sprites;
            this.animatingSprites = animatingSprites;
            this.keys = keys;
            this.buttons = buttons;
            this.Effects = effects;

            Lost = false;
            this.random = random;
        }
        public override void changeBinds(List<Keys> newBinds, List<bool> bools)
        {
            base.changeBinds(newBinds, bools);
            //    grid.downKey = newBinds[0];
            //    grid.turnKey = newBinds[1];
            //    grid.leftKey = newBinds[2];
            //    grid.rightKey = newBinds[3];
            //    grid.TeleKey = newBinds[4];
            //    grid.switchKeys[0] = newBinds[5];
            //    grid.switchKeys[1] = newBinds[6];
            //    grid.switchKeys[2] = newBinds[7];
            //    grid.switchKeys[3] = newBinds[8];
            //    pauseKey = newBinds[9];
            //    grid.playSounds = bools[1];
            //    grid.holdTurn = bools[2];
            //    grid.holdDown = bools[3];
            //    grid.holdSide = bools[4];
            //    grid.willProject = bools[5];
        }        
        public override void Reset()
        {
            main.Reset();
        }
        public override void Update(GameTime time, Screenmanager manny)
        {
            base.Update(time, manny);
            
            main.Update(time);
        }
        public override void Play(GameTime time)
        {
            foreach (AnimatingSprite animatingSprite in  animatingSprites)
            {
                animatingSprite.Animate(time);
            }
            base.Play(time);
        }
        public override void Draw(SpriteBatch batch)
        {
            main.Draw(batch);
        }
    }
}
