using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Vector2 inputMovement;
    private Vector2 inputLook;
    private Vector3 movementDirection;
    private Vector3 lookDirection;

    private bool checkArray = false;
    public bool isAiming = false;

    //References
    private Rigidbody rb;
    private PlayerAnimations playerAnimationsScript;
    public CinemachineTargetGroup targetGroup;
    public CinemachineTargetGroup.Target AimTarget;


    AudioManager AM;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimationsScript = GetComponent<PlayerAnimations>();

        AM = AudioManager.AMInstance;
    }

    public void OnMove(InputAction.CallbackContext context) => inputMovement = context.ReadValue<Vector2>();

    public void OnLook(InputAction.CallbackContext context) => inputLook = context.ReadValue<Vector2>();

    private void Update()
    {
        ReadInputs();

        RotatePlayer();

        //KeepPlayerInCam();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ReadInputs()
    {
        movementDirection = new Vector3(inputMovement.x, 0f, inputMovement.y);
        lookDirection = Vector3.right * inputLook.x + Vector3.forward * inputLook.y;
    }

    private void MovePlayer()
    {
        if (movementDirection.magnitude >= 0.1f)
        {
            rb.MovePosition(rb.position + movementDirection * speed * Time.deltaTime);
            playerAnimationsScript.Run(true);
        }
        else
        {
            playerAnimationsScript.Run(false);
        }
    }

    private void RotatePlayer()
    {
        if (lookDirection.sqrMagnitude > 0f)
        {
            //Rotate controller depending look direction (right stick)
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            playerAnimationsScript.Aim(true);
            AddNewTarget(true);
            isAiming = true;
        }
        else if (movementDirection.magnitude > 0f)
        {
            //Rotate controller depending movement direction (left stick)
            transform.rotation = Quaternion.LookRotation(movementDirection.normalized, Vector3.up);
            playerAnimationsScript.Aim(false);
            AddNewTarget(false);
        }
        else
        {
            playerAnimationsScript.Aim(false);
            AddNewTarget(false);
            isAiming = false;
        }
    }

    private void KeepPlayerInCam()
    {
        if (lookDirection.sqrMagnitude > 0f)
        {
            Vector3 positionAimScreen = Camera.main.WorldToViewportPoint(AimTarget.target.transform.position);
            if (positionAimScreen.x < 0) transform.position += Vector3.right;
            else if (1 < positionAimScreen.x) transform.position += Vector3.left;
            else if (positionAimScreen.y < 0) transform.position += Vector3.forward;
            else if (0.90 < positionAimScreen.y) transform.position += Vector3.back;
        }

        Vector3 positionPlayerScreen = Camera.main.WorldToViewportPoint(transform.position);
        if (positionPlayerScreen.x < 0) transform.position += Vector3.right;
        else if (1 < positionPlayerScreen.x) transform.position += Vector3.left;
        else if (positionPlayerScreen.y < 0) transform.position += Vector3.forward;
        else if (0.90 < positionPlayerScreen.y) transform.position += Vector3.back;
    }

    private void AddNewTarget(bool add)
    {
        switch (add)
        {
            case true:
                if (!checkArray)
                {
                    checkArray = true;
                    for (int i = 0; i < targetGroup.m_Targets.Length; i++)
                    {
                        if (targetGroup.m_Targets[i].target == null)
                        {
                            targetGroup.m_Targets.SetValue(AimTarget, i);
                            return;
                        }
                    }
                }
                break;
            case false:
                if (checkArray)
                {
                    checkArray = false;
                    for (int i = 0; i < targetGroup.m_Targets.Length; i++)
                    {
                        if (targetGroup.m_Targets[i].target == AimTarget.target)
                        {
                            targetGroup.m_Targets.SetValue(null, i);
                            return;
                        }
                    }
                }
                break;
        }  
    }

    public void playFootStep()
    {
        float newPitch = Random.Range(0.8f, 1.2f);

        if(AM.isOnWood == true)
        {
            int newIndex = Random.Range(0, (AM.woodFootstepArray.Length - 1));
            AM.PlaySFX(AM.woodFootstepArray[newIndex], 1, newPitch);
        }
        else
        {
            int newIndex = Random.Range(0, (AM.carpetFootstepArray.Length - 1));
            AM.PlaySFX(AM.carpetFootstepArray[newIndex], 1, newPitch);
        }
    }
}
