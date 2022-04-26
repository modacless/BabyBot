using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class EnemySensors : MonoBehaviour
{

    private List<Transform> allGoals = new List<Transform>();
    private Transform actualGoal;
    private float radius;

    public UnityEvent changeFocus;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform beforeChange = actualGoal;
        actualGoal = GetNearestGoal();
        if(actualGoal != beforeChange)
        {
            changeFocus.Invoke();
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
        
    }

    public void OnTriggerExit(Collider other)
    {
        
    }

}
