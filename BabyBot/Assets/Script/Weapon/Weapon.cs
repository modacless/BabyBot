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
    private PlayerMovement playerMovementScript;

    public int actualAmo;

    private float fireRateTimer = 0f;
    private float gunFireRate;

    private bool isPressingFire = false;
    private bool isShooting = false;
    
    private bool isReloading = false;
    private bool CanShoot = true;
    private bool needToPreHeated = false;
    private float preHeatedTime;
    private float gainFireRate;

    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();

        gunFireRate = stats.fireRate;
        actualAmo = stats.magazineAmmo;
        gainFireRate = stats.fireRate - stats.finalCadence;
    }
    private void Update()
    {
        Shoot();
        Cadence();
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
        if (isPressingFire && !isShooting && playerMovementScript.isAiming)
        {
            //if (stats.needPreheated) FireMiniGun();

            isShooting = true;
            GameObject myBullet = Instantiate(stats.bullet, firePoint.transform.position, firePoint.transform.rotation);
            myBullet.GetComponent<Bullet>().direction = firePoint.transform.forward;
            myBullet.GetComponent<Bullet>().speed = stats.bulletSpeed;
            myBullet.GetComponent<Bullet>().lifeTime = stats.bulletLifeTime;
            myBullet.GetComponent<Bullet>().damage = stats.bulletDamage;
            actualAmo--;

            //StartCoroutine(couldown());
        }
    }

    private void Cadence()
    {
        if (isShooting)
        {
            fireRateTimer += Time.deltaTime;
            if (fireRateTimer > gunFireRate)
            {
                isShooting = false;
                fireRateTimer = 0;
            }
        }
    }

    public virtual void FireMiniGun()
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
    }
}