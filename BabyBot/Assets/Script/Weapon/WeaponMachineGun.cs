using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponMachineGun : Weapon
{

    [SerializeField]
    private float fireAngle;

    protected override void Shoot()
    {
        float randomAngle = Random.Range(-fireAngle, fireAngle);
        Vector3 randomFire = Quaternion.Euler(0, randomAngle, 0) * transform.forward;
        GameObject myBullet = Instantiate(actualBulletUsed, firePoint.transform.position, transform.rotation);
        myBullet.GetComponent<Bullet>().InitBullet(bulletLifeTime, Time.time, bulletSpeed, bulletDamage, randomFire);
        actualAmo--;
    }

    private void OnDrawGizmos()
    {
        if (drawDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 360 + fireAngle, 0) * transform.forward * 10f);
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 360 - fireAngle, 0) * transform.forward * 10f);
        }

    }

    protected override void Upgrade1()
    {

    }

    protected override void Upgrade2()
    {

    }


    protected override void Upgrade3()
    {

    }
}
