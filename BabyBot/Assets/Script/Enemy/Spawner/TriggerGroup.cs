using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGroup : MonoBehaviour
{
    public GameObject[] enemyRemaining;
    public GameObject[] enemyToSpawn;
    public TriggerGroup triggerToCheck;

    public bool canSpawn;
    private bool haveSpawn;


    private void Start()
    {
        foreach (GameObject enemy in enemyToSpawn)
        {
            enemy.SetActive(false);
        }
    }

    private void Update()
    {
        if (canSpawn == false)
        {
            CheckIfCanSpawn();
        }
        else if (haveSpawn == false)
        {
            ActiveEnemy();
        }

    }

    private void ActiveEnemy()
    {
        foreach (GameObject enemy in enemyToSpawn)
        {
            enemy.SetActive(true);
        }

        haveSpawn = true;
    }

    private void CheckIfCanSpawn()
    {
        if (enemyRemaining.Length == 0)
        {
            canSpawn = true;
        }
        
        if (triggerToCheck.canSpawn)
        {
            canSpawn = true;
        }
    }

}
