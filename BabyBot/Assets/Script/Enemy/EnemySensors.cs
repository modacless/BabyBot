using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class EnemySensors : MonoBehaviour
{

    public List<Transform> allGoals = new List<Transform>();
    private Transform actualGoal;
    private float radius;

    [System.Serializable]
    public class EventChangeFocus : UnityEvent<Transform> { };

    public EventChangeFocus changeFocus;

    private NavMeshAgent navAgent;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(allGoals.Count > 0)
        {
            changeFocus.Invoke(GetNearestGoal());
        }
        else
        {
            changeFocus.Invoke(null);
        }
    }

    public Transform GetNearestGoal()
    {

        if(allGoals.Count > 1)
        {
            Transform minPosition = allGoals[0];

            for (int i = 1; i < allGoals.Count; i++)
            {
                if(transform.position.magnitude - minPosition.position.magnitude > transform.position.magnitude - allGoals[i].position.magnitude) {
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

    public void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Player")
        {
            if (!allGoals.Contains(collision.transform))
            {
                allGoals.Add(collision.transform);
            }
        }

    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            allGoals.Remove(collision.transform);
        }
    }


}
