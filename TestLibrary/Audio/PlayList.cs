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
    public class PlayList<TSoundType> where TSoundType : Enum
    {
        public class IntLooper : ILocationGenerator<int>
        {
            public IntLooper(PlayList<TSoundType> playList) => myPlaylist = playList;
            PlayList<TSoundType> myPlaylist;
            int soundIndex = 0;
            public int GetNextLocation() => soundIndex = (soundIndex + 1) % myPlaylist.sounds.Count;
            
        }
        List<TSoundType> sounds;
        SoundEffectInstance currentSound;

        Func<int> GetNextSound;

        void Update()
        {
            if (currentSound.State != SoundState.Stopped) return;

            currentSoundIndex = GetNextSound();

        }
    }
}
