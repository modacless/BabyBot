using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBullet : Bullet
{
    public LayerMask layerRaycast;
    public GameObject lightSaber;
    public float offsetYSpawn;
    [HideInInspector] public bool instantiateLightSaber = false;

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (instantiateLightSaber)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 10f, out hit, layerRaycast))
            {
                Instantiate(lightSaber, new Vector3(transform.position.x, hit.transform.position.y + offsetYSpawn, transform.position.z), lightSaber.transform.rotation);
            }
        } 
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemySensors>().TakeDamage((int)damage);
        }
        if (collider.tag != ("Bullet") && collider.tag != ("Enemy")) Destroy(gameObject);
    }
}
