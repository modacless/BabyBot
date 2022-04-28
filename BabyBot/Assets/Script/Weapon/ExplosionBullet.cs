using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public GameObject explosion;

    protected override void OnDestroy()
    {
        GameObject explos = Instantiate(explosion, transform.position, explosion.transform.rotation);
        explos.GetComponent<Bullet>().InitBullet(0, 0, 0, 0, Vector3.zero, fromPlayer);
    }
}
