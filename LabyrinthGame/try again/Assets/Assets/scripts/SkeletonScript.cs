using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public Transform[] points;
    public GameObject skeleton;
    private int destPoint = 0;
    private Animator Anim;
    private UnityEngine.AI.NavMeshAgent agent;
    private int chaseSpeed = 5;
    private int patrolSpeed = 3;
    public GameObject player;
    public GameObject hitObject;
    public int attackDist = 3;
    private bool attackAvailable = true;
    private bool attackWithinRange = false;
    private bool playerChaseState = false;
    private bool canBeHit = true;
    public float maxDistance;
    public int health = 100;
    public bool isAlive = true;
    private float sphereRadius = 4f;
    private Vector3 origin;
    private Vector3 direction;
    private Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Anim = GetComponent<Animator>();
        InvokeRepeating("Tick", 0.0f, .2f);
        GotoNextPoint();
    }
    //update just for testing
    void Update()
    {
        /* inputs for testing
        if(Input.GetKeyDown("g")){
            Death();
            health = 0;
        }
        if (Input.GetKeyDown("h"))
        {
            Hit(20);
        }
        if (Input.GetKeyDown("r"))
        {
            health = 100;
            isAlive = true;
            Anim.SetBool("Up", true);
            GotoNextPoint();
        }*/
        if (isAlive)
        {
            Anim.SetFloat("speed", agent.velocity.sqrMagnitude);
            if (!agent.pathPending && agent.remainingDistance < 0.5f && !playerChaseState)
            {
                GotoNextPoint();
            }
            else if (playerChaseState)
            {
                ChasePlayer();
            }
        }
    }

    void Tick()
    {
        if (isAlive)
        {
            origin = transform.position + new Vector3(0, 2.0f, 3.0f);
            direction = transform.forward;
            RaycastHit hit;
            if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance))
            {
                hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Player"))
                {
                    player = hitObject;
                    playerChaseState = true;
                }
            }
            if (playerChaseState)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= attackDist)
                {
                    if (player)
                        attackWithinRange = true;
                    agent.isStopped = true;
                    if (attackAvailable)
                    {
                        Anim.SetBool("Attack1h1", true);
                        StartCoroutine(AttackDelay());
                    }
                    attackAvailable = false;
                }
                else
                {
                    attackWithinRange = false;
                    //agent.isStopped = false;
                }
            }


        }
            
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.5f);
        attackAvailable = true;
        if (!attackWithinRange && isAlive)
        {
            agent.isStopped = false;
        }
    }
    void GotoNextPoint()
    {
        if (isAlive)
        {
            agent.speed = patrolSpeed;
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }
    }
    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.destination = player.transform.position;
        transform.LookAt(player.transform.position);
    }

    void Hit(int damage)
    {
        Anim.SetBool("Hit1", true);
        health = health - damage;
        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        isAlive = false;
        agent.isStopped = true;
        Anim.SetBool("Fall1", true);
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.freezeRotation = true;
        Destroy(skeleton, 5);
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("PlayerSword"))
        {
            if (canBeHit) { 
                Hit(20);
                canBeHit = false;
                StartCoroutine(hitCountdown());
            }

        }
    }

    private IEnumerator hitCountdown()
    {
        yield return new WaitForSeconds(.5f);
        canBeHit = true;
    }




}

