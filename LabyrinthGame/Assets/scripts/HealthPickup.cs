using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private playerInventory inventory;
    // Start is called before the first frame update
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
        if(other.CompareTag("Player") && inventory.health < inventory.maxHealth)
        {
            inventory.health += 20;
            if (inventory.health > inventory.maxHealth)
                inventory.health = inventory.maxHealth;

            Destroy(gameObject);
        }
    }
}
