using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinotaurScript : MonoBehaviour
{
    public GameObject Minotaur;
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
    public int health = 500;
    public bool isAlive = true;
    private GameObject axeHitbox;
    private Vector3 origin;
    private Vector3 direction;
    private Rigidbody m_rigidbody;
    private AudioSource audio;
    public AudioClip hitNoise;
    public AudioClip deathNoise;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Anim = GetComponent<Animator>();
        InvokeRepeating("Tick", 0.0f, .2f);
        axeHitbox = GameObject.Find("axeHitBox");
        axeHitbox.SetActive(false);
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
            origin = transform.position + new Vector3(0, 1f, 0);
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
                        int rand = Random.Range(1, 5);
                        audio.PlayOneShot(hitNoise);
                        switch (rand)
                        {
                            case 1:
                                Anim.SetBool("Attack1", true);
                                StartCoroutine(OverHandTempHitbox());
                                break;
                            case 2:
                                Anim.SetBool("Attack2", true);
                                StartCoroutine(SwingTempHitbox());
                                break;
                            case 3:
                                Anim.SetBool("Attack3", true);
                                StartCoroutine(StabTempHitBox());
                                break;
                            case 4:
                                Anim.SetBool("Dodge", true);
                                break;

                        }

                            StartCoroutine(AttackDelay());
                        
                    }
                    attackAvailable = false;
                } else
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
        Anim.SetBool("GotHit", true);
        audio.Play();
        health = health - damage;
        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        agent.isStopped = true;
        Anim.SetBool("Death", true);
        Anim.SetBool("GotHit", false);
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.freezeRotation = true;
        isAlive = false;
        Destroy(Minotaur, 20);
        StartCoroutine(LoadNextScene());
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PlayerSword"))
        {
            if (canBeHit)
            {
                axeHitbox.SetActive(false);
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
    private IEnumerator OverHandTempHitbox()
    {
        yield return new WaitForSeconds(1f);
        axeHitbox.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        axeHitbox.SetActive(false);

    }
    private IEnumerator SwingTempHitbox()
    {
        yield return new WaitForSeconds(1.25f);
        axeHitbox.SetActive(true);
        yield return new WaitForSeconds(0.833f);
        axeHitbox.SetActive(false);
    }
    private IEnumerator StabTempHitBox()
    {
        axeHitbox.SetActive(true);
        yield return new WaitForSeconds(1f);
        axeHitbox.SetActive(false);
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }




}
