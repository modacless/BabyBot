using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DropEnemy : MonoBehaviour
{
    public enum EnemyType
    {
        Range,
        Cac
    }
    public float heightSpawn;
    public float speed;

    public EnemyType wichEnemy;

    [Header("Unity Setup")]
    public GameObject parachuteObj;
    public LayerMask layerMask;
    public Transform startRaycast;
    public Rigidbody selfRb;
    public NavMeshAgent selfNavMesh;
    public EnemyCac enemyCacLogic;
    public EnemyRange enemyRangeLogic;

    public Transform destination;


    private void Awake()
    {
        selfNavMesh.isStopped = true;
    }

    private void Start()
    {
        switch (wichEnemy)
        {
            case EnemyType.Range:
                enemyRangeLogic.enabled = false;
                break;
            case EnemyType.Cac:
                enemyCacLogic.enabled = false;
                break;
        }

        gameObject.transform.position = new Vector3(transform.position.x, heightSpawn, transform.position.z);
    }

    private void Update()
    {
        Debug.DrawRay(startRaycast.position, Vector3.down * 100f, Color.red);

        //Vector3 movPos = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, destination.position.y, speed * Time.deltaTime), transform.position.z);
        //transform.position = movPos;

    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(3);

        selfRb.useGravity = true;
        selfNavMesh.enabled = true;
        parachuteObj.SetActive(false);
        Debug.Log("je rentre");

        this.enabled = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            selfNavMesh.isStopped = true;
            parachuteObj.SetActive(false);
        }
    }
}
