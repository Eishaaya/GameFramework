using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary.Inputs
{
    public abstract class InputStateComponent
    {
        protected bool state;

        public abstract void Update(GameTime gameTime);
        public abstract bool Press(BoolInt newInfo);
        public abstract int GetValue { get; }

        public static implicit operator bool(InputStateComponent me) => me.state;
        public static explicit operator int(InputStateComponent me) => me.GetValue;

        public static bool operator >(InputStateComponent me, int other)
            => me.GetValue > other;
        public static bool operator <(InputStateComponent me, int other)
            => me.GetValue < other;
        public static bool operator >=(InputStateComponent me, int other)
            => me.GetValue >= other;
        public static bool operator <=(InputStateComponent me, int other)
            => me.GetValue <= other;

        public static string operator +(InputStateComponent me, string other)
            => $"{me.state}, {other}";
        public static string operator +(string other, InputStateComponent me)
            => $"{other}, {me.state}";
    }

    public class DigitalStateComponent : InputStateComponent
    {
        Ticker heldTicker;
        public override int GetValue
        {
            get => heldTicker.Millies;
        }

        public DigitalStateComponent()
        {
            heldTicker = new Ticker();
        }

        public override void Update(GameTime gameTime)
        {
            heldTicker.Tick(gameTime);
        }
        public override bool Press(BoolInt isDown)
        {
            var held = isDown && state;
            if (!held)
            {
                heldTicker.Reset();
            }
            state = isDown;
            return isDown;
        }
    }

    public class AnalogStateComponent : InputStateComponent
    {
        int location;
        public override int GetValue
        {
            get => location;
        }

        public override bool Press(BoolInt newInfo)
        {
            state = location != newInfo;
            location = newInfo;

            return state;
        }

        public override void Update(GameTime gameTime) { }
    }
}
