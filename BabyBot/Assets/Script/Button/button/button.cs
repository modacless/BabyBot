using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class button : MonoBehaviour
{

    public float stayActiveTime;
    private bool canActivate = false;

    [HideInInspector]
    public bool isActivated = false;

    [HideInInspector]
    public bool stayActive = false;
    private float startTime;

    [HideInInspector]
    public bool Player1In = false;
    [HideInInspector]
    public bool Player2In = false;

    public MeshRenderer selfMeshRenderer;

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
                selfMeshRenderer.material.color = Color.red;
            }
        }
    }

    public void Player1Use(InputAction.CallbackContext context)
    {
        if (Player1In && isActivated == false)
        {
            if (context.started)
            {
                Debug.Log("Player 1");
                isActivated = true;

                //Audio
                AudioManager Audio = AudioManager.AMInstance;
                Audio.PlaySFX(Audio.levier, Audio.levierSource, 1);
                //----

                startTime = Time.time;

                selfMeshRenderer.material.color = Color.green;
            }
        }
    }

    public void Player2Use(InputAction.CallbackContext context)
    {
        if (Player2In && isActivated == false)
        {
            if (context.started)
            {
                isActivated = true;
                Debug.Log("Player 2");

                //Audio
                AudioManager Audio = AudioManager.AMInstance;
                Audio.PlaySFX(Audio.levier, Audio.levierSource, 1);
                //----

                startTime = Time.time;

                selfMeshRenderer.material.color = Color.green;
            }
        }
    }
}
