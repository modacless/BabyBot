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
    [HideInInspector]
    public GameObject fromPlayer;

    protected virtual void Start()
    {
        startTime = Time.time;
    }

    public virtual void InitBullet(float _lifeTime, float _startTime, float _speed, float _damage, Vector3 _direction, GameObject from)
    {
        lifeTime = _lifeTime;
        startTime = _startTime;
        speed = _speed;
        damage = _damage ;
        direction = _direction;
        fromPlayer = from;
    }

    protected virtual void Update()
    {
        if (Time.time > startTime + lifeTime) Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage((int)damage, fromPlayer);
            //Debug.Log((int)damage);
        }
        if(collider.tag != ("Bullet") && collider.tag != ("Player") && collider.tag != ("IgnorePlayerBullet")) Destroy(gameObject);
    }
    protected virtual void FixedUpdate()
    {
        transform.position += direction.normalized * speed * Time.fixedDeltaTime;

    }

    protected virtual void OnDestroy()
    {
        
    }

}


