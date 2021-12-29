using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    public GameObject deathEffect;
    public GameObject potion;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        if ((deathEffect != null) && (potion != null))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(potion, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
