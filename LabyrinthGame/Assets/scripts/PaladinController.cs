using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaladinController : MonoBehaviour
{
    public GameObject Paladin;
    private Animator Anim;
    private UnityEngine.AI.NavMeshAgent agent;
    public GameObject player;
    public GameObject hitObject;
    public int attackDist = 3;
    private bool attackAvailable = true;
    private bool attackWithinRange = false;
    private bool playerChaseState = false;
    private bool canBeHit = true;
    public float maxDistance;
    public int health = 200;
    public bool isAlive = true;
    private Vector3 origin;
    private Vector3 direction;
    private Rigidbody m_rigidbody;
    public AudioClip attackSound;
    public AudioClip deathSound;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Anim = GetComponent<Animator>();
        InvokeRepeating("Tick", 0.0f, .2f);
        audio = GetComponent<AudioSource>();
    }
    //update just for testing
    void Update()
    {

        if (isAlive)
        {
            Anim.SetFloat("Locomotion", agent.velocity.sqrMagnitude);

            if (playerChaseState)
            {

                ChasePlayer();
            }
        }
    }

    void Tick()
    {
        if (isAlive)
        {
            origin = transform.position + new Vector3(0, .5f, 0);
            direction = player.transform.position - transform.position;
            RaycastHit hit;
            Debug.DrawRay(origin, direction * maxDistance, Color.red);
            if (Physics.Raycast(origin, direction, out hit, maxDistance))
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
                        int rand = Random.Range(1, 7);
                        audio.PlayOneShot(attackSound);
                        switch (rand)
                        {
                            case 1:
                                Anim.SetBool("Block", true);
                                break;
                            case 2:
                                Anim.SetBool("Slash", true);
                                break;
                            case 4:
                                Anim.SetBool("Jump", true);
                                break;
                            case 5:
                                Anim.SetBool("Heavy Spin", true);
                                break;
                            case 6:
                                Anim.SetBool("Spin", true);
                                break;
                            case 3:
                                Anim.SetBool("Jump", true);
                                break;

                        }

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
        yield return new WaitForSeconds(2.5f);
        attackAvailable = true;
        if (!attackWithinRange && isAlive)
        {
            agent.isStopped = false;
        }
    }
    void ChasePlayer()
    {
        agent.speed = 5;
        agent.destination = player.transform.position;
        transform.LookAt(player.transform.position);
    }






    void Hit(int damage)
    {
        int rand = Random.Range(1, 3);
        audio.Play();
        switch (rand)
        {
            case 1:
                Anim.SetBool("Hit 1", true);
                break;
            case 2:
                Anim.SetBool("Hit 2", true);
                break;
        }

        health = health - damage;
        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        agent.isStopped = true;
        audio.PlayOneShot(deathSound);
        Anim.SetBool("Death", true);
        Anim.SetBool("Hit 1", false);
        Anim.SetBool("Hit 2", false);
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.freezeRotation = true;
        isAlive = false;
        StartCoroutine(LoadNextScene());
        Destroy(Paladin, 20);
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PlayerSword"))
        {
            if (canBeHit)
            {
                Hit(20);
                canBeHit = false;
                StartCoroutine(hitCountdown());
            }

        }
    }
    private IEnumerator hitCountdown()
    {
        yield return new WaitForSeconds(1f);
        canBeHit = true;
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("lvl 3");
    }
}
