using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateManager : MonoBehaviour
{
    public List<PressurePlate> myPressurePlates;
    public UnityEvent evenement;

    private bool lockEnter;

    private void Start()
    {
        myPressurePlates[0].canBeActivated = true;
    }
    void Update()
    {
        
            for(int i = 0; i < myPressurePlates.Count; i++)
            {
                if (myPressurePlates[i].canBeActivated)
                {
                    myPressurePlates[i].gameObject.SetActive(true);
                    PPActivated(i);
                }
                else
                {
                    myPressurePlates[i].gameObject.SetActive(false);
                }
            }
        
    }

    public void PPActivated(int i)
    {
        if(myPressurePlates[i].isActivated)
        {
            if(i < myPressurePlates.Count-1)
            {
                myPressurePlates[i+1].canBeActivated = true;
            }
            if(i > 0)
            {
                myPressurePlates[i-1].canBeActivated = false;
            }
            if(i == myPressurePlates.Count - 1)
            {
                if (lockEnter == false)
                {
                    evenement.Invoke();
                    myPressurePlates[i].lockSysteme = true;
                    lockEnter = true;
                }
            }
        }
        else
        {
            if (i < myPressurePlates.Count - 1)
            {
                myPressurePlates[i+1].canBeActivated = false;
            }
        }


    }
}
