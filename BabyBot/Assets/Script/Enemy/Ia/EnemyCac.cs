using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCac : EnemySensors
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void StateIdle()
    {
        navAgent.isStopped = true;
        rbd.velocity = Vector3.zero;
    }

    protected override void StateDetect()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(actualGoal.transform.position);
    }

    protected override void StateAttack()
    {
        actualAttackCooldown += Time.deltaTime;
        if (actualAttackCooldown >= attackCooldown)
        {
            DoAttack();
            actualAttackCooldown = 0;
        }
        navAgent.isStopped = true;
        rbd.velocity = Vector3.zero;
    }

    private void DoAttack()
    {
        GameObject projectile = Instantiate(attackGameObject, transform.forward, transform.rotation, null);
        projectile.GetComponent<CacProjectileLogic>().InitProjectile(lifeTime, speed, projectileDamage);
    }
}
