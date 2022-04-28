using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


//De rien à toi liseur de ce script Franglais :) ( tu as le drois de me détester )
[RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent))]
public class EnemySensors : MonoBehaviour
{
    [Header("State")]
    public StateEnemy enemyState;
    public enum StateEnemy
    {
        Idle,
        Detect,
        Attack,
        Dead
    }

    //Own Data
    [Header("Enemy Data")]
    protected Rigidbody rbd;
    [SerializeField]
    protected float attackCooldown;
    protected float actualAttackCooldown = 0;
    [SerializeField]
    public float lifePoint;
    protected bool isDead = false;
    [SerializeField]
    protected float timerDead;

    protected NavMeshAgent navAgent;

    private WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();

    //World Data
    public List<Transform> allGoals = new List<Transform>();
    protected Transform actualGoal;


    //Detection

    [Header("Detection")]
    [SerializeField]
    private float rangeRadiusDetection;
    [SerializeField]
    private float rangeRadiusAttack;

    //Projectile data
    [Header("Projectile data")]
    [SerializeField]
    protected float lifeTime;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int projectileDamage;
    [SerializeField]
    protected float projectileSpawnRange;

    [SerializeField]
    protected GameObject attackGameObject;

    [Header ("Animator")]
    [SerializeField]
    protected Animator selfAnimator;


    protected virtual void Start()
    {
        rbd = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();

        GameObject[] _allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in _allPlayers)
        {
            allGoals.Add(player.transform);
        }

        actualGoal = _allPlayers[0].transform;

        actualAttackCooldown = attackCooldown;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        //Configure la machine à état
        SetState();

        actualGoal = GetNearestGoal();

        //Applique l'effet de la machine à état
        switch (enemyState)
        {
            case StateEnemy.Idle:
                StateIdle();
                break;
            case StateEnemy.Detect:
                StateDetect();
                break;
            case StateEnemy.Attack:
                StateAttack();
                break;
            case StateEnemy.Dead:
                StateDead();
                break;
            default:
                StateIdle();
                break;
        }

    }

    public Transform GetNearestGoal()
    {
        if (allGoals.Count > 1)
        {
            Transform minPosition = allGoals[0];

            for (int i = 1; i < allGoals.Count; i++)
            {
                if((transform.position - minPosition.position).magnitude > (transform.position - allGoals[i].position).magnitude) {
                    minPosition = allGoals[i];
                }
            }

            return minPosition;
        }
        else
        {
            if(allGoals.Count == 1)
            {
                return allGoals[0];
            }
            else
            {
                return null;
            }
        }
    }

    //Dessine en éditeur les cercles représentant la vision du l'ia
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeRadiusAttack);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeRadiusDetection);

    }

    //Check if player is in range of a (sphere)sensor
    private bool UpdateSensors(float rangeToCheck)
    {
        float lengthDectection = (transform.forward * rangeToCheck).magnitude;
        float lengthNearestPlayer = (actualGoal.position - transform.position).magnitude;

        if(lengthDectection > lengthNearestPlayer)
        {
            return true;
        }

        if(lifePoint <= 0)
        {
            isDead = true;
        }

        return false;
        
    }

    protected virtual void SetState()
    {
        if (UpdateSensors(rangeRadiusDetection) && !UpdateSensors(rangeRadiusAttack))
        {
            enemyState = StateEnemy.Detect;
        }

        if (UpdateSensors(rangeRadiusAttack))
        {
            enemyState = StateEnemy.Attack;
        }

        if(!UpdateSensors(rangeRadiusAttack) && !UpdateSensors(rangeRadiusDetection))
        {
            enemyState = StateEnemy.Idle;
        }

        //Always in last
        if (isDead)
        {
            enemyState = StateEnemy.Dead;
        }
    }

    //est appelé dans l'update quand le mob ne voit rien
    protected virtual void StateIdle()
    {
        navAgent.isStopped = true;
        rbd.velocity = Vector3.zero;
    }
    //est appelé dans l'update quand le mob détécte un joueur à sa portée
    protected virtual void StateDetect()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(actualGoal.transform.position);
    }
    //est appelé quand le joueur se trouve à porté d'attaque
    protected virtual void StateAttack()
    {
        navAgent.isStopped = true;
        rbd.velocity = Vector3.zero;
    }

    protected virtual void StateDead()
    {
        navAgent.isStopped = true;
        rbd.velocity = Vector3.zero;
        StartCoroutine(DeadRoutine());
    }

    protected virtual IEnumerator DeadRoutine()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 90));
        float actualTimerDead = 0;
        GetComponent<CapsuleCollider>().enabled = false;
        while(actualTimerDead < timerDead)
        {
            actualTimerDead += Time.fixedDeltaTime;
            yield return waitFixedUpdate;
        }

        Destroy(this.gameObject);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            Bullet bulletLogic = collision.collider.GetComponent<Bullet>();
            if (bulletLogic)
            {
                TakeDamage((int)bulletLogic.damage);
            }
        }
    }*/

    public virtual void TakeDamage(float damage)
    {
        lifePoint -= damage;
    }

    

}
