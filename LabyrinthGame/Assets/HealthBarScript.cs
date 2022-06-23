using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
	private Image healthBar;
	public float currentHealth;
	private float maximumHealth = 100f;

	//Replace with Player controller script
	playerInventory Player;

    // Start is called before the first frame update
    void Start()
    {
    	healthBar = GetComponent<Image>();
    	Player = FindObjectOfType<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Player.health;
        healthBar.fillAmount = currentHealth / maximumHealth;
    }
}
