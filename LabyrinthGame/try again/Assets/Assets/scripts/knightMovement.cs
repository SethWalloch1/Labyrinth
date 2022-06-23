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
    //private Ray ray;
    //private RaycastHit hit;
    //public float rayDistance = 4f;
    //private CapsuleCollider playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //playerCollider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        // anim.SetFloat("speed", Input.GetAxis("Vertical"));
        // anim.SetFloat("Direction", Input.GetAxis("Horizontal"));
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //float turnVertical = Input.GetAxis("Mouse Y");

        anim.SetFloat("speed", moveVertical);
        anim.SetFloat("Direction", moveHorizontal);
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        if (Input.GetKeyDown("space"))
        {
            anim.Play("Base Layer.Jumping", 0, 0.25f);
            rb.AddForce(transform.up * 500);
        }

        transform.Translate(movement * Time.deltaTime * movementSpeed);

        turn.x += Input.GetAxis("Mouse X") * .5f;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        //other animations
        //Replace with:
        // if(health <= 0){
        // 	anim.Play("Base Layer.Standing React Death Backward");
        // }
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.Play("Base Layer.Standing React Death Backward");

        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.Play("Base Layer.Standing Melee Attack Horizontal", 0, 0.15f);
            StartCoroutine(tempCollider());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play("Base Layer.Reaching Out", 0, 0.15f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.Play("Base Layer.Stand To Roll", 0, 0.15f);
            rb.velocity = transform.forward * (movementSpeed + 2f);

        }
    }
    void FixedUpdate()
    {
       
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SkeletonSword"))
        {
            inventory.health -= 10;
            if(inventory.health <= 0)
            {
                anim.Play("Base Layer.Standing React Death Backward");
            }
        }
    }
    private IEnumerator tempCollider()
    {
        swordCollider.SetActive(true);
        yield return new WaitForSeconds(1f);
        swordCollider.SetActive(false);
    }

}
