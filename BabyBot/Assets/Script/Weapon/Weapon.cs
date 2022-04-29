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
    public GameObject firePoint;
    [HideInInspector] public PlayerMovement playerMovementScript;


    public AudioSource playerShotSource;
    public AudioClip[] currentShotsArray;


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
    public float magazineAmmo;

    
    [HideInInspector]public float actualAmo;
    protected float fireRateTimer = 0f;
    protected float reloadTimer = 0f;

    protected bool isPressingFire = false;
    protected bool isShooting = false;
    protected bool isReloading = false;

    [HideInInspector]public float _reloadTime;
    [HideInInspector]public float actualReloadTime;
    [HideInInspector]public bool _isReloading;

    public bool drawDebug;

    public List<UpgradeStruct> upgradeStruct;

    protected virtual void Start()
    {
        GetComponent<PlayerInput>().actions["Fire"].started += Fire;
        GetComponent<PlayerInput>().actions["Fire"].canceled += Fire;
        playerMovementScript = GetComponent<PlayerMovement>();

        actualBulletUsed = initialBullet;
        actualBulletUsed.transform.localScale = sizeBullet;
        actualAmo = magazineAmmo;

        currentShotsArray = AudioManager.AMInstance.waterGunShotsArray;

        //gainFireRate = stats.fireRate - stats.finalCadence;
        _reloadTime = reloadTime;
        _isReloading = isReloading;
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

    protected virtual void AnimationShoot()
    {
        if (isPressingFire /*&& playerMovementScript.isAiming*/ && !isReloading) playerMovementScript.playerAnimationsScript.Shoot(true);
        else playerMovementScript.playerAnimationsScript.Shoot(false);
    }

    protected virtual void Shoot()
    {
        GameObject myBullet = Instantiate(actualBulletUsed, firePoint.transform.position, transform.rotation * Quaternion.Euler(0,-90,0));
        myBullet.GetComponent<Bullet>().InitBullet(bulletLifeTime, Time.time, bulletSpeed, bulletDamage, transform.forward,this.gameObject);
        actualAmo--;

        //Audio
        float pitch = Random.Range(0.8f, 1.2f);
        int index = Random.Range(0, (currentShotsArray.Length - 1));
        AudioManager.AMInstance.PlaySFX(currentShotsArray[index], playerShotSource, pitch);
        //----
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
            playerMovementScript.playerAnimationsScript.Reload(true, reloadTime);
            _isReloading = true;

            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                ResetAmmoWeapon();
            }
        }

        actualReloadTime = reloadTimer;
    }

    public void ResetAmmoWeapon()
    {
        isReloading = false;
        playerMovementScript.playerAnimationsScript.Reload(false, reloadTime);
        _isReloading = false;
        reloadTimer = 0;
        actualAmo = magazineAmmo;
    }

    public void UpgradeWeapon(int upgradeToChose)
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

    protected virtual void Upgrade1()
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