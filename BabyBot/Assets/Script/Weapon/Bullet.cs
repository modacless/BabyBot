using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    private Rigidbody selfRigidbody;
    [HideInInspector]
    public float lifeTime;
    [HideInInspector]
    private float startTime;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public Vector3 direction;

    private void Start()
    {
        selfRigidbody = GetComponent<Rigidbody>();
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time > startTime + lifeTime) Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag != ("Bullet")) Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        selfRigidbody.velocity = direction * speed;
    }
}


