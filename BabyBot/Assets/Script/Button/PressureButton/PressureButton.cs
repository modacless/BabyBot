using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PressureButton : MonoBehaviour
{
    public bool appuie = false;
    public float timeNeeded;
    private int numberOfPlayer = 0;
    public float ActualTime = 0;

    public UnityEvent evenement;



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            numberOfPlayer++;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            numberOfPlayer--;
        }

    }


    public void Validate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            appuie = true;
        }
        if (context.canceled)
        {
            appuie = false;

        }
    }
    private void Update()
    {
        if (!appuie)
        {
            if (ActualTime < 0) ActualTime = 0;
            if (ActualTime > 0) ActualTime -= Time.deltaTime;
        }
        else
        {
            ActualTime += Time.deltaTime * numberOfPlayer;
        }
        if(ActualTime>=timeNeeded)
        {

            evenement.Invoke();
        }
    }
}
