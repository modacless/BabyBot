using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBullet : Bullet
{
    public GameObject lightSaber;
    public float offsetYSpawn;

    protected override void OnDestroy()
    {
        base.OnDestroy();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 10f, out hit))
        {
            Instantiate(lightSaber, new Vector3(transform.position.x, hit.transform.position.y + offsetYSpawn, transform.position.z), lightSaber.transform.rotation);
        } 
    }
}
