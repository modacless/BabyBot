using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolDie : MonoBehaviour
{

    public PoolManager plManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            plManager.GetPoolObject();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(plManager.poolList.Count > 0)
            {
                for (int i = plManager.poolList.Count - 1; i >= 0; i--)
                {
                    if (plManager.poolList[i].activeSelf)
                    {
                        plManager.DestroyObjectInPool(plManager.poolList[i]);
                        break;
                    }

                }
            }
            
        }*/

    }
}
