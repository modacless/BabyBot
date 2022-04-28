using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    public GameObject initialBullet;
    protected GameObject actualBulletUsed;
    [SerializeField]
    protected GameObject firePoint;
    private PlayerMovement playerMovementScript;

    [System.Serializable]
    public struct UpgradeStruct
    {
        //WEAPON DATA
        [SerializeField]
        public Vector3 sizeBullet;
        [SerializeField]
        public float reloadTime;
        [SerializeField]
        public float fireRate;
        [SerializeField]
        public float bulletDamage;
        [SerializeField]
        public float bulletLifeTime;
        [SerializeField]
        public float bulletSpeed;
        [SerializeField]
        public int magazineAmmo;

        public GameObject actualBulletUsed;
    }

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

    protected int actualAmo;
    protected float fireRateTimer = 0f;
    protected float reloadTimer = 0f;

    protected bool isPressingFire = false;
    protected bool isShooting = false;
    protected bool isReloading = false;

<<<<<<< HEAD
    public bool drawDebug;
=======
    private float actualCadence;
    private bool needToPreHeated = false;
//    private bool isLaser = false;
    private float preHeatedTime;
    private float gainCadence;
//    LineRenderer laserLine;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
 //   private float fireTimer;
>>>>>>> Tim

    public List<UpgradeStruct> upgradeStruct;

    protected virtual void Start()
    {
        GetComponent<PlayerInput>().actions["Fire"].started += Fire;
        GetComponent<PlayerInput>().actions["Fire"].canceled += Fire;
        playerMovementScript = GetComponent<PlayerMovement>();
<<<<<<< HEAD

        actualBulletUsed = initialBullet;
        actualBulletUsed.transform.localScale = sizeBullet;
        actualAmo = magazineAmmo;

        //gainFireRate = stats.fireRate - stats.finalCadence;
=======
        actualCadence = stats.basicCadence;
        actualAmo = stats.maxAmo;
        isReloading = false;
        CanShoot = true;
        gainCadence = stats.basicCadence - stats.finalCadence;
 //       laserLine = GetComponent<LineRenderer>();
>>>>>>> Tim
    }
    protected virtual void Update()
    {
        TryShoot();
        FireRate();
        Reload();
        AnimationShoot();
    }

    protected virtual void OnEnable()
    {
        drawDebug = true;
    }

    protected virtual void OnDisable()
    {
        drawDebug = false;
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

    private void AnimationShoot()
    {
        if (isPressingFire /*&& playerMovementScript.isAiming*/ && !isReloading) playerMovementScript.playerAnimationsScript.Shoot(true);
        else playerMovementScript.playerAnimationsScript.Shoot(false);
    }

    protected virtual void Shoot()
    {
        GameObject myBullet = Instantiate(actualBulletUsed, firePoint.transform.position, transform.rotation);
        myBullet.GetComponent<Bullet>().InitBullet(bulletLifeTime, Time.time, bulletSpeed, bulletDamage, transform.forward);
        actualAmo--;
    }

    protected void TryShoot()
    {
        if (!isReloading)
        {
            if (isPressingFire && !isShooting /*&& playerMovementScript.isAiming*/)
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

    protected void UpgradeWeapon(int upgradeToChose)
    {
        switch (upgradeToChose)
        {
            case 0:
                Upgrade1();
                break;
            case 1:
                Upgrade2();
                break;

            case 2:
                Upgrade3();
                break;
        }
    }
<<<<<<< HEAD

    protected virtual void Upgrade1()
=======
  
    public virtual void TryReload()
>>>>>>> Tim
    {
        TranslateStructToUpgrade(0);
    }

    protected virtual void Upgrade2()
    {
        TranslateStructToUpgrade(1);
    }


    protected virtual void Upgrade3()
    {
        TranslateStructToUpgrade(2);
    }

    private void TranslateStructToUpgrade(int upgradeStat)
    {
        sizeBullet = upgradeStruct[upgradeStat].sizeBullet;
        reloadTime = upgradeStruct[upgradeStat].reloadTime;
        fireRate = upgradeStruct[upgradeStat].fireRate;
        bulletDamage = upgradeStruct[upgradeStat].bulletDamage;
        bulletLifeTime = upgradeStruct[upgradeStat].bulletLifeTime;
        bulletSpeed = upgradeStruct[upgradeStat].bulletSpeed;
        magazineAmmo = upgradeStruct[upgradeStat].magazineAmmo;

        if (upgradeStruct[upgradeStat].actualBulletUsed != null)
        {
            actualBulletUsed = upgradeStruct[upgradeStat].actualBulletUsed;
        }

    }

}