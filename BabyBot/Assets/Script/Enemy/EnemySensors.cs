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
        Attack
    }

    //Own Data
    protected Rigidbody rbd;

    //World Data
    public List<Transform> allGoals = new List<Transform>();
    protected Transform actualGoal;

    //Event
    [System.Serializable]
    public class EventChangeFocus : UnityEvent<Transform> { };

    public EventChangeFocus changeFocus;

    protected NavMeshAgent navAgent;

    //Detection

    [Header("Detection")]
    [SerializeField]
    private float rangeRadiusDetection;
    [SerializeField]
    private float rangeRadiusAttack;


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
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        //Configure la machine à état
        SetState();

        actualGoal = GetNearestGoal();
        Debug.Log(actualGoal);

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
                Debug.Log((transform.position - minPosition.position).magnitude > (transform.position - allGoals[i].position).magnitude);
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

}
