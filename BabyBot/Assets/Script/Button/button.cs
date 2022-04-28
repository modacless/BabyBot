using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    public float stayActiveTime;
    public bool isActivated = false;
    public Material InitialMat;
    public Material usingMat;

    [HideInInspector]
    public bool stayActive = false;
    private float startTime;

    //public void OnTriggerStay(Collider other)
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
            transform.GetComponent<MeshRenderer>().materials[0] = usingMat;
            startTime = Time.time;
        }

    }

    private void Update()
    {
        if (isActivated && !stayActive)
        {
            if (Time.time > startTime + stayActiveTime)
            {
                isActivated = false;
                transform.GetComponent<MeshRenderer>().materials[0] = InitialMat;
            }
        }
    }
}
