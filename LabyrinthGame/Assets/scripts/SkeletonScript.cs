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
    public GameObject swordCollider;
    public GameObject healthPickup;
    public int attackDist = 3;
    private bool attackAvailable = true;
    private bool attackWithinRange = false;
    private bool playerChaseState = false;
    private bool canBeHit = true;
    public bool isTrggerEnem;
    public float maxDistance;
    public int health = 100;
    public bool isAlive = true;
    private float sphereRadius = 4f;
    private playerInventory inventory;
    private Vector3 origin;
    private Vector3 direction;
    private Rigidbody m_rigidbody;
    private AudioSource audio;
    public AudioClip deathClip;
    public isEnemyDead deathCheck;
    // Start is called before the first frame update
    void Start()
    {
        swordCollider.SetActive(false);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        //deathCheck = GameObject.Find("Gate").GetComponent<isEnemyDead>();
        InvokeRepeating("Tick", 0.0f, .2f);
        inventory = GameObject.Find("Player").GetComponent<playerInventory>();
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
            origin = transform.position + new Vector3(0, 1f, 0f);
            direction = transform.forward;
            Debug.DrawRay(origin, direction * maxDistance, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(origin, direction, out hit, maxDistance))
            {
                hitObject = hit.transform.gameObject;
                Debug.Log(hitObject.name);
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
                        StartCoroutine(TempCollider());
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
    IEnumerator TempCollider()
    {
        yield return new WaitForSeconds(0.5f);
        swordCollider.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        swordCollider.SetActive(false);
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
        swordCollider.SetActive(false);
        Anim.SetBool("Hit1", true);
        audio.Play();
        health = health - damage;
        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        canBeHit = false;
        audio.PlayOneShot(deathClip);
        isAlive = false;
        agent.isStopped = true;
        Anim.SetBool("Fall1", true);
        Instantiate(healthPickup, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.0f, gameObject.transform.position.z), Quaternion.identity);
        if(isTrggerEnem)
            deathCheck.KilledOpponent(gameObject);
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.freezeRotation = true;
        Destroy(skeleton, 5);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerSword"))
        {
            if (canBeHit) { 
                Hit(inventory.attack);
                canBeHit = false;
                StartCoroutine(hitCountdown());
            }

        }
    }

    private IEnumerator hitCountdown()
    {
        yield return new WaitForSeconds(.6f);
        canBeHit = true;
    }




}

