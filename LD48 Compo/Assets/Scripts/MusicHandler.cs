using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicHandler : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void SetMusic(int clipIndex, bool looping, float volume)
    {
        audioSource.Stop();

        SetClip(clipIndex);
        SetLoop(looping);
        SetVolume(volume);

        audioSource.Play();


    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetClip(int index)
    {
        audioSource.clip = audioClips[index];
    }

    public void SetLoop(bool isLooping)
    {
        audioSource.loop = isLooping;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
