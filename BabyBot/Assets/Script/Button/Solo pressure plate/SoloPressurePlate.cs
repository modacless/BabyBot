using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoloPressurePlate : MonoBehaviour
{
    [Header("Event at link")]
    public UnityEvent onPressure;
    public UnityEvent onRelease;

    [Header ("Unity Setup")]
    public MeshRenderer selfMeshRenderer;

    private bool isActivated;


    private void Start()
    {
        selfMeshRenderer.material.SetInt("_pressed", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated == false)
        {
            if (other.gameObject.tag == "Player")
            {
                isActivated = true;
                onPressure.Invoke();

                AudioManager Audio = AudioManager.AMInstance;
                Audio.PlaySFX(Audio.pressurePlate, Audio.pressureSource, 1);

                selfMeshRenderer.material.SetInt("_pressed", 0);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (isActivated == true)
        {
            if (other.gameObject.tag == "Player")
            {
                isActivated = false;
                onRelease.Invoke();

                selfMeshRenderer.material.SetInt("_pressed", 1);
            }
        }
    }

}
