using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLib.System.Audio
{
    [Serializable]
    public class AudioAttributes
    {
        public static int WALK_SOUND = 0;
        public static int JUMP_SOUND = 1;
        public static int LANDING_SOUND = 2;
        public static int HITTED_SOUND = 3;

        [SerializeField]
        protected AudioSource audioSource;

        public AudioSource AudioSource
        {
            get { return audioSource; }
            set { audioSource = value; }
        }

        [SerializeField]
        protected List<AudioClip> audioClips;

        public List<AudioClip> AudioClips
        {
            get { return audioClips; }
            set { audioClips = value; }
        }

        [SerializeField]
        protected bool isNPC;

        public bool IsNPC
        {
            get { return isNPC; }
            set { isNPC = value; }
        }

        [SerializeField]
        protected GameObject player;

        public GameObject Player
        {
            get { return player; }
            set { player = value; }
        }

        [SerializeField]
        protected GameObject source;

        public GameObject Source
        {
            get { return source; }
            set { source = value; }
        }

        [SerializeField]
        protected float maxDistance;

        public float MaxDistance
        {
            get { return maxDistance; }
            set { maxDistance = value; }
        }

        [SerializeField]
        protected float maxValuePercentage;

        public float MaxValuePercentage
        {
            get { return maxValuePercentage; }
            set { maxValuePercentage = value; }
        }

        public AudioAttributes()
        {
        }

        public void playAudio(int audioId)
        {
            if (isNPC)
            {
                float distance = Vector3.Distance(player.transform.position, source.transform.position);
                audioSource.volume = (distance <= maxDistance) ? 
                    (1.0f - (((100.0f * distance) / maxDistance) / 100)) * maxValuePercentage : 0;
            }
            if (audioSource != null && audioId < audioClips.Count)
            {
                audioSource.clip = audioClips[audioId];
                audioSource.Play();
            }
        }

        public void stopAudio()
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }
}
