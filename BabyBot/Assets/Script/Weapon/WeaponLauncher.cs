using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponLauncher : Weapon
{

    protected override void Start()
    {
        base.Start();

        currentShotsVolume = AudioManager.AMInstance.ballLauncherShotsVolume;
        currentShotsArray = AudioManager.AMInstance.ballLauncherShotsArray;
    }

    protected override void Upgrade1()
    {
        base.Upgrade1();

        currentShotsVolume = AudioManager.AMInstance.waterBallLauncherShotsVolume;
        currentShotsArray = AudioManager.AMInstance.waterBallLauncherShotsArray;
    }

    protected override void Upgrade2()
    {
        base.Upgrade2();

        currentShotsVolume = AudioManager.AMInstance.waterBallLauncherShotsVolume;
        currentShotsArray = AudioManager.AMInstance.waterBallLauncherShotsArray;
    }

    protected override void Upgrade3()
    {
        base.Upgrade3();

        currentShotsVolume = AudioManager.AMInstance.waterBallLauncherShotsVolume;
        currentShotsArray = AudioManager.AMInstance.waterBallLauncherShotsArray;
    }
}
