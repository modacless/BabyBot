using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    public float maxHealthPoint;
    private float currentHealthPoint;
    protected Rigidbody rb;

    public void TakeDamage(float damage)
    {
        currentHealthPoint -= damage;
        if (currentHealthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(GetComponent<Weapon>().stats.damage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealthPoint = maxHealthPoint;
    }
}
