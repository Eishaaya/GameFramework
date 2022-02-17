using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using System.Text;

namespace BaseGameLibrary
{
    public class Sequence : IRunnable
    {
        public List<IParamFunc<bool>> sequence { get; private set; }
        int current;
        Timer delay = new Timer(0);
        bool ticking = false;

        public Sequence()
        {
            current = 0;
        }

        public Sequence(params IParamFunc<bool>[] funcs)
        {
            current = 0;
            AttachSequence(funcs);
        }
        public void AttachSequence(params IParamFunc<bool>[] funcs)
        {
            sequence = new List<IParamFunc<bool>>(funcs);
        }

        public bool Wait (int time)
        {
            ticking = true;
            if (delay.TotalMillies != time)
            {                
                delay.SetLength(time);
            }
            return delay.Ready();
        }

        public int RunSequence(GameTime gameTime)
        {
            if (ticking)
            {
                delay.Tick(gameTime);
                ticking = false;
            }

            if (current >= sequence.Count)
            {
                current = 0;
            }
            current += sequence[current].Call() ? 1 : 0;
            return current;
        }

        public void Update(GameTime gameTime)
        {
            RunSequence(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) { }
    }
}
