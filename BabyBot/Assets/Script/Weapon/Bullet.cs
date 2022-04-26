using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody selfRigidbody;
    public float lifeTime;
    private float startTime;
    public float speed;
    public float damage;
    public Vector3 direction;

    private void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time > startTime + lifeTime) Destroy(transform.gameObject);
    }
    private void FixedUpdate()
    {
        selfRigidbody.velocity = direction * speed;
    }
}
