using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLeverLevel3 : MonoBehaviour
{
    Animator anim;
    private GameObject playerObject;
    bool canBePulled = true;
    private AudioSource audio;
    public GameObject octoWall;
    public OctoWall script;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        script = octoWall.GetComponent<OctoWall>();
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
            script.leversPulled++;
        }
    }
    IEnumerator PullLever()
    {
        audio.Play();
        anim.SetTrigger("QPressed");
        canBePulled = false;
        yield return new WaitForSeconds(5);
    }
}
