using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLogic : MonoBehaviour
{
    
    private float actualTime;
    [SerializeField]
    private float timeBeforeRespawn;

    [SerializeField]
    private Vector3 trainDirection;
    [SerializeField]
    private float trainSpeed;
    [SerializeField]
    private float trainDamage;


    private void FixedUpdate()
    {
        transform.position = trainSpeed * trainDirection * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerLife>().TakeDamage(trainDamage);
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemySensors>().TakeDamage(trainDamage);
        }

        if(other.name == "Destroy")
        {

        }
        
    }
}
