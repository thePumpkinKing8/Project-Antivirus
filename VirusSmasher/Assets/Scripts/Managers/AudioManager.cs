using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : Singleton<AudioManager>
{
    public float musicVolume = .4f;
    public float playerVolume = .4f;
    public float enemyVolume = .4f;
    public float otherVolume = .4f;

    public AudioMixer audioMixer; 
    public AudioSource musicSource;


    public List<AudioSource> playerSources;
    public List<AudioSource> enemySources;
    public List<AudioSource> otherSources;

    protected override void Awake()
    {
        musicSource.volume = musicVolume; 
        foreach(AudioSource source in playerSources)
        {
            source.volume = playerVolume;
        }
        foreach (AudioSource source in enemySources)
        {
            source.volume = enemyVolume;
        }
        foreach (AudioSource source in otherSources)
        {
            source.volume = otherVolume;
        }
    }

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

    public void ChangeMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayerPlay(AudioClip clip)
    {
        var played = false;
        foreach(AudioSource source in playerSources)
        {
            if (source.isPlaying)
            {
                if(source.clip == clip)
                {
                    if(!played)
                    {
                        source.Play();
                        played = true;
                        return;
                    }
                    else
                        source.Stop();
                }
            }
               
            else if(played == false)
            {
                source.clip = clip;
                source.Play();
                played = true;
            }
        }
        if(!played)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.volume = playerVolume;
            playerSources.Add(newSource);
            newSource.Play();
        }
    }
    public void EnemyPlay(AudioClip clip)
    {
        var played = false;
        foreach (AudioSource source in enemySources)
        {
            if (source.isPlaying)
            {
                if (source.clip == clip)
                {
                    if (!played)
                    {
                        source.Play();
                        played = true;
                        return;
                    }
                    else
                        source.Stop();
                }
            }

            else if (played == false)
            {
                source.clip = clip;
                source.Play();
                played = true;
            }
        }
        if (!played)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.volume = playerVolume;
            enemySources.Add(newSource);
            newSource.Play();
        }
    }

    public void otherPlay(AudioClip clip)
    {
        var played = false;
        foreach (AudioSource source in otherSources)
        {
            if (source.isPlaying)
            {
                if (source.clip == clip)
                {
                    if (!played)
                    {
                        source.Play();
                        return;
                    }
                    else
                        source.Stop();
                }
            }

            else if (played == false)
            {
                source.clip = clip;
                source.Play();
                played = true;
            }
        }
        if (!played)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.volume = otherVolume;
            otherSources.Add(newSource);
            newSource.Play();
        }
    }
}
