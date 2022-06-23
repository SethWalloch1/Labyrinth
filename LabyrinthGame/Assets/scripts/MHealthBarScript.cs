using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MHealthBarScript : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float maximumHealth = 500f;

    //Replace with Player controller script
    public MinotaurScript MinotaurScript;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = MinotaurScript.health;
        healthBar.fillAmount = currentHealth / maximumHealth;
    }
}
