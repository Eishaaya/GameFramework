using Microsoft.Xna.Framework.Audio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary.Audio
{
    public enum PlayStyle
    {
        Shuffle,
        OrderedLoop,
        Ordered
    }
    public interface ILocationGenerator<T>
    {
        T GetNextLocation();
        void Reset();
    }
    public class IndexLooper<T> : ILocationGenerator<int>
    {
        public IndexLooper(List<T> playList) => mylist = playList;
        readonly List<T> mylist;
        int soundIndex = 0;
        public int GetNextLocation() => soundIndex = (soundIndex + 1) % mylist.Count;

        public void Reset()
        {
            soundIndex = 0;
        }
    }
    public class IndexShuffler<T> : ILocationGenerator<int>
    {
        public IndexShuffler(List<T> playList, Random? random = null)
        {
            mylist = playList;
            this.random = random ?? Random.Shared;
            bag = new(mylist.Count);
        }
        readonly Random random;
        readonly List<T> mylist;
        readonly List<int> bag;
        public void Reset()
        {
            for (int i = 0; i < mylist.Count; bag.Add(i)) ;
            bag.Shuffle(random);
        }
        public int GetNextLocation()
        {
            if (bag.Count == 0) Reset();

            var nextLocation = bag[^1];
            bag.RemoveAt(bag.Count - 1);//
            return nextLocation;
        }
    }

    public sealed class PlayList<TSoundType> : IRunnable where TSoundType : Enum
    {
        readonly List<TSoundType> sounds;
        SoundEffectInstance currentSound;
        readonly ILocationGenerator<int> soundPicker;
        public PlayList(ILocationGenerator<int> soundPicker, params TSoundType[] sounds)
        {
            this.soundPicker = soundPicker;
            this.sounds = new(sounds);
            currentSound = MusicManager<TSoundType>.Instance.Play(sounds[soundPicker.GetNextLocation()]);
        }
        public void Reset()
        {
            soundPicker.Reset();
        }
        public void Update()
        {
            if (currentSound.State != SoundState.Stopped) return;

            currentSound = MusicManager<TSoundType>.Instance.Play(sounds[soundPicker.GetNextLocation()]);

        }

        void IRunnable.Update(GameTime gameTime) => Update();

        void IRunnable.Draw(SpriteBatch spriteBatch) { }
    }
}
