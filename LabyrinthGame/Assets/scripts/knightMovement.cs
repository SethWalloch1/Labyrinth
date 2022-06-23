using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public float movementSpeed = 5f;
    public float rotationSpeed = 100f;
    Rigidbody rb;
    private Vector2 turn;
    public playerInventory inventory;
    public GameObject swordCollider;
    public bool canAttack = true;
    public bool canJump = true;
    public bool canFootstep = true;
    public bool canBeHit = true;
    public AudioClip oofSound;
    public AudioClip deathSound;
    public AudioSource audio;
    public AudioSource swordAudio;
    //private Ray ray;
    //private RaycastHit hit;
    //public float rayDistance = 4f;
    //private CapsuleCollider playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<playerInventory>();
        //playerCollider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        swordCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // anim.SetFloat("speed", Input.GetAxis("Vertical"));
        // anim.SetFloat("Direction", Input.GetAxis("Horizontal"));
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //float turnVertical = Input.GetAxis("Mouse Y");
        if ((moveHorizontal != 0 || moveVertical != 0) && audio.isPlaying == false)
        {
            audio.volume = Random.Range(0.8f, 1);
            audio.pitch = Random.Range(0.8f, 1.1f);
            audio.Play();
        }
        anim.SetFloat("speed", moveVertical);
        anim.SetFloat("Direction", moveHorizontal);
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        if (Input.GetKeyDown("space") && canJump)
        {
            anim.Play("Base Layer.Jumping", 0, 0.25f);
            rb.AddForce(transform.up * 2000f);
            StartCoroutine(jumpTime());

        }

        transform.Translate(movement * Time.deltaTime * movementSpeed);

        turn.x += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        //other animations
        //Replace with:
        // if(health <= 0){
        // 	anim.Play("Base Layer.Standing React Death Backward");
        // }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            anim.Play("Base Layer.Standing Melee Attack Horizontal", 0, 0.3f);
            swordAudio.Play();
            StartCoroutine(tempCollider());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play("Base Layer.Reaching Out", 0, 0.15f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.Play("Base Layer.Falling To Roll", 0, 0.15f);
            rb.velocity = transform.forward * movementSpeed;
        }
    }
    void FixedUpdate()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("SkeleSwordCollider") || other.CompareTag("MinotaurAxe") || other.CompareTag("DenekeSword")) && canBeHit)
        {
            GetComponent<AudioSource>().PlayOneShot(oofSound);
            inventory.health -= inventory.armor * 10;
            StartCoroutine(hitDelay());

            if (inventory.health <= 0)
            {
                audio.PlayOneShot(deathSound);
                anim.Play("Base Layer.Standing React Death Backward");
            }
        }
        if(other.CompareTag("Trap") && canBeHit)
        {
            GetComponent<AudioSource>().PlayOneShot(oofSound);
            inventory.health -= inventory.armor * 30;
            StartCoroutine(hitDelay());

            if (inventory.health <= 0)
            {
                audio.PlayOneShot(deathSound);
                anim.Play("Base Layer.Standing React Death Backward");
            }
        }
    }

    private IEnumerator tempCollider()
    {
        canAttack = false;
        swordCollider.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        swordCollider.SetActive(false);
        canAttack = true;
        yield return new WaitForSeconds(1f);

    }
    private IEnumerator jumpTime()
    {
        canJump = false;
        yield return new WaitForSeconds(1f);
        canJump = true;

    }
    private IEnumerator hitDelay()
    {
        canBeHit = false;
        yield return new WaitForSeconds(1f);
        canBeHit = true;
    }

}