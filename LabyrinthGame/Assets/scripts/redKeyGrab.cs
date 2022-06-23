using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redKeyGrab : MonoBehaviour
{
    playerInventory pi;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.Find("Player").GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            pi.hasRedKey = true;
            audio.Play();
            Destroy(gameObject);
        }
    }
}
