using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponMachineGun : Weapon
{

    [SerializeField]
    private float fireAngle;


    protected override void Start()
    {
        base.Start();

        currentShotsArray = AudioManager.AMInstance.assaultGunShotsArray;
    }


    protected override void Shoot()
    {
        float randomAngle = Random.Range(-fireAngle, fireAngle);
        Vector3 randomFire = Quaternion.Euler(0, randomAngle, 0) * transform.forward;
        GameObject myBullet = Instantiate(actualBulletUsed, firePoint.transform.position, transform.rotation);
        myBullet.GetComponent<Bullet>().InitBullet(bulletLifeTime, Time.time, bulletSpeed, bulletDamage, randomFire);
        actualAmo--;
        Upgrade3();
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
        base.Upgrade1();

        currentShotsVolume = AudioManager.AMInstance.fireAssaultGunShotsVolume;
        currentShotsArray = AudioManager.AMInstance.FireAssaultGunShotsArray;
    }

    protected override void Upgrade2()
    {
        base.Upgrade2();

        currentShotsVolume = AudioManager.AMInstance.sparkleGunShotsVolume;
        currentShotsArray = AudioManager.AMInstance.SparkleGunShotsArray;
    }

    protected override void Upgrade3()
    {
        base.Upgrade3();

        currentShotsVolume = AudioManager.AMInstance.lightningGunShotsVolume;
        currentShotsArray = AudioManager.AMInstance.LightningGunShotsArray;
    }


}
