using Microsoft.Xna.Framework.Audio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary.Screens.Support
{
    public partial class MusicManager<TSoundType> where TSoundType : Enum
    {
        /// <summary>
        /// Used for the music manager to know which sounds to disable
        /// </summary>
        public interface ISoundEvaluator
        {
            bool Match(TSoundType sound, TSoundType predicate);
        }
        public struct SoundMatcherEvaluator : ISoundEvaluator
        {
            public bool Match(TSoundType sound, TSoundType predicate) => sound.Equals(predicate);
        }
        public struct SoundFlagEvaluator : ISoundEvaluator
        {
            public bool Match(TSoundType sound, TSoundType predicate) => sound.HasFlag(predicate);
        }
        record struct TaggedSoundInstance (TSoundType tag, SoundEffectInstance Instance);
        public record class PooledSound(SoundEffect Template, Stack<SoundEffectInstance> Pool)
        {
            public PooledSound(SoundEffect template, float volume)
                : this(template, new Stack<SoundEffectInstance>())
            {
                Volume = volume;
            }
            public float Volume { get; set; }
            public int ActiveCount { get; internal set; } = 0;
        }
        record class SoundAttribute
        {
            public bool Blocked { get; set; }
            public float Volume { get; set; }
        }

        /// <summary>
        /// Returns a sound evaluator that checks for flags
        /// </summary>
        public static SoundFlagEvaluator SoundFlagger => new ();

        /// <summary>
        /// Returns a sound evaluator that runs straight up equality checks
        /// </summary>
        public static SoundMatcherEvaluator SoundMatcher => new ();
    }
}
