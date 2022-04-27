using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    public ScriptableWeapon stats;
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected GameObject EndOfGun;
    [SerializeField]
    protected GameObject gun;

    private PlayerMovement playerMovementScript;

    public int actualAmo;
    public bool isReloading = false;
    public bool CanShoot = true;

    private float actualCadence;
    private bool needToPreHeated = false;
//    private bool isLaser = false;
    private float preHeatedTime;
    private float gainCadence;
//    LineRenderer laserLine;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
 //   private float fireTimer;

    private bool isFire = false;

    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        actualCadence = stats.basicCadence;
        actualAmo = stats.maxAmo;
        isReloading = false;
        CanShoot = true;
        gainCadence = stats.basicCadence - stats.finalCadence;
 //       laserLine = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        if (isFire && playerMovementScript.isAiming)
        {
            if (stats.needPreheated) FireMiniGun();

            GameObject myBullet = Instantiate(bullet, EndOfGun.transform.position, gun.transform.rotation);
            myBullet.GetComponent<Bullet>().direction = gun.transform.forward;
            myBullet.GetComponent<Bullet>().speed = stats.bulletSpeed;
            myBullet.GetComponent<Bullet>().lifeTime = stats.bulletLifeTime;
            myBullet.GetComponent<Bullet>().damage = stats.bulletDamage;
            Debug.Log(gun.transform.forward);
            actualAmo--;
            StartCoroutine(couldown());
        }
    }

    public virtual void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFire = true;
        }
        if (context.canceled)
        {
            isFire = false;
        }
    }

    public virtual void FireMiniGun()
    {
        if (needToPreHeated) preHeatedTime = 0;
        needToPreHeated = false;
        preHeatedTime += Time.deltaTime;
        if(actualCadence != stats.finalCadence) actualCadence = stats.basicCadence - gainCadence * stats.preheatedCurve.Evaluate(preHeatedTime);

    }
    public virtual void TryReload()
    {
        if (actualAmo != stats.maxAmo && !isReloading) StartCoroutine(Reload());
    }
    public virtual IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(stats.reloadTime);
        actualAmo = stats.maxAmo;
        isReloading = false;
    }
    public virtual IEnumerator couldown()
    {
        CanShoot = false;
        yield return new WaitForSeconds(actualCadence);
        CanShoot = true;
    }
}