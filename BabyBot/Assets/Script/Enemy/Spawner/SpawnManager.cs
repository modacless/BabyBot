using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeRespawn;
    [SerializeField]
    private float numberOfEnemyToSpawn;
    private float actualTimeBeforSpawn;

    //Tous les objets qui sont
    public List<GameObject> TypeSpawner;

    [SerializeField]
    private static int maxEnemy = 10;
    [SerializeField]
    public static int actualEnemy;

    [HideInInspector]public bool canSpawn;

    void Start()
    {
        actualTimeBeforSpawn = timeBeforeRespawn;
    }

    void FixedUpdate()
    {
        if (canSpawn)
        {
            actualTimeBeforSpawn += Time.deltaTime;

            if (actualTimeBeforSpawn >= timeBeforeRespawn)
            {
                actualTimeBeforSpawn = 0;

                for (int i = 0; i < numberOfEnemyToSpawn; i++)
                {
                    int randomEnemy = Random.Range(0, TypeSpawner.Count);
                    Instantiate(TypeSpawner[randomEnemy], transform.position, transform.rotation);
                }

            }
        }
    }
}
