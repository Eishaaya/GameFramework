using Microsoft.Xna.Framework;

using System;

namespace BaseGameLibrary
{
    public abstract class TickerBase
    {
        protected TimeSpan wait;
        public int Millies { get => (int)wait.TotalMilliseconds; }
        public void Tick(GameTime time)
        {
            wait += time.ElapsedGameTime;
        }
        public void Reset()
        {
            wait = TimeSpan.Zero;
        }

        public void CloneLogic(TickerBase copy)
        {
            copy.wait = wait;
        }
    }

    public class Ticker : TickerBase
    {
        private Ticker() { }
        public Ticker(int waitMillies = 0)
            : this(new TimeSpan(0, 0, 0, 0, waitMillies)) { }
        public Ticker(TimeSpan wait)
        {
            this.wait = wait;
        }

        public Ticker Clone()
        {
            var copy = new Ticker();
            CloneLogic(copy);
            return copy;
        }
    }

    public class Timer : TickerBase
    {
        public int TotalMillies { get => (int)until.TotalMilliseconds; }

        TimeSpan until;

        public static implicit operator TimeSpan(Timer timer) => timer.until;
        public static implicit operator Timer(TimeSpan span) => new Timer(span);
        public static implicit operator Timer(int time) => new Timer(time);

        private Timer() { }
        public Timer(int length = 0)
            : this(TimeSpan.FromMilliseconds(length)) { }
        public Timer(TimeSpan until)
        {
            this.until = until;
            wait = TimeSpan.Zero;
        }

        public Timer Clone()
        {
            var copy = new Timer();
            CloneLogic(copy);
            return copy;
        }
        public void CloneLogic(Timer copy)
        {
            base.CloneLogic(copy);
            copy.until = until;
        }

        public void SetLength(int length)
        {
            until = TimeSpan.FromMilliseconds(length);
        }



        public bool Ready(bool reset = true)
        {
            if (wait >= until)
            {
                if (reset)
                {
                    wait = TimeSpan.Zero;
                }
                return true;
            }
            return false;
        }
    }
}
