using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : Bullet
{
    protected override void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage(damage,fromPlayer);
        }

        if (collider.tag != ("Bullet") && collider.tag != "Enemy")
            DestroyBullet();
    }

    protected virtual void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
