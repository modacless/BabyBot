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

    //public void OnTriggerStay(Collider other)
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
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

    public void Validate(InputAction.CallbackContext context)
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
