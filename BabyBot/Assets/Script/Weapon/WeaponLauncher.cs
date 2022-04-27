using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponLauncher : Weapon
{

    /*protected override void Shoot()
    {

        //Upgrade2();
    }*/

    private void OnDrawGizmos()
    {
        if (drawDebug)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 360 + fireAngle, 0) * transform.forward * 10f);
            //Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 360 - fireAngle, 0) * transform.forward * 10f);
        }
    }

    protected override void Upgrade1()
    {
        base.Upgrade1();
    }

    protected override void Upgrade2()
    {
        base.Upgrade2();

    }

    protected override void Upgrade3()
    {
        base.Upgrade3();

    }
}
