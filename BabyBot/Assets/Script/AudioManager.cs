using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AMInstance;
    public AudioSource SFXAudioSource;
    public AudioSource musicsAudioSource;

    [Header("Player")]

    public bool isOnWood = true;
    public AudioClip[] woodFootstepArray;
    public AudioClip[] carpetFootstepArray;


    [Header("Weapons")]

    public AudioClip[] regularShotsArray;
    public AudioClip[] waterGunShotsArray;

    public AudioClip[] assaultGunShotsArray;
    public AudioClip[] FireAssaultGunShotsArray;
    public AudioClip[] SparkleGunShotsArray;
    public AudioClip[] LightningGunShotsArray;

    public AudioClip[] ballLauncherShotsArray;
    public AudioClip[] waterBallLauncherShotsArray;

    public AudioClip[] waterImpactsArray;
    public AudioClip[] lightsaberImpactsArray;
    public AudioClip sharkAppear;
    public AudioClip sharkHit;
    public AudioClip lightsaberIgnite;


    [Header("Enemys")]

    public AudioClip[] enemyShotsArray;
    public AudioClip[] enemyAttacksArray;
    public AudioClip[] enemyDeathArray;


    [Header("World")]

    public AudioClip levier;
    public AudioClip Plane;
    public AudioClip pressurePlate;
    public AudioClip trainIdle;
    public AudioClip trainWhistle;


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
