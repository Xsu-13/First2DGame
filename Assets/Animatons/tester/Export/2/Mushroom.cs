using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Animator animator;
    [Header("Коэффициент силы прыжка")]
    [SerializeField] float k;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("groundCheck"))
        {
            animator.SetTrigger("Bounce");

            PlayerMovement player = collision.transform.parent.GetComponent<PlayerMovement>();
            player.Jump(player.jumpForce * k);
        }
    }
}
