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

    [Header("Shon settings")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;

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

                //animator.SetTrigger("Attack");
                animator.SetTrigger("attack1");
                Attack();
            }
            /*
            if (Input.GetMouseButtonUp(0))
            {
                animator.ResetTrigger("Attack");
            }
            */

            if (Input.GetKeyDown(KeyCode.Space) && gcheck.isGrounded == true)
            {
                jump = true;
                Jump();

            }

            if (Input.GetKey(KeyCode.S) && gcheck.isGrounded == true)
            {
                animator.SetTrigger("Sqade");
                //sqade = true;
                canMove = false;
            }

            if (!Input.GetKeyUp(KeyCode.S) && sqade == true)
            {
                animator.ResetTrigger("Sqade");
                //sqade = false;
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
        if(selectPlayer.player.currentCharacter == Characters.KelliCharacter)
            Instantiate(shere, spawner.transform.position, spawner.transform.rotation);
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies)
            {
                EnemyHealth enemyH = enemy.GetComponent<EnemyHealth>();
                enemyH.TakeDamage(20);
            }
        }
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
