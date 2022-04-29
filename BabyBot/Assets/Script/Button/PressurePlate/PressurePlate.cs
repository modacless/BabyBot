using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressurePlate : MonoBehaviour
{
    public bool isActivated = false;
    public bool canBeActivated = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;

            //Audio
            AudioManager Audio = AudioManager.AMInstance;
            Audio.PlaySFX(Audio.pressurePlate, Audio.pressurePlateVolume, 1);
            //----
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = false;
        }

    }
}