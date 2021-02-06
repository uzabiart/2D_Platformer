using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerModule : Module
{
    public AudioClip[] randomizedAudioClips;
    public float sfxVolume;

    public void PlayMySFX()
    {
        int randomClip = UnityEngine.Random.Range(0, randomizedAudioClips.Length);
        if (sfxVolume == 0) sfxVolume = 0.1f;
        AudioManager.playSFX?.Invoke(randomizedAudioClips[randomClip], sfxVolume);
    }
}