using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Custom settings")]
    [Header("Скорость")]
    public float speed;
    [Header("Сила прыжка")]
    public float jumpForce;
    

    [Header("Shon settings")]
    [SerializeField] Transform attackPoint;
    [Header("Радиус атаки мечом")]
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;
    [Header("Кривая разгона суперспособности")]
    [SerializeField] AnimationCurve forceAnim;
    [Header("Кривая удара оленя(отскок)")]
    [SerializeField] public AnimationCurve deerForce;


    //изменения
    [Header("Скорость способности разгоняться")]
    [SerializeField] [Range(0f, 10f)] float runSpeed;
    bool run = false;
    //[SerializeField] float timerLimit;

    

    [Header("-----Inside set-----")]
    public bool fff;
    Vector2 horizontalMove;
    public Rigidbody2D rb;
    //Player player;
    SelectPlayer selectPlayer;
    public GameObject partner;
    Vector3 correctPos;
    public GroundCheck gcheck;
    public GameObject shere;
    public GameObject spawner;
    //public GameObject selectedCharacter;
    //bool LeftClickWasClicked = false;
    KelliController controller;
    float force;
    float time;
    float time1;

    public float deerforce;
    public float forceee;

    public Animator animator;
    public bool jump = false;
    public bool sqade = false;
    public bool attack = false;
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GetComponent<KelliController>();
        //player = new Player(new Kelli());
        selectPlayer = FindObjectOfType<SelectPlayer>();

        
    }

    private void FixedUpdate()
    {
        if (fff)
        {
            /*
            time1 += Time.deltaTime;
            deerforce = forceAnim.Evaluate(time1);
            //rb.MovePosition(new Vector2(startPosit.x + time1*0.00001f, startPosit.y + deerforce));

            //transform.position = Vector3.Lerp(transform.position, new Vector3(force, 0, 0) + transform.position, runSpeed * Time.deltaTime);

            if (deerForce.Evaluate(time1) <= 0)
            {
                fff = false;
                time1 = 0;
            }
            */
            time1 += Time.deltaTime;
            deerforce = forceAnim.Evaluate(time1);
            transform.position = Vector3.Lerp(transform.position, new Vector3(time1 * forceee, deerforce, 0) + transform.position, 5f * Time.deltaTime);
            if (deerForce.Evaluate(time1) <= 0)
            {
                fff = false;
                time1 = 0;
            }
        }
        if (run == true)
        {
            time += Time.deltaTime;
            force = forceAnim.Evaluate(time);
            if (transform.localScale.x < 0)
                force *= -1f;
            transform.position = Vector3.Lerp(transform.position, new Vector3(force, 0, 0) + transform.position, runSpeed * Time.deltaTime);

            if (forceAnim.Evaluate(time) <= 0)
            {
                gameObject.layer = 8;
                run = false;
                time = 0;
            }
        }
    }
    void Update()
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

            if (Input.GetKeyUp(KeyCode.S) && sqade == true)
            {
                animator.ResetTrigger("Sqade");
                //sqade = false;
                canMove = true;
            }

            
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                Switch();
            }
            
            //Рывок
            if (Input.GetKeyDown(KeyCode.R) && selectPlayer.player.currentCharacter == Characters.KelliCharacter)
            {
                gameObject.layer = 9;
                run = true;
                force = forceAnim.Evaluate(0);
            }
            
            

            //Для проверки
            /*
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                GetComponent<PlayerHealth>().TakeDamage(30);
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                GetComponent<PlayerMana>().TakeDamage(20);
            }
            */
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

    public void PotionActivate(PotionType type)
    {
        if(type == PotionType.type0)
        {
            GetComponent<PlayerHealth>().AddHealth(20);
        }
    }
}
