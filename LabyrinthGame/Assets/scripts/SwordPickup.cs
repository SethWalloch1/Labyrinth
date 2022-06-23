using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    // Start is called before the first frame update
    playerInventory inventory;
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.hasSwordUpgrade = true;
            inventory.attack *= 2;
            Destroy(gameObject);
        }
    }
}
