using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressurePlate : MonoBehaviour
{
    public bool isActivated = false;
    public bool canBeActivated = false;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
        }

    }
}
