using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGroup : MonoBehaviour
{
    #region Variable

    [Header ("For solo enemy")]
    public GameObject[] enemyRemaining;
    [Header ("For a group")]
    public TriggerGroup[] triggerToCheck;

    public List<GameObject> enemyToSpawn = new List<GameObject>();
    [Space]
    [SerializeField]private bool stay;

    [HideInInspector]public bool canSpawn;
    [HideInInspector] public bool allDead;
    private bool haveSpawn;

    private bool lockEnemyRemainingCheck;

    #endregion

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyToSpawn.Add(transform.GetChild(i).gameObject);
        }

        if (stay == false)
        {
            foreach (GameObject enemy in enemyToSpawn)
            {
                enemy.SetActive(false);
            }
        }

        if (enemyRemaining.Length == 0)
        {
            lockEnemyRemainingCheck = true;
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
        // Check if all enemy of this group are dead
        if (gameObject.transform.childCount == 0)
        {
            allDead = true;

        }

        // Check if enemy in the list are dead
        if (lockEnemyRemainingCheck == false)
        {
            if (enemyRemaining.Length == 0)
            {
                canSpawn = true;
            }
        }


        // Check if other group are dead
        if (triggerToCheck.Length > 0)
        {
            int numOfDone = 0;

            for (int i = 0; i < triggerToCheck.Length; i++)
            {
                if (triggerToCheck[i] != null && triggerToCheck[i].allDead == true)
                {
                    numOfDone += 1;
                }
            }

            if (numOfDone == triggerToCheck.Length)
            {
                canSpawn = true;
            }
        }
    }

}
