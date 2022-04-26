using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AMInstance;
    AudioSource audioSource;

    [Header("Player")]

    public AudioClip[] woodFootstepArray;


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

        audioSource = this.GetComponent<AudioSource>();
    }

    



    public void PlaySFX(AudioClip _sound, float _volume, float _pitch)
    {
        audioSource.volume = _volume;
        audioSource.pitch = _pitch;
        audioSource.clip = _sound;

        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
