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

    //WEAPON DATA
    [SerializeField]
    protected Vector3 sizeBullet;
    [SerializeField]
    protected float reloadTime;
    [SerializeField]
    protected float fireRate;
    [SerializeField]
    protected float bulletDamage;
    [SerializeField]
    protected float bulletLifeTime;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected int magazineAmmo;


    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();

        actualBulletUsed = allBulletType[0];
        actualBulletUsed.transform.localScale = sizeBullet;
        actualAmo = magazineAmmo;

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
        GameObject myBullet = Instantiate(actualBulletUsed, firePoint.transform.position, transform.rotation);
        myBullet.GetComponent<Bullet>().InitBullet(bulletLifeTime, Time.time, bulletSpeed, bulletDamage, transform.forward);
        Debug.Log(transform.rotation.eulerAngles);
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

    protected virtual void FireRate()
    {
        if (isShooting)
        {
            fireRateTimer += Time.deltaTime;
            if (fireRateTimer >= fireRate)
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
            if (reloadTimer >= reloadTime)
            {
                isReloading = false;
                reloadTimer = 0;
                actualAmo = magazineAmmo;
            }
        }
    }

}