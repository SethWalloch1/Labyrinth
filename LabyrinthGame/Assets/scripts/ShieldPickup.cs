using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
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
            inventory.hasArmorUpgrade = true;
            inventory.armor = 0.5f;
            Destroy(gameObject);
        }
    }
}
