using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverAnimation : MonoBehaviour
{
    Animator anim;
    private GameObject playerObject;
    public Animator playerAnim;
    public GameObject movingWall;
    private Animator animWall;
    private AudioSource audio;
    public AudioSource wallAudio;
    bool canBePulled = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerObject = GameObject.Find("Player");
        wallAudio = GameObject.Find("MovingWall").GetComponent<AudioSource>();
        playerAnim = playerObject.GetComponent<Animator>();
        animWall = movingWall.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey("q") && canBePulled)
        {
            StartCoroutine("PullLever");
        }
    }
    IEnumerator PullLever()
    {
        audio.Play();
        anim.SetTrigger("QPressed");
        playerAnim.SetTrigger("QPressed");
        animWall.SetTrigger("WallMove");
        wallAudio.Play();
        canBePulled = false;
        yield return new WaitForSeconds(5);
    }
}
