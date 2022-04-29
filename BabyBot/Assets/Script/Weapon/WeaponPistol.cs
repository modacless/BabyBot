using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPistol : Weapon
{
    protected override void Start()
    {
        base.Start();

        currentShotsArray = AudioManager.AMInstance.waterGunShotsArray;
    }
}
