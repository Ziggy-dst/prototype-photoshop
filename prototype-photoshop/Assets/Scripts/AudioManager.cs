using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace CustomNamespace
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void PlaySound(AudioClip audioClip)
        {
            GameObject tempAudio = new GameObject("Temp Audio");
            AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();

            tempAudioSource.playOnAwake = false;
            tempAudioSource.clip = audioClip;
            tempAudioSource.Play();
            StartCoroutine(DestroyTempAudio(tempAudio));
        }

        private IEnumerator DestroyTempAudio(GameObject tempAudio)
        {
            float clipLength = tempAudio.GetComponent<AudioSource>().clip.length;
            yield return new WaitForSeconds(clipLength + 1f);
            Destroy(tempAudio);
        }
    }
}