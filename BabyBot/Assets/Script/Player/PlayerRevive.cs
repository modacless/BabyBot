using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRevive : MonoBehaviour
{
    public PlayerInfo playerInfoScript;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerInfo>().isPressingRevive) playerInfoScript.isReviving = true;
            else playerInfoScript.isReviving = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInfoScript.isReviving = false;
        }
    }
}
