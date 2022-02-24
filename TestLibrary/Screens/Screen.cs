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


    enum MyGameInputs
    {
        MoveLeft,
        MoveRight,
        Jump,
        Fire,
        Horsey
    }

    //class InputManager<T> where T: Enum
    //{
    //    private Dictionary<T, List<(Func<KeyboardState, object[], bool> predicate, object[] parameters)>> keyMappings = new Dictionary<T, List<(Func<KeyboardState, object[], bool> predicate, object[] parameters)>>();

    //    public bool UseKeyboard { get; private set; } = false;
    //    public bool UseMouse { get; private set; } = false;
    //    public bool UseJoystick { get; private set; } = false;

    //    private KeyboardState ks;

    //    public void AddMapping(T input, Func<KeyboardState, object[], bool> mapping, params object[] parameters)
    //    {
    //        UseKeyboard = true;

    //        if(!keyMappings.ContainsKey(input))
    //        {
    //            keyMappings[input] = new List<(Func<KeyboardState, object[], bool> predicate, object[] parameters)>();
    //        }

    //        keyMappings[input].Add((mapping, parameters));
    //    }

    //    public void Update()
    //    {
    //        if(UseKeyboard)
    //        {
    //            // update KeyboardState
    //        }
    //    }

    //    public bool this[T index]
    //    {
    //        get
    //        {
    //            foreach (var (predicate, parameters) in keyMappings[index])
    //            {
    //                if (predicate(ks, parameters)) return true;
    //            }

    //            return false;
    //        }
    //    }
    //}

    //class MyGame
    //{
    //    void Init()
    //    {
    //        InputManager<MyGameInputs> inputManager = new InputManager<MyGameInputs>();

    //        inputManager.AddMapping(MyGameInputs.MoveLeft, (ks, key) => ks.IsKeyDown((Keys)key[0]), Keys.Left);
    //        inputManager.AddMapping(MyGameInputs.Horsey, (ks, keys) => ks.IsKeyDown((Keys)keys[0]) && ks.IsKeyDown((Keys)keys[1]), Keys.H, Keys.LeftControl);

    //        bool isHorsey = inputManager[MyGameInputs.Horsey];
    //    }
    //}


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
        protected Click[] mouseClicks;
        public bool heldMouse;
        protected Vector2 mousePos;

        //Key detection
        protected bool keysDown = false;
        protected KeyboardState Idaho;


        //ctors
        public Screen(IEnumerable<ActionButton> buttons = null, IEnumerable<IRunnable> pretties = null)
            : this(null, null, buttons, pretties) { }
        public Screen(SoundEffect m, IEnumerable<ActionButton> buttons = null, IEnumerable<IRunnable>pretties = null)
            : this(m, null, buttons, pretties) { }
        public Screen(SoundEffect m, SoundEffect im, IEnumerable<ActionButton> buttons = null, IEnumerable<IRunnable> pretties = null)
            : this(m, im, new ButtonManager(buttons), new AestheticsManager(pretties)) { }
        public Screen(SoundEffect m, SoundEffect im, ButtonManager buttons, AestheticsManager pretties)
        {
            buttonManager = buttons ?? new ButtonManager();
            aesthetics = pretties ?? new AestheticsManager();
            playMusic = true;
            Idaho = new KeyboardState();
            mousy = new MouseState();
            mouseClicks = new Click[3];
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
            heldMouse = false;
            buttonManager.Update(mousePos, heldMouse, mouseClicks);
            CheckMouse();
        }

        protected void CheckKeys()
        {
            Idaho = Keyboard.GetState();
            if (Idaho.GetPressedKeyCount() == 0)
            {
                keysDown = false;
            }
        }

        protected void CheckMouse()
        {
            mousy = Mouse.GetState();
            mousePos = mousy.Position.ToVector2();

            heldMouse = false;

            if (mousy.LeftButton == ButtonState.Pressed)
            {
                heldMouse = true;
                mouseClicks[(int)ClickType.Left].IsClicked = true;
            }
            if (mousy.RightButton == ButtonState.Pressed)
            {
                heldMouse = true;
                mouseClicks[(int)ClickType.Right].IsClicked = true;
            }
            if (mousy.MiddleButton == ButtonState.Pressed)
            {
                heldMouse = true;
                mouseClicks[(int)ClickType.Middle].IsClicked = true;
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
