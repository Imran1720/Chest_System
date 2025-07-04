using UnityEngine;

namespace ChestSystem.Sound
{
    public class SoundService
    {
        private AudioSource audioSourceBGM;
        private AudioSource audioSourceSFX;

        private SoundClipsSO soundsList;

        public SoundService(AudioSource audioSourceBGM, AudioSource audioSourceSFX, SoundClipsSO soundsList)
        {
            this.audioSourceBGM = audioSourceBGM;
            this.audioSourceSFX = audioSourceSFX;
            this.soundsList = soundsList;

            PlayBGM();
        }

        private void PlayBGM()
        {
            AudioClip clip = GetSoundClip(ESoundType.BGM);
            if (audioSourceBGM.clip != null)
            {
                audioSourceBGM.clip = clip;
                audioSourceBGM.Play();
            }
        }

        public void PlaySFX(ESoundType soundType)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioSourceSFX.PlayOneShot(clip);
            }
        }

        private AudioClip GetSoundClip(ESoundType bGM)
        {
            Sounds soundItem = soundsList.audioList.Find(item => item.soundType.Equals(bGM));

            if (soundItem.audioClip == null)
            {
                return null;
            }
            return soundItem.audioClip;
        }
    }
}
