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
    public bool isDead = false;
    [SerializeField]
    protected float timerDead;

    protected NavMeshAgent navAgent;

    private WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();

    //World Data
    public List<Transform> allGoals = new List<Transform>();
    protected Transform actualGoal;
    protected PlayerInfo actualPlayerInfo;


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
    public float speed;
    [SerializeField]
    protected int projectileDamage;
    [SerializeField]
    protected float projectileSpawnRange;
    [SerializeField]
    protected float knockbackForce;

    [SerializeField]
    protected GameObject attackGameObject;

    [Header ("Collider")]
    [SerializeField]
    protected CapsuleCollider selfHitBox;

    [Header ("GFX")]
    [SerializeField]
    protected Animator selfAnimator;
    [SerializeField]
    protected GameObject deadthParticleSysteme;
    [SerializeField]
    protected GameObject selfMesh;

    public int pointEnemy;

    public AudioSource enemyShotSource;
    public AudioSource enemyDeathSource;
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
        actualPlayerInfo = actualGoal.GetComponent<PlayerInfo>();


        actualAttackCooldown = attackCooldown;

        deadthParticleSysteme.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update()
    {


            SetState();

            actualGoal = GetNearestGoal();

            //if(actualGoal != null && actualGoal.GetComponent<PlayerInfo>().)
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
                case StateEnemy.Dead:
                    StateDead();
                    break;
                default:
                    StateIdle();
                    break;
            }
        


        //Configure la machine � �tat
        

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

            if (!minPosition.GetComponent<PlayerInfo>().playerInLife)
            {
                for(int i = 0; i< allGoals.Count; i++)
                {
                    if (allGoals[i].GetComponent<PlayerInfo>().playerInLife)
                    {
                        minPosition = allGoals[i];
                    }
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

    protected virtual void StateDead()
    {
        transform.parent = null;
        StartCoroutine(DeadRoutine());

        navAgent.isStopped = true;
        navAgent.velocity = Vector3.zero;
        navAgent.angularSpeed = 0;

        rbd.velocity = Vector3.zero;
        selfHitBox.enabled = false;

        selfMesh.SetActive(false);
        deadthParticleSysteme.SetActive(true);
    }

    protected virtual IEnumerator DeadRoutine()
    {
        yield return new WaitForSeconds(timerDead);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            Bullet bulletLogic = collision.collider.GetComponent<Bullet>();
            if (bulletLogic)
            {
               
                if(lifePoint <= 0)
                {
                    isDead = true;
                    transform.parent = null;
                }
            }
        }
        if(collision.collider.tag == "Train")
        {

        }
    }

    public virtual void TakeDamage(float damage,GameObject fromPlayer)
    {
        lifePoint -= damage;
        if (lifePoint <= 0)
        {
            PlayerInfoManager.instance.AddPlayerScore(fromPlayer.GetComponent<PlayerInfo>(), pointEnemy);
            isDead = true;
            transform.parent = null;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        selfAnimator.SetTrigger("takeDamage");

        lifePoint -= damage;
        if (lifePoint <= 0)
        {
            isDead = true;
            transform.parent = null;
        }
    }



}
