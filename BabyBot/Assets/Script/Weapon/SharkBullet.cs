using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBullet : Bullet
{
    public AudioSource appearSource;
    public AudioSource eatSource;

    public LayerMask layerRaycast;
    public GameObject lightSaber;
    public float offsetYSpawn;
    [HideInInspector] public bool instantiateLightSaber = false;

    protected override void Start()
    {
        base.Start();

        //Audio
        AudioManager Audio = AudioManager.AMInstance;
        float pitch = Random.Range(0.8f, 1.2f);
        Audio.PlaySFX(Audio.sharkAppear, appearSource, pitch);
        //----
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (instantiateLightSaber)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 10f, out hit, layerRaycast))
            {
                GameObject lightSaberInstance = Instantiate(lightSaber, new Vector3(transform.position.x, hit.transform.position.y + offsetYSpawn, transform.position.z), lightSaber.transform.rotation);
                lightSaberInstance.GetComponent<Bullet>().InitBullet(0, 0, 0, 0, Vector3.zero, fromPlayer);
            }
        } 
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage(damage, fromPlayer);
            //Audio
            AudioManager Audio = AudioManager.AMInstance;
            float pitch = Random.Range(0.8f, 1.2f);
            Audio.PlaySFX(Audio.sharkHit, eatSource, pitch);
            //----
        }
        if (collider.tag != ("Bullet") && collider.tag != ("Enemy")) Destroy(gameObject);
    }
}
