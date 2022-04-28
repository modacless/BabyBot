using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public GameObject explosion;

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
    }
}
