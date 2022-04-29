using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLogic : MonoBehaviour
{
    
    private float actualTime = 0;
    [SerializeField]
    private float timeBeforeRespawn;

    private Vector3 trainDirection;
    [SerializeField]
    private float trainSpeed;
    [SerializeField]
    private float trainDamage;

    [SerializeField]
    private GameObject model;
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private bool activeAtStart;
    [SerializeField]
    private bool stopTrainAtStart;

    private float speedAtStart;

    private void Start()
    {
        model.SetActive(activeAtStart);
        trainDirection = -model.transform.right;
        if (stopTrainAtStart)
        {
            speedAtStart = 0;
        }

    }
    private void FixedUpdate()
    {
        model.transform.position += trainSpeed * trainDirection * Time.fixedDeltaTime * speedAtStart;

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

        if(other.tag == "Train")
        {
            Debug.Log("test");
            model.SetActive(false);
        }
        
    }

    private void Spawn()
    {
        model.transform.position = spawnObject.transform.position;
        model.SetActive(true);
    }

    public void StartTrain()
    {
        speedAtStart = 1;
    }
}
