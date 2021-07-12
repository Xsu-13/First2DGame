using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public GameObject potion;
    public GameObject sphere;
    public GameObject spawner;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(potion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    void Start()
    {
        
    }


    void Update()
    {
        //if (health >= 0)
            //Instantiate(sphere, spawner.transform.position, spawner.transform.rotation);
    }
}
