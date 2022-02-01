using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BaseGameLibrary
{
    //Magic stan APPROVED
    [StructLayout(LayoutKind.Explicit)]
    public struct Setting
    {
        public enum Types : byte
        {
            BoolValue,
            FloatValue,
            KeyValue,
            IntValue
        }

        [FieldOffset(0)]
        public int ID;

        [FieldOffset(1)]
        public Types Type;

        [FieldOffset(2)]
        public bool BoolValue;

        [FieldOffset(2)]
        public float FloatValue;

        [FieldOffset(2)]
        public Keys KeyValue;

        [FieldOffset(2)]
        public int IntValue;

        [FieldOffset(3)]
        public int oldValue;


        #region bob the builder
        public Setting(int ID, Types myType, int value)
            : this(ID, myType)
        {
            IntValue = value;
            oldValue = IntValue;
        }

        public Setting(int ID, Types myType, float value)
            : this(ID, myType)
        {
            FloatValue = value;
            oldValue = IntValue;
        }

        public Setting(int ID, Types myType, bool value)
            : this(ID, myType)
        {
            BoolValue = value;
            oldValue = IntValue;
        }

        public Setting(int ID, Types myType, Keys value)
            : this(ID, myType)
        {
            KeyValue = value;
            oldValue = IntValue;
        }

        public Setting(int ID, Types myType)
        {
            this.ID = ID;
            Type = myType;

            KeyValue = Keys.None;
            BoolValue = false;
            FloatValue = 0;
            IntValue = 0;
            oldValue = 0;
        }
        #endregion

        public void Revert()
        {
            IntValue = oldValue;
        }
        public void UpdateValue()
        {
            oldValue = IntValue;
        }

        public static explicit operator bool (Setting setting) => setting.BoolValue;
        public static explicit operator float(Setting setting) => setting.FloatValue;
        public static explicit operator Keys(Setting setting) => setting.KeyValue;
        public static explicit operator int(Setting setting) => setting.IntValue;
    }

    public class Screen //: IRunnable
    {
        public Dictionary<int, Setting> Settings { get; } = new Dictionary<int, Setting>();
        //Music management
        public SoundEffectInstance IntroMusic { get; set; }
        bool introDone;
        public SoundEffectInstance Music { get; set; }
        protected bool playMusic;

        //internal binds

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


        public virtual void ChangeSetting(Setting setting)
        {
            Settings[setting.ID] = setting;
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

        internal void AddSetting(Setting setting)
        {
            Settings[setting.ID] = setting;
        }
    }
}
