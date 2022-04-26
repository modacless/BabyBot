using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Vector2 inputMovement;
    private Vector2 inputLook;
    private Vector3 movementDirection;
    private Vector3 lookDirection;
    
    //References
    private Rigidbody rb;
    private PlayerAnimations playerAnimationsScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimationsScript = GetComponent<PlayerAnimations>();
    }

    public void OnMove(InputAction.CallbackContext context) => inputMovement = context.ReadValue<Vector2>();

    public void OnLook(InputAction.CallbackContext context) => inputLook = context.ReadValue<Vector2>();

    private void Update()
    {
        ReadInputs();

        RotatePlayer();
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
        }
        else if (movementDirection.magnitude > 0f)
        {
            //Rotate controller depending movement direction (left stick)
            transform.rotation = Quaternion.LookRotation(movementDirection.normalized, Vector3.up);
            playerAnimationsScript.Aim(false);
        }
        else
        {
            playerAnimationsScript.Aim(false);
        }
    }
}
