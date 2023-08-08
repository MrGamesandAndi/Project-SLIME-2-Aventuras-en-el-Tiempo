using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource musicSource;
    private AudioSource otherMusicSource;
    private AudioSource sfxSource;
    private bool firstMusicSourceIsPlaying;

    public static AudioManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if(instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {

        //Create audio sources and saves them as reference
        musicSource = gameObject.AddComponent<AudioSource>();
        otherMusicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        //Loop the music tracks
        musicSource.loop = true;
        otherMusicSource.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        //Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : otherMusicSource;

        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1f)
    {
        AudioSource activeSource;
        //Determine which source is active
        if (firstMusicSourceIsPlaying)
        {
            activeSource = musicSource;
        }
        else
        {
            activeSource = otherMusicSource;
        }

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : otherMusicSource;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? otherMusicSource : musicSource;

        //Swap the source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        //Set the fields of the audio source, then start the crossfade couroutine
        newSource.clip = musicClip;
        newSource.Play();

        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        //Make sure the source is active and playing
        if(!activeSource.isPlaying)
        {
            activeSource.Play();
        }

        float t = 0f;

        //Fade out
        for (t = 0f; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        //Fade in
        for (t = 0f; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = t / transitionTime;
            yield return null;
        }
    }

    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0f;

        for (t = 0f; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = t / transitionTime;
            yield return null;
        }

        original.Stop();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySfx(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        otherMusicSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public bool CheckIfMusicIsPlaying()
    {
        if(!musicSource.isPlaying || !otherMusicSource.isPlaying)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
