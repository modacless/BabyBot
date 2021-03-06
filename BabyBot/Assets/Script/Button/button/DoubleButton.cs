using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleButton : MonoBehaviour
{
    public button firstButton;
    public button secondButton;
    public UnityEvent evenement;

    void Update()
    {
        if(firstButton.isActivated && secondButton.isActivated)
        {
            firstButton.isActivated = false;
            firstButton.isActivated = false;

            firstButton.stayActive = true;
            secondButton.stayActive = true;

            Debug.Log("Solution");
            evenement.Invoke();
        }
    }
}
