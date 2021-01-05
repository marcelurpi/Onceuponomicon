using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(float volume, AudioClip soundClip)
    {
        soundSource.volume = volume;
        soundSource.clip = soundClip;
        soundSource.Play();
    }

    public void SetMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }
}
