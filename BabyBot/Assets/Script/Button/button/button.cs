using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class button : MonoBehaviour
{

    public float stayActiveTime;
    private bool canActivate = false;

    [HideInInspector]
    public bool isActivated = false;

    [HideInInspector]
    public bool stayActive = false;
    private float startTime;

    private float actualCooldownTime;

    [HideInInspector]
    public bool Player1In = false;
    [HideInInspector]
    public bool Player2In = false;

    public MeshRenderer selfMeshRenderer;

    [Header("UI")]
    public Image cooldownUi;

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

    private void Start()
    {
        selfMeshRenderer.material.SetInt("_pressed", 1);

        cooldownUi.fillAmount = 0;
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
            if (actualCooldownTime <= 0)
            {
                isActivated = false;
                selfMeshRenderer.material.SetInt("_pressed", 1);

                actualCooldownTime = 0;
            }
            else
            {
                actualCooldownTime -= Time.deltaTime;
                cooldownUi.fillAmount = actualCooldownTime / stayActiveTime;
            }
        }


        if (stayActive == true)
        {
            cooldownUi.fillAmount = 0;
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

                actualCooldownTime = stayActiveTime;

                selfMeshRenderer.material.SetInt("_pressed", 0); ;
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

                actualCooldownTime = stayActiveTime;

                selfMeshRenderer.material.SetInt("_pressed", 0);
            }
        }
    }
}
