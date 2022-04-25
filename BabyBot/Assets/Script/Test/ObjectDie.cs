using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDie : MonoBehaviour
{
    public PoolManager pl;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            pl.DestroyObjectInPool(this.gameObject);
        }
    }
}
