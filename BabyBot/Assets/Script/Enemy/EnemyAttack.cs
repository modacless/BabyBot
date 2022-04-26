using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent enemyAttackEvent;

    [SerializeField]
    private float cooldownAttack;
    private float timerCooldown;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider collision)
    {

        if (collision.tag == "Player")
        {
            timerCooldown += Time.deltaTime;
            if(timerCooldown >= cooldownAttack)
            {
                enemyAttackEvent.Invoke();
                timerCooldown = 0;
            }

        }

    }

    public void OnTriggerExit(Collider collision)
    {
        timerCooldown = 0;
    }

}
