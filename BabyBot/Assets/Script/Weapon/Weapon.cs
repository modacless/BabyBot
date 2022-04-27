using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private ScriptableWeapon stats;
    [SerializeField]
    protected GameObject firePoint;
    private GameObject gunBullet;
    private PlayerMovement playerMovementScript;

    public int actualAmo;
    private float fireRateTimer = 0f;
    private float reloadTimer = 0f;

    private bool isPressingFire = false;
    private bool isShooting = false;
    private bool isReloading = false;

    /*private bool needToPreHeated = false;
    private float preHeatedTime;
    private float gainFireRate;*/

    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();

        gunBullet = stats.bullet;
        gunBullet.transform.localScale = stats.sizeBullet;
        actualAmo = stats.magazineAmmo;
        //gainFireRate = stats.fireRate - stats.finalCadence;
    }
    private void Update()
    {
        Shoot();
        FireRate();
        Reload();
    }

    public virtual void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressingFire = true;
        }
        if (context.canceled)
        {
            isPressingFire = false;
        }
    }

    private void Shoot()
    {
        if (!isReloading)
        {
            if (isPressingFire && !isShooting && playerMovementScript.isAiming)
            {
                //if (stats.needPreheated) FireMiniGun();

                isShooting = true;
                GameObject myBullet = Instantiate(gunBullet, firePoint.transform.position, firePoint.transform.rotation);
                myBullet.GetComponent<Bullet>().direction = firePoint.transform.forward;
                myBullet.GetComponent<Bullet>().speed = stats.bulletSpeed;
                myBullet.GetComponent<Bullet>().lifeTime = stats.bulletLifeTime;
                myBullet.GetComponent<Bullet>().damage = stats.bulletDamage;
                actualAmo--;

                //StartCoroutine(couldown());
            }
        } 
    }

    private void FireRate()
    {
        if (isShooting)
        {
            fireRateTimer += Time.deltaTime;
            if (fireRateTimer >= stats.fireRate)
            {
                isShooting = false;
                fireRateTimer = 0;
            }
        }
    }

    private void Reload()
    {
        if (actualAmo <= 0)
        {
            isReloading = true;

            reloadTimer += Time.deltaTime;
            if (reloadTimer >= stats.reloadTime)
            {
                isReloading = false;
                reloadTimer = 0;
                actualAmo = stats.magazineAmmo;
            }
        }
    }


    /*public virtual void FireMiniGun()
    {
        if (needToPreHeated) preHeatedTime = 0;
        needToPreHeated = false;
        preHeatedTime += Time.deltaTime;
        if(gunFireRate != stats.finalCadence) gunFireRate = stats.fireRate - gainFireRate * stats.preheatedCurve.Evaluate(preHeatedTime);

    }
    public virtual void TryReload()
    {
        if (actualAmo != stats.magazineAmmo && !isReloading) StartCoroutine(Reload());
    }
    public virtual IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(stats.reloadTime);
        actualAmo = stats.magazineAmmo;
        isReloading = false;
    }
    public virtual IEnumerator couldown()
    {
        CanShoot = false;
        yield return new WaitForSeconds(gunFireRate);
        CanShoot = true;
    }*/
}