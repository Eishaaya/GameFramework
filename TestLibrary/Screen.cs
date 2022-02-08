using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using static BaseGameLibrary.ActionButton;

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

        public Indextionary<int, Setting> Settings { get; } = new Indextionary<int, Setting>();
        protected ButtonManager buttonManager;
        protected AestheticsManager aesthetics;

        //Music management
        public SoundEffectInstance IntroMusic { get; set; }
        bool introDone;
        public SoundEffectInstance Music { get; set; }
        protected bool playMusic;

        //internal binds

        //Mouse detection        
        protected MouseState mousy;
        protected bool[] mouseClicks;
        public bool heldMouse;
        protected Vector2 mousePos;

        //Key detection
        protected bool keysDown = false;
        protected KeyboardState Maryland;


        //ctors
        public Screen(List<ActionButton> buttons = null)
            : this(null, null, buttons) { }
        public Screen(SoundEffect m, List<ActionButton> buttons = null)
            : this(m, null, buttons) { }
        public Screen(SoundEffect m, SoundEffect im, IEnumerable<ActionButton> buttons)
        {
            buttonManager = new ButtonManager(this, buttons);
            aesthetics = new AestheticsManager();
            playMusic = true;
            Maryland = new KeyboardState();
            mousy = new MouseState();
            Music = null;
            IntroMusic = null;
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
            buttonManager.Update(mousePos, mouseClicks);
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
            mousy = Mouse.GetState();
            for (int i = 0; i < mouseClicks.Length; i++)
            {
                mouseClicks[i] = false;
            }

            heldMouse = false;
            if (mousy.LeftButton == ButtonState.Pressed)
            {
                heldMouse = true;
                mouseClicks[(int)ClickType.Left] = true;
            }
            if (mousy.RightButton == ButtonState.Pressed)
            {
                heldMouse = true;
                mouseClicks[(int)ClickType.Right] = true;
            }
            if (mousy.MiddleButton == ButtonState.Pressed)
            {
                heldMouse = true;
                mouseClicks[(int)ClickType.Middle] = true;
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
            aesthetics.Update(time);
        }

        public virtual void Transfer(int transfer)
        {
            //do shit here
        }
        public virtual void Reset()
        {
            Music.Stop();
        }
        public virtual void Draw(SpriteBatch batch)
        {
            aesthetics.Draw(batch);
            buttonManager.Draw(batch);
        }

        internal void AddSetting(Setting setting)
        {
            Settings[setting.ID] = setting;
        }
    }
}
