using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : Bullet
{
    public AudioSource igniteSource;
    public AudioSource killingSource;


    public float turnSpeed;
    public float lifeTimeLightSaber;
    public float damageLightSaber;

    private float startTimeLightSaber = 0;

    protected override void Start()
    {
        startTimeLightSaber = Time.time;

        //Audio
        AudioManager Audio = AudioManager.AMInstance;
        float pitch = Random.Range(0.9f, 1.1f);
        Audio.PlaySFX(Audio.lightsaberIgnite, igniteSource, pitch);
        //----
    }

    protected override void Update()
    {
        if (Time.time > startTimeLightSaber + lifeTimeLightSaber) Destroy(gameObject);
    }

    protected override void FixedUpdate()
    {
        transform.Rotate(Vector3.right * (turnSpeed * Time.fixedDeltaTime));
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            //Audio
            AudioManager Audio = AudioManager.AMInstance;
            float pitch = Random.Range(0.8f, 1.2f);
            int index = Random.Range(0, (Audio.lightsaberImpactsArray.Length - 1));
            Audio.PlaySFX(Audio.lightsaberImpactsArray[index], killingSource, pitch);
            //----
            collider.GetComponent<EnemySensors>().TakeDamage(damageLightSaber, fromPlayer);
        }

        if (collider.tag == ("Bullet")) Destroy(gameObject);
    }
}
