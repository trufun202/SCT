using System;
using Microsoft.Xna.Framework.Audio;

namespace SnowConeTycoon.Shared.Models
{
    public class ControlledSoundEffect
    {
        private SoundEffectInstance SoundEffect;

        public ControlledSoundEffect(SoundEffectInstance soundEffect)
        {
            SoundEffect = soundEffect;
        }

        public bool IsLooped
        {
            get
            {
                return SoundEffect.IsLooped;
            }

            set
            {
                SoundEffect.IsLooped = value;
            }
        }

        public void Play()
        {
            if (Player.SoundEnabled)
            {
                SoundEffect.Play();
            }
        }

        public void PlayMusic()
        {
            if (Player.MusicEnabled)
            {
                SoundEffect.Play();
            }
        }

        public void Stop()
        {
            SoundEffect.Stop();
        }

        public void Pause()
        {
            SoundEffect.Pause();
        }

        public void Resume()
        {
            if (Player.MusicEnabled)
            {
                SoundEffect.Resume();
            }
        }
    }
}
