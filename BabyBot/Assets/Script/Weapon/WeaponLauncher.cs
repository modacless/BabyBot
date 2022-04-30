using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponLauncher : Weapon
{

    protected override void Start()
    {
        base.Start();

        currentShotsArray = AudioManager.AMInstance.ballLauncherShotsArray;
    }

    protected override void Upgrade1()
    {
        base.Upgrade1();

        currentShotsArray = AudioManager.AMInstance.waterBallLauncherShotsArray;
    }

    protected override void Upgrade2()
    {
        base.Upgrade2();

        currentShotsArray = AudioManager.AMInstance.waterBallLauncherShotsArray;
    }

    protected override void Upgrade3()
    {
        base.Upgrade3();

        currentShotsArray = AudioManager.AMInstance.waterBallLauncherShotsArray;
    }

    protected override void AnimationShoot()
    {
        if (isPressingFire && !isShooting && !isReviving) playerMovementScript.playerAnimationsScript.ShootSingle(true);
        else playerMovementScript.playerAnimationsScript.ShootSingle(false);
    }
}
