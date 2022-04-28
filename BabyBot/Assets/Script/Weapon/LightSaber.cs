using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : Bullet
{
    public float turnSpeed;
    public float lifeTimeLightSaber;
    public float damageLightSaber;

    private float startTimeLightSaber = 0;

    protected override void Start()
    {
        startTimeLightSaber = Time.time;
    }

    protected override void Update()
    {
        if (Time.time > startTimeLightSaber + lifeTimeLightSaber) Destroy(gameObject);
    }

    protected override void FixedUpdate()
    {
        transform.Rotate(Vector3.right * (turnSpeed * Time.fixedDeltaTime));
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage(damageLightSaber, fromPlayer);
        }

        if (collider.tag == ("Bullet")) Destroy(gameObject);
    }
}
