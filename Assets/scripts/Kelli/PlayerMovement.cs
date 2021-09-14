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
    //Player player;
    SelectPlayer selectPlayer;
    public GameObject partner;
    Vector3 correctPos;
    //public GameObject selectedCharacter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GetComponent<KelliController>();
        //player = new Player(new Kelli());
        selectPlayer = FindObjectOfType<SelectPlayer>();
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


            if (Input.GetMouseButtonDown(0))
            {

                animator.SetTrigger("Attack");
                Attack();
            }
            if (Input.GetMouseButtonUp(0))
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

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                Switch();
            }
            

        }
    }

    void Attack()
    {
        selectPlayer.player.Attack();
        //корректуируем
        //Instantiate(shere, spawner.transform.position, spawner.transform.rotation);
    }
    void Jump()
    {
        animator.SetTrigger("Jump");
        rb.velocity = Vector2.up * jumpForce;
        
    }
    void Switch()
    {
        int rotate = 1;
        selectPlayer.player.Switch();
        if (transform.localScale.x == -1)
            rotate = -1;
        if (selectPlayer.player.currentCharacter == Characters.KelliCharacter)            
            correctPos = new  Vector3(-0.3f*rotate, -0.5f, 0);
        else correctPos = new Vector3(0.3f*rotate, 0.5f, 0);

        partner.transform.position = transform.position + correctPos;
        partner.transform.localScale = transform.localScale;
        partner.SetActive(true);
        gameObject.SetActive(false);
    }
}
