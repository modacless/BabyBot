using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    public GameObject[] allBulletType;
    private GameObject actualBulletUsed;

    [SerializeField]
    protected GameObject firePoint;

    private PlayerMovement playerMovementScript;

    public int actualAmo;
    protected float fireRateTimer = 0f;
    protected float reloadTimer = 0f;

    protected bool isPressingFire = false;
    protected bool isShooting = false;
    protected bool isReloading = false;

    public ScriptableWeapon stats;

    /*private bool needToPreHeated = false;
    private float preHeatedTime;
    private float gainFireRate;*/

    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();

        actualBulletUsed = stats.bullet;
        actualBulletUsed.transform.localScale = stats.sizeBullet;
        actualAmo = stats.magazineAmmo;
        //gainFireRate = stats.fireRate - stats.finalCadence;
    }
    private void Update()
    {
        TryShoot();
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

    protected virtual void Shoot()
    {
        GameObject myBullet = Instantiate(actualBulletUsed, firePoint.transform.position, firePoint.transform.rotation);
        myBullet.GetComponent<Bullet>().InitBullet(stats.bulletLifeTime, Time.time, stats.bulletSpeed, stats.bulletDamage, transform.rotation.eulerAngles);
        actualAmo--;
    }

    private void TryShoot()
    {
        if (!isReloading)
        {
            if (isPressingFire && !isShooting && playerMovementScript.isAiming)
            {
                isShooting = true;
                Shoot();
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

}