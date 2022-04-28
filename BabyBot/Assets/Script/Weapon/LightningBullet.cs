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
    private List<GameObject> allBulletTargeted;

    private float distanceBetweenTarget;

    private WaitForFixedUpdate waitForFixed = new WaitForFixedUpdate();

    public int maxEnemyBounce;
    private int tuchEnemy = 0;


    protected override void Start()
    {
        base.Start();
        mainBulletCollider = GetComponent<SphereCollider>();
        baseSizeColider = mainBulletCollider.radius;
        allBulletTargeted = new List<GameObject>();

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
            if(collider.tag == "Enemy" && !secondBulletTarget && tuchEnemy< maxEnemyBounce && !allBulletTargeted.Contains(collider.gameObject)) //Start lightning
            {
                allBulletTargeted.Add(collider.gameObject);
                secondBulletTarget = collider.gameObject;
                SecondBullet.SetActive(true);
                distanceBetweenTarget = Vector3.Distance(transform.position, SecondBullet.transform.position);
                tuchEnemy++;
                MoveToNextTarget();
                collider.GetComponent<EnemySensors>().TakeDamage((int)(damage));
                Debug.Log(tuchEnemy + " " + SecondBullet.transform.position);
                Debug.Log(collider.GetComponent<EnemySensors>().lifePoint);
            }
        }

    }

    protected override void DestroyBullet()
    {
        Debug.Log("Destroy");
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
        //transform.position = secondBulletTarget.transform.position;
        mainBulletCollider.center = secondBulletTarget.transform.position - transform.position;
        mainBulletCollider.radius = baseSizeColider;
        secondBulletTarget = null;
        DestroyBullet();
    }

}