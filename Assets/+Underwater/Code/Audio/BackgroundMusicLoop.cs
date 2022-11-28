using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlamingApes.Underwater
{
    public class BackgroundMusicLoop : MonoBehaviour
    {
        public static BackgroundMusicLoop instance;

        [SerializeField]
        private AudioSource musicSource;

        public bool isPlaying;
        public AudioClip[] musicClips;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            isPlaying = true;
            StartCoroutine(PlayMusicLoop());
        }

        IEnumerator PlayMusicLoop ()
        {
            yield return null;

            while(isPlaying)
            {
                for(int i = 0; i < musicClips.Length; i++)
                {
                    musicSource.clip = musicClips[i];
                    musicSource.Play();
                }
                while(musicSource.isPlaying)
                {
                    yield return null;
                }
            }
        }
    }
}
