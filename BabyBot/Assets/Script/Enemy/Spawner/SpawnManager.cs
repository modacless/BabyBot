using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeRespawn;
    private float actualTimeBeforSpawn;

    //Tous les objets qui sont
    public List<GameObject> TypeSpawner;

    [SerializeField]
    private static int maxEnemy = 10;
    [SerializeField]
    public static int actualEnemy;

    void Start()
    {
        actualTimeBeforSpawn = timeBeforeRespawn;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualTimeBeforSpawn += Time.fixedDeltaTime;
        if (actualTimeBeforSpawn >= timeBeforeRespawn)
        {
            int randomEnemy = Random.Range(0, TypeSpawner.Count);
            Instantiate(TypeSpawner[randomEnemy]);

            actualTimeBeforSpawn = 0;
        }


    }
}
