using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float lifeTime;
    [HideInInspector]
    protected float startTime;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public Vector3 direction;

    protected virtual void Start()
    {
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

    protected virtual void Update()
    {
        if (Time.time > startTime + lifeTime) Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage((int)damage);
        }
        if(collider.tag != ("Bullet")) Destroy(gameObject);
    }
    protected virtual void FixedUpdate()
    {
        transform.position += direction.normalized * speed * Time.fixedDeltaTime;
    }

    protected virtual void OnDestroy()
    {

    }
}


