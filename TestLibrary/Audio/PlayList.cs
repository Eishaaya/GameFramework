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
        public T GetNextLocation();
    }
    public class IndexLooper<T> : ILocationGenerator<int>
    {
        public IndexLooper(List<T> playList) => mylist = playList;
        List<T> mylist;
        int soundIndex = 0;
        public int GetNextLocation() => soundIndex = (soundIndex + 1) % mylist.sounds.Count;

    }
    public class IndexShuffler<T> : ILocationGenerator<int>
    {
        public IndexShuffler(List<T> playList, Random? random = null)
        {
            mylist = playList;
            this.random = random ?? Random.Shared;
        }
        Random random;
        List<T> mylist;
        List<int> bag;
        public int GetNextLocation()
        {
            return bag[]
        }

    }

    public class PlayList<TSoundType> where TSoundType : Enum
    {


        List<TSoundType> sounds;
        SoundEffectInstance currentSound;

        Func<int> GetNextSound;

        void Update()
        {
            if (currentSound.State != SoundState.Stopped) return;

            currentSound = MusicManager<TSoundType>.Instance.Play(sounds[GetNextSound()]);

        }
    }
}
