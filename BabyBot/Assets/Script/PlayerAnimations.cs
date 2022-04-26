using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    public void Run(bool enable)
    {
        switch (enable)
        {
            case true:
                playerAnimator.SetBool("isRunning", true);
                break;
            case false:
                playerAnimator.SetBool("isRunning", false);
                break;
        }
    }

    public void Aim(bool enable)
    {
        switch (enable)
        {
            case true:
                playerAnimator.SetBool("isShooting", true);
                break;
            case false:
                playerAnimator.SetBool("isShooting", false);
                break;
        }
    }
}
