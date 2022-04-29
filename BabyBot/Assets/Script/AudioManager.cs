using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AMInstance;
    public AudioSource SFXAudioSource;
    public AudioSource musicsAudioSource;

    public float baseSFXVolume = 1;
    public float baseMusicVolume = 0;

    [Header("Player")]

    public bool isOnWood = true;
    public float woodFootstepVolume = 1;
    public AudioClip[] woodFootstepArray;
    public float carpetFootstepVolume = 1;
    public AudioClip[] carpetFootstepArray;


    [Header("Weapons")]

    public float regularShotsVolume = 1;
    public AudioClip[] regularShotsArray;
    public float waterGunShotsVolume = 1;
    public AudioClip[] waterGunShotsArray;

    public float assaultGunShotsVolume = 1;
    public AudioClip[] assaultGunShotsArray;
    public float fireAssaultGunShotsVolume = 1;
    public AudioClip[] FireAssaultGunShotsArray;
    public float sparkleGunShotsVolume = 1;
    public AudioClip[] SparkleGunShotsArray;
    public float lightningGunShotsVolume = 1;
    public AudioClip[] LightningGunShotsArray;

    public float ballLauncherShotsVolume = 1;
    public AudioClip[] ballLauncherShotsArray;
    public float waterBallLauncherShotsVolume = 1;
    public AudioClip[] waterBallLauncherShotsArray;

    public float waterImpactsVolume = 1;
    public AudioClip[] waterImpactsArray;
    public float lightsaberImpactsVolume = 1;
    public AudioClip[] lightsaberImpactsArray;
    public float sharkAppearVolume = 1;
    public AudioClip sharkAppear;
    public float sharkHitVolume = 1;
    public AudioClip sharkHit;
    public float lightsaberIgniteVolume = 1;
    public AudioClip lightsaberIgnite;


    [Header("Enemys")]

    public float enemyShotsVolume = 1;
    public AudioClip[] enemyShotsArray;
    public float enemyAttacksVolume = 1;
    public AudioClip[] enemyAttacksArray;
    public float enemyDeathVolume = 1;
    public AudioClip[] enemyDeathArray;


    [Header("World")]

    public float levierVolume = 1;
    public AudioClip levier;
    public float planeVolume = 1;
    public AudioClip Plane;
    public float pressurePlateVolume = 1;
    public AudioClip pressurePlate;
    public float trainIdleVolume = 1;
    public AudioClip trainIdle;
    public float trainWhistleVolume = 1;
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
        SFXAudioSource.volume = _volume * baseSFXVolume;
        SFXAudioSource.pitch = _pitch;

        SFXAudioSource.PlayOneShot(_sound);
    }
    public void PlayMusics(AudioClip _sound, float _volume, float _pitch)
    {
        musicsAudioSource.volume = _volume * baseMusicVolume;
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
