using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    public float turnSpeed;
    public float lifeTime;
    public float damage;

    private float startTime = 0;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > startTime + lifeTime) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.right * (turnSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage((int)damage);


            //Audio
            AudioManager Audio = AudioManager.AMInstance;
            float pitch = Random.Range(0.8f, 1.2f);
            int index = Random.Range(0, (Audio.lightsaberImpactsArray.Length - 1));
            Audio.PlaySFX(Audio.lightsaberImpactsArray[index], 1, pitch);
            //----
        }

        if (collider.tag == ("Bullet")) Destroy(gameObject);
    }
}
