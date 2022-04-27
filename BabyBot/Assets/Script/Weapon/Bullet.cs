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

    public virtual void InitBullet(float _lifeTime, float _startTime, float _speed, float _damage, Vector3 _direction)
    {
        lifeTime = _lifeTime;
        startTime = _startTime;
        speed = _speed;
        damage = _damage ;
        direction = _direction;
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


