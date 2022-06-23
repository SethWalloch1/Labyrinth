using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeFallTrigger : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            rb.isKinematic = false;
    }
}
