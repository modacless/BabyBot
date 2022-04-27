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

    public int actualAmo;
    public bool isReloading = false;
    public bool CanShoot = true;
    private bool isShooting = false;

    public float actualCadence;
    private bool needToPreHeated = false;
//    private bool isLaser = false;
    private float preHeatedTime;
    private float gainCadence;
//    LineRenderer laserLine;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
 //   private float fireTimer;

    private void Start()
    {
        actualCadence = stats.basicCadence;
        actualAmo = stats.maxAmo;
        isReloading = false;
        CanShoot = true;
        gainCadence = stats.basicCadence - stats.finalCadence;
 //       laserLine = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        /*if (Input.GetKey(KeyCode.Mouse0))
        {
            TryFire();
        }
        if (Input.GetKey(KeyCode.R))
        {
            TryReload();
        }*/

        if (isShooting)
        {
            if (stats.needPreheated) FireMiniGun();
            //if (stats.isLaser) FireLaser();
            if (actualAmo != 0 && !isReloading && CanShoot)
            {
                Fire();
            }
        }
    }
    public virtual void TryFire(InputAction.CallbackContext context)
    {
        //isShooting = true;

        if (context.phase == InputActionPhase.Started)
        {
            isShooting = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            isShooting = false;
        }
    }

    public void StopFire(InputAction.CallbackContext context)
    {
        isShooting = false;
    }
    public virtual void Fire()
    {
        GameObject myBullet = Instantiate(bullet, EndOfGun.transform.position, transform.parent.transform.rotation);
        myBullet.GetComponent<Bullet>().direction = transform.parent.transform.forward;
        myBullet.GetComponent<Bullet>().speed = stats.bulletSpeed;
        myBullet.GetComponent<Bullet>().lifeTime = stats.bulletLifeTime;
        myBullet.GetComponent<Bullet>().damage = stats.bulletDamage;
        Debug.Log(transform.parent.transform.forward);
        actualAmo--;
        StartCoroutine(couldown());

    }
    public virtual void FireMiniGun()
    {
        if (needToPreHeated) preHeatedTime = 0;
        needToPreHeated = false;
        preHeatedTime += Time.deltaTime;
        if (actualCadence != stats.finalCadence) actualCadence = stats.basicCadence - gainCadence * stats.preheatedCurve.Evaluate(preHeatedTime);
    }

   /* public void FireLaser()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer > fireRate)
        {
            laserLine.SetPosition(0, EndOfGun.transform.position);
            Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0);
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, EndOfGun.transform.forward, out hit))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.collider.tag == "Ennemy")
                {
                    hit.
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (EndOfGun.transform.forward * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.05f);
        laserLine.enabled = false;
    }*/

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