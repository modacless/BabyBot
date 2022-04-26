using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{


    public enum EnemyState
    {
        Idle,
        Moving,
        Attack,
    }

    public EnemyState ownState;

    private NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Transform goal)
    {
        if (goal != null)
        {
            navAgent.SetDestination(goal.position);
            ownState = EnemyState.Moving;
            navAgent.isStopped = false;
        }
        else
        {
            navAgent.isStopped = true;
            ownState = EnemyState.Idle;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    public void Attack()
    {
        Debug.Log("Attack");
        ownState = EnemyState.Attack;
        //navAgent.SetDestination(this.transform.position);
        navAgent.isStopped = true;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
