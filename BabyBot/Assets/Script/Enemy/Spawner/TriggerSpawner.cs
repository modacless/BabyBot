using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    public SpawnManager[] arrayOfSpawner;

    private bool lockLogic;

    private void OnTriggerEnter(Collider other)
    {
        if (lockLogic == false)
        {
            if (other.tag == "Player")
            {
                lockLogic = true;

                foreach (SpawnManager spawner in arrayOfSpawner)
                {
                    spawner.canSpawn = true;
                }
            }
        }
    }

    public void StopAllSpawner()
    {
        foreach (SpawnManager spawner in arrayOfSpawner)
        {
            spawner.canSpawn = false;
        }
    }
}
