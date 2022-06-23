using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    private GameObject playerObject;
    private PlayerAnimationController player;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player") && player.hasKey && Input.GetKey("space")) {
            anim.SetTrigger("SpacePressed");
        }
    }
}
