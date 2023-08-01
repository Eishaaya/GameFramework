using Microsoft.Xna.Framework.Audio;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary.Screens.Support
{
    public partial class MusicManager<TSoundType> where TSoundType : Enum
    {
        float volume;
        /// <summary>
        /// the master volume of all sounds that exist
        /// </summary>
        public float Volume
        {
            get => volume;
            set
            {
                volume = value;
                foreach (var sound in activeSounds)
                {
                    sound.Instance.Volume = value * sounds[sound.tag].Volume;
                }
            }
        }
        public void SetVolume(TSoundType predicate, float preferredvolume)
        {
            foreach (var item in sounds)
            {
                if (!SoundEvaluator.Match(item.Key, predicate)) continue;

                item.Value.Volume = preferredvolume;
            }

            preferredvolume *= volume;
            foreach (var sound in activeSounds)
            {
                if (!SoundEvaluator.Match(sound.tag, predicate)) continue;
                sound.Instance.Volume = preferredvolume;
            }
        }        

        public ISoundEvaluator SoundEvaluator { get; set; } = SoundFlagger;

        private Dictionary<TSoundType, PooledSound> sounds;

        readonly List<TaggedSoundInstance> activeSounds = new();
        public static MusicManager<TSoundType> Instance { get; } = new();
        private MusicManager() { sounds = null!; }      

        public PooledSound this[TSoundType tag] => sounds[tag];

        public void AttachSounds(params (TSoundType tag, SoundEffect template, int volume)[] sounds) => this.sounds = sounds.ToDictionary(m => m.tag, m => new PooledSound(m.template, m.volume));

        public SoundEffectInstance Play(TSoundType chosenSound, bool looped = false)
        {
            //think of a nicer way to do this, but this shouldn't matter
            var chosenSoundSource = sounds[chosenSound];
            chosenSoundSource.ActiveCount++;

            var newSound = chosenSoundSource.Pool.TryPeek(out var instance) ? instance : chosenSoundSource.Template.CreateInstance();
            newSound.IsLooped = looped;
            newSound.Volume = volume * chosenSoundSource.Volume;
            newSound.Play();
            activeSounds.Add(new(chosenSound, newSound));
            return newSound;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CacheSound(int soundIndex)
        {
            var stoppingSound = activeSounds[soundIndex];
            //swapping and removing at end to avoid shifting
            (activeSounds[^1], activeSounds[soundIndex]) = (stoppingSound, activeSounds[^1]);
            activeSounds.RemoveAt(activeSounds.Count - 1);

            var soundCache = sounds[stoppingSound.tag];
            soundCache.Pool.Push(stoppingSound.Instance);
            soundCache.ActiveCount--;
        }
        public void Update()
        {
            for (int i = 0; i < activeSounds.Count; i++)
            {
                var sound = activeSounds[i];
                if (sound.Instance.State == SoundState.Stopped)
                {
                    CacheSound(i);
                }
            }
        }

        public void StopSounds(TSoundType predicate)
        {
            for (int i = 0; i < activeSounds.Count; i++)
            {
                var sound = activeSounds[i];
                if (SoundEvaluator.Match(sound.tag, predicate))
                {
                    sound.Instance.Stop();
                    CacheSound(i);
                }
            }
        }
        public void Mute() => Volume = 0;

    }
}
