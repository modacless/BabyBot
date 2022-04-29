using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().speed *= 0.5f;
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemySensors>().speed *= 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().speed *= 2.0f;
        }

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemySensors>().speed *= 2.0f;
        }
    }
}
