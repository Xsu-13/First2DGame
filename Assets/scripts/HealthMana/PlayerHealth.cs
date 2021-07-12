using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHelth = 300;
    public int currentHelth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHelth = maxHelth;
        healthBar.SetMaxHealth(maxHelth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TakeDamage(int damage)
    {
        currentHelth -= damage;
        healthBar.SetHealth(currentHelth);
    }
}
