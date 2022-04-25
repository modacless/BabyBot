using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public GameObject objToSpawn; //Object qui va remplir ma liste de pool object
    [SerializeField]
    private uint maxToFill; // Nombre d'object qu'on va remplir au début de la partie
    public List<GameObject> poolList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FillPool(maxToFill);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fill pool, dans le code, avec un max
    public void FillPool(uint maxList)
    {
        for(int i =0; i<maxList; i++)
        {
            GameObject objToAdd = Instantiate(objToSpawn, this.transform);
            objToAdd.SetActive(false);
            poolList.Add(objToAdd);

        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeSelf)
            {
                poolList[i].SetActive(true);
                poolList[i].transform.parent = null;
                return poolList[i];
            }
        }

        if(poolList.Count > 0)
        {
            GameObject objToAdd = Instantiate(poolList[0], this.transform.parent);
            objToAdd.SetActive(true);
            poolList.Add(objToAdd);
        }
        else
        {
            throw new ArgumentException("Pas d'object dans le pool d'objects");
        }
        return null;
    }

    public void DestroyObjectInPool(GameObject objectInPool)
    {
        objectInPool.SetActive(false);
        //Pas nécessaire
        objectInPool.transform.position = new Vector3(0, 0, 0);
        objectInPool.transform.parent = this.transform.parent;
    }
}
