using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class button : MonoBehaviour
{

    public float stayActiveTime;
    private bool canActivate = false;
    public bool isActivated = false;

    [HideInInspector]
    public bool stayActive = false;
    private float startTime;

    public bool Player1In = false;
    public bool Player2In = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerInfo>().numPlayer == 1)
            {
                Player1In = true;
            }
            else
            {
                Player2In = true;
            }
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerInfo>().numPlayer == 1)
            {
                Player1In = false;
            }
            else
            {
                Player2In = false;
            }
        }

    }

    private void Update()
    {
        if (isActivated && !stayActive)
        {
            if (Time.time > startTime + stayActiveTime)
            {
                isActivated = false;
            }
        }
    }

    public void Player1Use(InputAction.CallbackContext context)
    {
        if (Player1In)
        {
            if (context.started)
            {
                if (canActivate)
                {
                    isActivated = true;

                    //Audio
                    AudioManager Audio = AudioManager.AMInstance;
                    Audio.PlaySFX(Audio.levier, Audio.levierVolume, 1);
                    //----

                    startTime = Time.time;
                }
            }
        }
    }

    public void Player2Use(InputAction.CallbackContext context)
    {
        if (Player2In)
        {
            if (context.started)
            {
                if (canActivate)
                {
                    isActivated = true;

                    //Audio
                    AudioManager Audio = AudioManager.AMInstance;
                    Audio.PlaySFX(Audio.levier, Audio.levierVolume, 1);
                    //----

                    startTime = Time.time;
                }
            }
        }
    }
}
