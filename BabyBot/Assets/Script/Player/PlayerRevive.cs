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
            if (other.GetComponent<PlayerInfo>().isPressingRevive && !playerInfoScript.playerInLife)
            {
                playerInfoScript.isReviving = true;
                other.GetComponent<PlayerInfo>().playerMovementScript.enabled = false;
                other.GetComponent<PlayerInfo>().playerMovementScript.playerAnimationsScript.Run(false);
                other.GetComponent<PlayerInfo>().actualWeapon.enabled = false;
            }
            else
            {
                playerInfoScript.isReviving = false;
                other.GetComponent<PlayerInfo>().playerMovementScript.enabled = true;
                other.GetComponent<PlayerInfo>().actualWeapon.enabled = true;
            }
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
