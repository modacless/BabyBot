using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    public float turnSpeed;
    public float lifeTime;

    private float startTime = 0;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > startTime + lifeTime) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.right * (turnSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == ("Untagged") || collider.tag == ("Bullet")) Destroy(gameObject);
    }
}
