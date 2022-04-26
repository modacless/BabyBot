using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AMInstance;
    public AudioSource SFXAudioSource;
    public AudioSource musicsAudioSource;

    [Header("Player")]

    public AudioClip[] woodFootstepArray;

    [Header("Musics")]

    public AudioClip[] runThemesArray;


    private void Awake()
    {
        if(AMInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            AMInstance = this;
        }
    }


    private void Start()
    {
        PlayMusics(runThemesArray[Random.Range(0, 3)], 1, 1);
    }


    public void PlaySFX(AudioClip _sound, float _volume, float _pitch)
    {
        SFXAudioSource.volume = _volume;
        SFXAudioSource.pitch = _pitch;
        SFXAudioSource.clip = _sound;

        SFXAudioSource.Play();
    }
    public void PlayMusics(AudioClip _sound, float _volume, float _pitch)
    {
        musicsAudioSource.volume = _volume;
        musicsAudioSource.pitch = _pitch;
        musicsAudioSource.clip = _sound;

        musicsAudioSource.Play();
    }

    public void StopSFX()
    {
        SFXAudioSource.Stop();
    }
    public void StopMusic()
    {
        musicsAudioSource.Stop();
    }
}
