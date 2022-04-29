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
            firstButton.stayActive = true;
            secondButton.stayActive = true;
            evenement.Invoke();
        }
    }

    public void destructButtons()
    {
        Destroy(firstButton.transform.gameObject);
        Destroy(secondButton.transform.gameObject);
    }
}
