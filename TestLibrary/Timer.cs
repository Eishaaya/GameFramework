using Microsoft.Xna.Framework;
using System;

namespace BaseGameLibrary
{
    public class Timer
    {
        public int Millies { get => (int)wait.TotalMilliseconds; }
        public int TotalMillies { get => (int)until.TotalMilliseconds; }

        TimeSpan wait;
        TimeSpan until;

        public static implicit operator TimeSpan (Timer timer) => timer.until;
        public static implicit operator Timer (TimeSpan span) => new Timer(span);
        public static implicit operator Timer (int time) => new Timer(time);

        public Timer(TimeSpan until)
        {
            this.until = until;
            wait = TimeSpan.Zero;
        }

        public void SetLength(int length)
        {
            until = TimeSpan.FromMilliseconds(length);
        }

        public Timer(int length)
            : this(TimeSpan.FromMilliseconds(length)) { }

        public void Tick(GameTime time)
        {
            wait += time.ElapsedGameTime;
        }

        public bool Ready(bool reset = true)
        {
            if(wait >= until)
            {
                if (reset)
                {
                    wait = TimeSpan.Zero;
                }
                return true;
            }
            return false;
        }

        public void Reset()
        {
            wait = TimeSpan.Zero;
        }
    }
}
