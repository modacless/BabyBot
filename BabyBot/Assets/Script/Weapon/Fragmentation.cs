using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragmentation : Explosion
{
    [Header("Bullets Parameters")]
    public float bulletLifeTime;
    public float bulletSpeed;
    public float bulletDamage;

    [Space]
    public List<GameObject> instantiatedBulletsOnDead;

    private int cercleDivision;

    protected override void Start()
    {
        base.Start();
        cercleDivision = 360 / instantiatedBulletsOnDead.Count;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < instantiatedBulletsOnDead.Count; i++)
        {
            //int randomAngle = Random.Range(0, 360);
            int randomAngle = Random.Range(cercleDivision * i, cercleDivision * (i + 1));
            Vector3 randomDirection = Quaternion.Euler(0, randomAngle, 0) * transform.forward;

            Instantiate(instantiatedBulletsOnDead[i], transform.position, instantiatedBulletsOnDead[i].transform.rotation);
            instantiatedBulletsOnDead[i].GetComponent<Bullet>().InitBullet(bulletLifeTime, Time.time, bulletSpeed, bulletDamage, randomDirection);
        }
    }

    //Add raycast to detect wall on spawn ?
}
