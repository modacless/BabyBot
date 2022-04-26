using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    public ScriptableWeapon stats;
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected GameObject EndOfGun;

    private bool fire = false;

    public int actualAmo;
    public bool isReloading = false;
    public bool CanShoot = true;

    private float actualCadence;
    private bool needToPreHeated = true;
    private float preHeatedTime;
    private float gainCadence;

    private void Start()
    {
        actualCadence = stats.basicCadence;
        actualAmo = stats.maxAmo;
        isReloading = false;
        CanShoot = true;
        gainCadence = stats.basicCadence - stats.finalCadence;
    }
    public void StartFire()
    {
        fire = true;
    }
    public void StopFire()
    {
        fire = false;
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
        if (fire) TryFire();
    }
    public virtual void TryFire()
    {
        if (actualAmo != 0 && !isReloading && CanShoot) Fire();
    }
    public virtual void Fire()
    {
        if(stats.needPreheated) FireMiniGun();

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