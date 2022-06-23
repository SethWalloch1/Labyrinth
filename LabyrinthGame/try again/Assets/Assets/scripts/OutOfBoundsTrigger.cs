using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour
{
    //public Transform Player;
    public Transform SpawnPoint;
    public bool test = false;
    playerInventory Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Player.health = -1;
            test = true;
            Player.transform.position = SpawnPoint.position;
        }
    }
}
