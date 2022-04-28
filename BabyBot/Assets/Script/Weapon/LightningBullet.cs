using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : PiercingBullet
{

    public GameObject SecondBullet;

    public float maxSizeColider;
    private float baseSizeColider;
    public float sizePerSecond;

    public LightningLogic secondBulletLogic;

    private bool mainBulletDie = false;

    private SphereCollider mainBulletCollider;

    private GameObject secondBulletTarget;
    private float distanceBetweenTarget;

    private WaitForFixedUpdate waitForFixed = new WaitForFixedUpdate();

    protected override void Start()
    {
        base.Start();
        mainBulletCollider = GetComponent<SphereCollider>();
        baseSizeColider = mainBulletCollider.radius;

    }

    protected override void Update()
    {
        if (!mainBulletDie)
        {
            if (Time.time > startTime + lifeTime) DestroyBullet();
        }

    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (!mainBulletDie)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemySensors>().TakeDamage((int)damage);
                DestroyBullet();
            }

            if (collider.tag != ("Bullet") && collider.tag != "Enemy")
                DestroyBullet();
        }
        else
        {
            if(collider.tag == "Enemy" && !secondBulletTarget) //Start lightning
            {
                secondBulletTarget = collider.gameObject;
                SecondBullet.SetActive(true);
                distanceBetweenTarget = Vector3.Distance(transform.position, SecondBullet.transform.position);
                //secondBulletLogic.GetComponent<LightningLogic>().goalPosition = secondBulletTarget.transform.position;
                MoveToNextTarget();
            }
        }

    }

    protected override void DestroyBullet()
    {
        mainBulletDie = true;
        speed = 0;
        StartCoroutine(ExtendCollider());
    }

    private IEnumerator ExtendCollider()
    {
        GetComponent<MeshRenderer>().enabled = false;
        while(mainBulletCollider.radius < maxSizeColider)
        {
            mainBulletCollider.radius += sizePerSecond * Time.fixedDeltaTime;
            yield return waitForFixed;
        }

        if(secondBulletTarget == null)
        {
            Destroy(this.gameObject);
        }

    }

    public void MoveToNextTarget()
    {
        StopCoroutine(ExtendCollider());
        mainBulletCollider.center = transform.localPosition - secondBulletTarget.transform.position;
        mainBulletCollider.radius = baseSizeColider;
        SecondBullet.transform.localPosition = transform.localPosition - secondBulletTarget.transform.position;
        DestroyBullet();
    }
}
