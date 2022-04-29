using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PressureButton : MonoBehaviour
{

    [HideInInspector]
    public bool appuie = false;
    public float timeNeeded;
    private int numberOfPlayer = 0;

    [HideInInspector]
    public float ActualTime = 0;
    private bool isActivated = false;

    public UnityEvent evenement;

    [HideInInspector]
    public bool Player1On = false;

    [HideInInspector]
    public bool Player2On = false;



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerInfo>().numPlayer == 1)
            {
                Player1On = true;
            }
            else
            {
                Player2On = true;
            }
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerInfo>().numPlayer == 1)
            {
                Player1On = false;
            }
            else
            {
                Player2On = false;
            }
        }

    }


    public void Player1Use(InputAction.CallbackContext context)
    {
        if (Player1On)
        {
            if (context.started)
            {
                appuie = true;
                numberOfPlayer++;
            }
            if (context.canceled)
            {
                appuie = false;
                numberOfPlayer--;
            }
        }
    }
    public void Player2Use(InputAction.CallbackContext context)
    {
        if (Player2On)
        {
            if (context.started)
            {
                appuie = true;
                numberOfPlayer++;
            }
            if (context.canceled)
            {
                appuie = false;
                numberOfPlayer--;
            }
        }
    }
    private void Update()
    {

        if (!isActivated)
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
            if (ActualTime >= timeNeeded)
            {
                isActivated = true;
                evenement.Invoke();
            }
        }
        else
        {
            ActualTime = timeNeeded;
        }
    }
}
