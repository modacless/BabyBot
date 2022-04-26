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

    public int actualAmo;
    public bool isReloading = false;
    public bool CanShoot = true;

    private void Start()
    {
        actualAmo = stats.maxAmo;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            TryFire();
        }
        if (Input.GetKey(KeyCode.R))
        {
            TryReload();
        }
    }
    public virtual void TryFire()
    {
        if (actualAmo != 0 && !isReloading && CanShoot) Fire();
    }
    public virtual void Fire()
    {
        GameObject myBullet = Instantiate(bullet, EndOfGun.transform.position, transform.parent.transform.rotation);
        myBullet.GetComponent<Bullet>().direction = transform.parent.transform.forward;
        myBullet.GetComponent<Bullet>().speed = stats.BulletSpeed;
        myBullet.GetComponent<Bullet>().lifeTime = stats.BulletLifeTime;
        Debug.Log(transform.parent.transform.forward);
        actualAmo--;
        StartCoroutine(couldown());

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
        yield return new WaitForSeconds(stats.cadence);
        CanShoot = true;
    }
}
