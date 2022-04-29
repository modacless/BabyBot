using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaLogic : MonoBehaviour
{
    public float multiplicatorSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().speed *= multiplicatorSpeed;
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemySensors>().speed *= 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().speed = other.GetComponent<PlayerInfo>().startSpeed;
        }

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemySensors>().speed *= 0.5f;
        }
    }
}
