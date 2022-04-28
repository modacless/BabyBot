using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLogic : MonoBehaviour
{
    
    private float actualTime = 0;
    [SerializeField]
    private float timeBeforeRespawn;

    [SerializeField]
    private Vector3 trainDirection;
    [SerializeField]
    private float trainSpeed;
    [SerializeField]
    private float trainDamage;

    [SerializeField]
    private GameObject model;
    [SerializeField]
    private GameObject spawnObject;

    private void Start()
    {
        model.SetActive(false);

    }
    private void FixedUpdate()
    {
        model.transform.position += trainSpeed * trainDirection * Time.fixedDeltaTime;

        actualTime += Time.fixedDeltaTime;
        if(actualTime >= timeBeforeRespawn)
        {
            Spawn();
            actualTime = 0;
        }
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

        if(other.name == "Model")
        {
            model.SetActive(false);
        }
        
    }

    private void Spawn()
    {
        model.transform.position = spawnObject.transform.position;
        model.SetActive(true);
    }
}
