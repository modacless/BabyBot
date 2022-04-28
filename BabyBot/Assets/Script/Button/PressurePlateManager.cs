using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateManager : MonoBehaviour
{
    public List<PressurePlate> myPressurePlates;
    public UnityEvent evenement;

    private void Start()
    {
        myPressurePlates[0].canBeActivated = true;
    }
    void Update()
    {
        
        for(int i = 0; i < myPressurePlates.Count; i++)
        {
            if (myPressurePlates[i].isActivated || myPressurePlates[i].canBeActivated)
            {
                PPActivated(i, myPressurePlates[i].isActivated);
            }
            if(i<0)
            {
                if()
            }

        }
    }

    public void PPActivated(int i, bool a)
    {
        if(a)
        {
            if(i < myPressurePlates.Count-1)
            {
                myPressurePlates[i].canBeActivated = true;
            }
        }
        else
        {
            if (i < myPressurePlates.Count - 1)
            {
                myPressurePlates[i].canBeActivated = false;
            }
        }


    }
}
