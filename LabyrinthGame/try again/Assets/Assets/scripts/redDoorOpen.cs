using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redDoorOpen : MonoBehaviour
{
    playerInventory pi;
    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.Find("Player").GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && pi.hasRedKey)
        {
            Destroy(gameObject);
        }
    }
}
