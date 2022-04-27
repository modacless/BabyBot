using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemySensors
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

        transform.LookAt(actualGoal);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }

    private void DoAttack()
    {
        GameObject projectile = Instantiate(attackGameObject, transform.position + transform.forward * projectileSpawnRange, transform.rotation, null);
        projectile.GetComponent<CacProjectileLogic>().InitProjectile(lifeTime, speed, projectileDamage);
    }
}