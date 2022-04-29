using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainLogic : MonoBehaviour
{
    public AudioSource trainSource;

    private float actualTime = 0;
    [SerializeField]
    private float timeBeforeRespawn;

    private Vector3 trainDirection;
    [SerializeField]
    private float trainSpeed;

    [SerializeField]
    private GameObject model;
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private bool activeAtStart;
    [SerializeField]
    private bool stopTrainAtStart;

    public float speedAtStart = 1;

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

        if(speedAtStart == 1)
        {
            actualTime += Time.fixedDeltaTime;
            if (actualTime >= timeBeforeRespawn)
            {
                Spawn();
                actualTime = 0;
            }
        }
    }

    [ContextMenu("spawn train")]
    private void Spawn()
    {
        model.transform.position = spawnObject.transform.position;
        model.SetActive(true);
        //AudioManager.AMInstance.PlaySFX(AudioManager.AMInstance.trainWhistle, trainSource, 1);
    }

    public void StartTrain()
    {
        speedAtStart = 1;
        AudioManager.AMInstance.PlaySFX(AudioManager.AMInstance.trainWhistle, trainSource, 1);
    }
}
