using Microsoft.Xna.Framework;

using System;

namespace BaseGameLibrary
{
    public abstract class TickerBase
    {
        protected TimeSpan wait;
        /// <summary>
        /// elapsed milleseconds
        /// </summary>
        public int Millies { get => (int)wait.TotalMilliseconds; }
        /// <summary>
        /// adds delta time to the timer
        /// </summary>
        /// <param name="time">gametime for the current update</param>
        public void Tick(GameTime time) => Tick(time.ElapsedGameTime);
        public void Tick(TimeSpan deltaTime) => wait += deltaTime;
        /// <summary>
        /// resets elapsed time to 0
        /// </summary>
        public void Reset()
        {
            wait = TimeSpan.Zero;
        }

        protected void CloneLogic(TickerBase copy)
        {
            copy.wait = wait;
        }
    }

    /// <summary>
    /// Wrapper over timespan (probably unneccesary)
    /// </summary>
    public sealed class Ticker : TickerBase
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
    /// <summary>
    /// Timer that must be manually ticked
    /// </summary>
    public sealed class Timer : TickerBase
    {
        public int TotalMillies { get => (int)until.TotalMilliseconds; }

        TimeSpan until;

        public static implicit operator TimeSpan(Timer timer) => timer.until;
        public static implicit operator Timer(TimeSpan span) => new (span);
        public static implicit operator Timer(int time) => new (time);

        private Timer() { }
        public Timer(int length = 0)
            : this(TimeSpan.FromMilliseconds(length)) { }
        public Timer(TimeSpan until)
        {
            this.until = until;
            wait = TimeSpan.Zero;
        }

        /// <summary>
        /// clones the timer
        /// </summary>
        /// <returns>a shallow copy</returns>
        public Timer Clone()
        {
            var copy = new Timer();
            CloneLogic(copy);
            return copy;
        }
        private void CloneLogic(Timer copy)
        {
            base.CloneLogic(copy);
            copy.until = until;
        }

        /// <summary>
        /// set the timer to run for a certain amount of time
        /// </summary>
        /// <param name="length">the amount of time in milliseconds</param>
        public void SetLength(int length)
        {
            until = TimeSpan.FromMilliseconds(length);
        }


        /// <summary>
        /// checks if enough time has passed
        /// </summary>
        /// <param name="reset">whether or not the timer will reset if enough time has passed</param>
        /// <returns>whether enough time has passed</returns>
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
