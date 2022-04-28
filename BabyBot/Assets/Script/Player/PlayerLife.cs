using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float lifePoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {

    }

    public void TakeDamage(float damage)
    {
        lifePoint -= damage;
        if(lifePoint <= 0)
        {
            Die();
        }
    }
}
