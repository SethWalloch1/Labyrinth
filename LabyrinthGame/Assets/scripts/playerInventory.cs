using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasRedKey = false;
    public bool hasYellowKey = false;
    public bool hasBlueKey = false;
    public bool hasSwordUpgrade = false;
    public bool hasArmorUpgrade = false;

    public float armor = 1f;
    public int attack = 10;
    public float health = 100.0f;
    public float maxHealth = 100.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
