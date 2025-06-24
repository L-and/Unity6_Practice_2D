using System;
using UnityEngine;

namespace Platformer
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource audioSource;
        public AudioClip bgmClip;
        public AudioClip jumpClip;
        
        public void SetBgmSound(AudioClip bgmClip)
        {
            audioSource.clip = bgmClip;

            audioSource.playOnAwake = true;
            audioSource.loop = true;
            audioSource.Play();
        }
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            SetBgmSound(bgmClip);
        }

        public void OnJumpSound()
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }
}

