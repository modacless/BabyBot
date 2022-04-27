using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : Weapon
{
    // Start is called before the first frame update
    protected override void Shoot()
    {
        for(int i = 0; i< stats.numberBulletSpawned; i++)
        {
            float randomDirection = Random.Range(transform.rotation.eulerAngles.y - stats.shootConeAngle, transform.rotation.eulerAngles.y + stats.shootConeAngle);

            GameObject myBullet = Instantiate(stats.bullet, firePoint.transform.position, firePoint.transform.rotation);
            myBullet.GetComponent<Bullet>().InitBullet(stats.bulletLifeTime, Time.time, stats.bulletSpeed, stats.bulletDamage, new Vector3(0,randomDirection,0));
            actualAmo--;

        }
    }
}
