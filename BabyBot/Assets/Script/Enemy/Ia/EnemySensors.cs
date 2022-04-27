using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


//De rien � toi liseur de ce script Franglais :) ( tu as le drois de me d�tester )
[RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent))]
public class EnemySensors : MonoBehaviour
{
    [Header("State")]
    public StateEnemy enemyState;
    public enum StateEnemy
    {
        Idle,
        Detect,
        Attack
    }

    //Own Data
    protected Rigidbody rbd;
    [SerializeField]
    protected float attackCooldown;
    protected float actualAttackCooldown = 0;

    protected NavMeshAgent navAgent;

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

        //Configure la machine � �tat
        SetState();

        actualGoal = GetNearestGoal();

        //Applique l'effet de la machine � �tat
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

    //Dessine en �diteur les cercles repr�sentant la vision du l'ia
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

        return false;
        
    }

    private void SetState()
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
    }

    //est appel� dans l'update quand le mob ne voit rien
    protected virtual void StateIdle()
    {
        navAgent.isStopped = true;
        rbd.velocity = Vector3.zero;
    }
    //est appel� dans l'update quand le mob d�t�cte un joueur � sa port�e
    protected virtual void StateDetect()
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(actualGoal.transform.position);
    }
    //est appel� quand le joueur se trouve � port� d'attaque
    protected virtual void StateAttack()
    {
        navAgent.isStopped = true;
        rbd.velocity = Vector3.zero;
    }

}
