using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{

    //Projectile Data
    protected float lifeTime;
    protected float actualLifeTime;
    protected float speed;
    private int projectileDamage;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        actualLifeTime = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        actualLifeTime += Time.fixedDeltaTime;
        if (actualLifeTime >= lifeTime)
        {
            DestroyProjectile();
        }

        ProjectileMovement();
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerInfo>())
            {
                other.gameObject.GetComponent<PlayerInfo>().DamagePlayer(projectileDamage);
            }
        }
    }

    public void InitProjectile(float _lifeTime, float _speed,int _projectileDamage)
    {
        lifeTime = _lifeTime;
        speed = _speed;
        _projectileDamage = projectileDamage;

    }

    protected virtual void ProjectileMovement()
    {
        transform.position = transform.forward * speed;
    }

}
