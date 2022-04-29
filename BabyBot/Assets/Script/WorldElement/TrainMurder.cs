using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMurder : MonoBehaviour
{
    [SerializeField]
    private float trainDamage;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerInfo>().DamagePlayer((int)trainDamage);
        }

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemySensors>().TakeDamage(trainDamage);
            Debug.Log("test");
        }

    }

}
