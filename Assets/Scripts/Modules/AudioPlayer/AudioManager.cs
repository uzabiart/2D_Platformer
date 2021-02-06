using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public static Action<AudioClip, float> playSFX;

    private void OnEnable()
    {
        playSFX += PlaySound;
    }

    private void OnDisable()
    {
        playSFX -= PlaySound;
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        source.PlayOneShot(clip, volume);
    }
}
