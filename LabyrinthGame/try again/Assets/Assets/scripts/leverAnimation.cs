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

    void Start()
    {
        anim = GetComponent<Animator>();
        playerObject = GameObject.Find("Player");
        playerAnim = playerObject.GetComponent<Animator>();
        animWall = movingWall.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey("q"))
        {
            StartCoroutine("PullLever");
        }
    }
    IEnumerator PullLever()
    {
        Vector3 temp = playerObject.transform.rotation.eulerAngles;
        temp.y = -180;
        playerObject.transform.rotation = Quaternion.Euler(temp);
        playerObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z + 1f);

        anim.SetTrigger("QPressed");
        playerAnim.SetTrigger("QPressed");
        animWall.SetTrigger("WallMove");
        yield return new WaitForSeconds(5);
    }
}
