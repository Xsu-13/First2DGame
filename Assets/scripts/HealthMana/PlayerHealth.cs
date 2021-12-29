using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class PlayerHealth : MonoBehaviour
{
    private int maxHelth = 500;
    public int currentHelth;
    public HealthBar healthBar;
    [SerializeField] GameObject gameOver;
    PlayerMovement playerMovement;
    Animator animator;
    [SerializeField] Material mat;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        //currentHelth = maxHelth;
        //healthBar.SetMaxHealth(maxHelth);
    }


    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(50);*/

        if (currentHelth <= 0)
        {
            SceneManager.LoadScene("Game Over");
            //animator.SetBool("die", true);
            //playerMovement.enabled = false;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHelth -= damage;
        healthBar.SetHealth(currentHelth);
        mat.SetFloat("_FillPhase", 0.4f);
        Invoke("Return", 0.2f);
    }

    public void SetHealth(int health)
    {
        currentHelth = health;
        healthBar.SetHealth(currentHelth);
    }
    public void AddHealth(int health)
    {
        currentHelth += health;
        healthBar.SetHealth(currentHelth);
    }

    public void Return()
    {
        mat.SetFloat("_FillPhase", 0f);
    }
}
