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
    class Screen
    {
        //Music management
        public SoundEffectInstance IntroMusic { get; set; }
        bool introDone;
        public SoundEffectInstance Music { get; set; }
        protected bool playMusic;
        
        //internal binds
        public List<Keys> binds { get; set; }

        //Mouse detection
        protected MouseState mousy;        
        protected bool mouseRightClick;
        protected bool mouseLeftClick;
        public bool heldMouse;

        //Key detection
        protected bool keysDown = false;
        protected KeyboardState Maryland;


        //ctors
        public Screen()
            : this(null, null) { }
        public Screen(SoundEffect m)
            : this(m, null) { }
        public Screen(SoundEffect m, SoundEffect im)
        {
            playMusic = true;
            Maryland = new KeyboardState();
            mousy = new MouseState();
            mouseRightClick = false;
            mouseLeftClick = false;
            Music = null;
            IntroMusic = null;
            mouseRightClick = false;
            if (m != null)
            {
                Music = m.CreateInstance();
                Music.IsLooped = true;
                if (im != null)
                {
                    IntroMusic = im.CreateInstance();
                    introDone = false;
                }
                else
                {
                    introDone = true;
                }
            }
            else
            {
                introDone = true;
            }
        }


        public virtual void changeBinds(List<Keys> newBinds, List<bool> bools)
        {
            playMusic = bools[0];
            if (!playMusic)
            {
                StopMusic();
            }
        }

        public virtual List<bool> GetBools()
        {
            return new List<bool>();
        }
        public void StopMusic()
        {
            if (Music != null)
            {
                introDone = false;
                Music.Stop();
                if (IntroMusic != null)
                {
                    IntroMusic.Stop();
                }
            }
        }
        public virtual void Start()
        {
            keysDown = true;
            heldMouse = true;
            if (playMusic)
            {
                if (IntroMusic == null)
                {
                    if (Music != null && Music.State != SoundState.Playing)
                    {
                        Music.Play();
                    }
                    return;
                }
                if (Music.State == SoundState.Playing)
                {
                    return;
                }
                IntroMusic.Play();
            }
            introDone = false;
        }
        public virtual void Update(GameTime time, Screenmanager manny)
        {
            Play(time);
            CheckKeys();
            CheckMouse();
        }

        protected void CheckKeys()
        {
            Maryland = Keyboard.GetState();
            if (Maryland.GetPressedKeyCount() == 0)
            {
                keysDown = false;
            }
        }

        protected void CheckMouse()
        {
            if (mousy.LeftButton == ButtonState.Pressed || mousy.RightButton == ButtonState.Pressed)
            {
                heldMouse = true;
            }
            else
            {
                heldMouse = false;
            }

            mousy = Mouse.GetState();
            mouseRightClick = false;
            mouseLeftClick = false;


            if (mousy.RightButton == ButtonState.Pressed)
            {
                mouseRightClick = true;
            }
            
            if (mousy.LeftButton == ButtonState.Pressed)
            {
                mouseLeftClick = true;
            }
        }

        public void PlayMusic()
        {
            if (!introDone && playMusic && (IntroMusic == null || IntroMusic.State == SoundState.Stopped))
            {
                Music.Play();
                introDone = true;
            }
        }
        public virtual void Play(GameTime time)
        {
            if (Music != null)
            {
                PlayMusic();
            }
        }

        public virtual void Transfer(int transfer)
        {

        }
        public virtual void Reset()
        {

        }
        public virtual void Draw(SpriteBatch batch)
        {

        }
    }
}
