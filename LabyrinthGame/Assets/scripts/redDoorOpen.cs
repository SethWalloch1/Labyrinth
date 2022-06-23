using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redDoorOpen : MonoBehaviour
{
    playerInventory pi;
    Animator anim;
    AudioSource audio;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.Find("Player").GetComponent<playerInventory>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && pi.hasRedKey && !isOpen)
        {
            anim.SetTrigger("DoorMove");
            audio.Play();
            isOpen = true;
        }
    }
}
