using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public void Play(object value)
    {
        AudioSource audio = value as AudioSource;

        audio.Play();
    }

    public void Stop(object value)
    {
        AudioSource audio = value as AudioSource;

        audio.Stop();
    }
}
