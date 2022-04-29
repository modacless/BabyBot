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
        selfAnimator.SetBool("isMoving", false);
    }

    protected override void StateDetect()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(actualGoal.transform.position);
        selfAnimator.SetBool("isMoving", true);
    }

    protected override void StateAttack()
    {
        actualAttackCooldown += Time.deltaTime;
        if (actualAttackCooldown >= attackCooldown)
        {
            DoAttack();
            actualAttackCooldown = 0;

            selfAnimator.SetTrigger("attack");
        }
        navAgent.isStopped = true;
        rbd.velocity = Vector3.zero;

        transform.LookAt(actualGoal);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));

        selfAnimator.SetBool("isMoving", false);
    }

    protected override void StateDead()
    {
        base.StateDead();
    }

    private void DoAttack()
    {
        GameObject projectile = Instantiate(attackGameObject, transform.position + transform.forward * projectileSpawnRange, transform.rotation, null);
        projectile.GetComponent<CacProjectileLogic>().InitProjectile(lifeTime, speed, projectileDamage);


        //Audio
        AudioManager Audio = AudioManager.AMInstance;

        float pitch = Random.Range(0.8f, 1.2f);
        int index = Random.Range(0, (Audio.enemyShotsArray.Length - 1));
        Audio.PlaySFX(Audio.enemyShotsArray[index], enemyShotSource, pitch);
        //----
    }
}
