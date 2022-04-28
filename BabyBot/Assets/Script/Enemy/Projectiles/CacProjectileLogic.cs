using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacProjectileLogic : ProjectileLogic
{
    public int damage;

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerInfo>().DamagePlayer(damage);

            Destroy(gameObject);
        }
    }
}
