using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DenekeHealth : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float maximumHealth = 200f;

    //Replace with Player controller script
    public PaladinController PaladinController;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = PaladinController.health;
        healthBar.fillAmount = currentHealth / maximumHealth;
    }
}

