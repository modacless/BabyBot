using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressurePlate : MonoBehaviour
{
    [HideInInspector]
    public bool isActivated = false;
    [HideInInspector]
    public bool canBeActivated = false;

    [HideInInspector]
    public bool lockSysteme;

    [Header("Unity Setup")]
    public MeshRenderer selfMeshRender;



    private void Start()
    {
        selfMeshRender.material.SetInt("_pressed", 1);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (lockSysteme == false)
        {
            if (other.CompareTag("Player"))
            {
                if (isActivated == false)
                {
                    isActivated = true;

                    selfMeshRender.material.SetInt("_pressed", 0);
                    AudioManager Audio = AudioManager.AMInstance;
                    Audio.PlaySFX(Audio.pressurePlate, Audio.pressureSource, 1);
                }
            }
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (lockSysteme == false)
        {
            if (other.CompareTag("Player"))
            {
                if (isActivated == true)
                {
                        selfMeshRender.material.SetInt("_pressed", 1);
                        isActivated = false;
                }
            }
        }
    }
}
