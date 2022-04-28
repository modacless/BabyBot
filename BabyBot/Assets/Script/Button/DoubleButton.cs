using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButton : MonoBehaviour
{
    public UnityEventQueueSystem evenement;
    public button firstButton;
    public button secondButton;
    public bool twiceIsActive;

    void Update()
    {
        if(firstButton.isActivated && secondButton.isActivated)
        {
            firstButton.stayActive = true;
            secondButton.stayActive = true;
        }
    }
}
