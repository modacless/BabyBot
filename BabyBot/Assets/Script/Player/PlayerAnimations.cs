using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator playerAnimatorMovement;
    public Animator playerAnimatorShoot;

    public void Run(bool enable)
    {
        switch (enable)
        {
            case true:
                playerAnimatorMovement.SetBool("isRunning", true);
                break;
            case false:
                playerAnimatorMovement.SetBool("isRunning", false);
                break;
        }
    }

    public void Aim(bool enable)
    {
        switch (enable)
        {
            case true:
                
                break;
            case false:
                
                break;
        }
    }

    public void Shoot(bool enable)
    {
        switch (enable)
        {
            case true:
                playerAnimatorShoot.SetBool("isShooting", true);
                break;
            case false:
                playerAnimatorShoot.SetBool("isShooting", false);
                break;
        }
    }

    public void ShootSingle(bool enable)
    {
        switch (enable)
        {
            case true:
                playerAnimatorShoot.SetBool("isShootingSingle", true);
                break;
            case false:
                playerAnimatorShoot.SetBool("isShootingSingle", false);
                break;
        }
    }

    public void Reload(bool enable, float reloadTime)
    {
        playerAnimatorShoot.SetFloat("reloadSpeed", 1 * (1 / reloadTime));

        switch (enable)
        {
            case true:
                playerAnimatorShoot.SetBool("isReloading", true);
                break;
            case false:
                playerAnimatorShoot.SetBool("isReloading", false);
                break;
        }
    }
}
