using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public bool jump = false;
    public bool sqade = false;
    public bool attack = false;
    public bool canMove = true;
    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    Vector2 horizontalMove;
    bool LeftClickWasClicked = false;
    public GroundCheck gcheck;
    public GameObject shere;
    public GameObject spawner;
    KelliController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GetComponent<KelliController>();
    }


    void FixedUpdate()
    {
        if (controller.craftIsActive)
        {
            return;
        }
        else
        {
            horizontalMove.x = Input.GetAxis("Horizontal");
            if (horizontalMove.x != 0)
            {
                animator.SetTrigger("run");
                if (horizontalMove.x > 0)
                {
                    transform.localScale = new Vector2(1f, 1f);
                }
                else
                {
                    transform.localScale = new Vector2(-1f, 1f);
                }
            }
            else animator.ResetTrigger("run");
            animator.SetFloat("speed", Mathf.Abs(horizontalMove.x));

            if (canMove)
                rb.velocity = new Vector2(horizontalMove.x * speed, rb.velocity.y);


            if (Input.GetMouseButtonDown(0) && LeftClickWasClicked == false)
            {
                LeftClickWasClicked = true;

                animator.SetTrigger("Attack");
                //Attack();
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.ResetTrigger("Attack");
            }

            if (Input.GetMouseButtonDown(1) && LeftClickWasClicked == true)//&& attackTimer<=0)
            {
                LeftClickWasClicked = false;

                animator.SetTrigger("Attack");
                //Attack();
            }
            if (Input.GetMouseButtonUp(1))
            {
                animator.ResetTrigger("Attack");
            }

            if (Input.GetKeyDown(KeyCode.Space) && gcheck.isGrounded == true)
            {
                jump = true;
                Jump();

            }

            if (Input.GetKey(KeyCode.S) && gcheck.isGrounded == true)
            {
                animator.SetTrigger("Sqade");
                canMove = false;
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.ResetTrigger("Sqade");
                canMove = true;
            }

            if (attack == true)
                Attack();

        }
    }

    void Attack()
    {     
        Instantiate(shere, spawner.transform.position, spawner.transform.rotation);
        attack = false;
    }
    void Jump()
    {
        animator.SetTrigger("Jump");
        rb.velocity = Vector2.up * jumpForce;
        
    }
}
