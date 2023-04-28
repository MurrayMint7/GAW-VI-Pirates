using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask Whatisground;
    public LayerMask Whatisplayer;

    public float health = 100;

    //Patroling
    public Vector3 walkPoint;
    bool walkpointSet;
    public float walkpointRange;

    //attacking
    public float timebetweenAttacks;
    public bool alreadyAttacked;
    public GameObject projectile;

    //states
    public float sightRange;
    public float attackRange;
    public bool PlayerInSightRange;
    public bool PlayerInAttackRange;

    void Update()
    {
        //check for sight and attack range
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, Whatisplayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Whatisplayer);

        if(!PlayerInSightRange && !PlayerInAttackRange)
        {
            Patrolling();
        }
        if(PlayerInSightRange && !PlayerInAttackRange)
        {
            ChasePlayer();
        }
        if (PlayerInSightRange && PlayerInAttackRange)
        {
            AttackPlayer();
        }
    }
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    void Patrolling()
    {
        if(!walkpointSet) 
        {
            SearchWalkPoint();
        }

        if (walkpointSet) 
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distancetowalkPoint = transform.position - walkPoint;

        //walk point reached 
        if(distancetowalkPoint.magnitude < 1f)
        {
            walkpointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        //calculate random point in range 
        float RandomZ = Random.Range(-walkpointRange, walkpointRange);
        float RandomX = Random.Range(-walkpointRange, walkpointRange);

        walkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        if(Physics.Raycast(walkPoint, -transform.up, Whatisground))
        {
            walkpointSet = true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position); 
    }

    void AttackPlayer()
    {
        //make sure enemy does not move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 2f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timebetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }


    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player Projectile"))
        {
            Debug.Log("Ow Back");
            health = health - 25;
            if (health <= 0)
            {
                Invoke(nameof(DestroyEnemy), 0.5f);
            }
        }
    }
}
