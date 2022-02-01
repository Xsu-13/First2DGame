using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Custom settings")]
    [Header("��������")]
    [SerializeField] public int health;
    [Header("������ ����� ������, ���� �� ����")]
    public GameObject deathEffect;
    [Header("���������� �������, ���� ����")]
    public GameObject potion;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("health");
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
