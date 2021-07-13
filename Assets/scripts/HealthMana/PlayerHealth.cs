using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHelth = 300;
    public int currentHelth;
    public HealthBar healthBar;

    void Start()
    {
        currentHelth = maxHelth;
        healthBar.SetMaxHealth(maxHelth);
    }


    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(50);*/
    }
    void TakeDamage(int damage)
    {
        currentHelth -= damage;
        healthBar.SetHealth(currentHelth);
    }
}
