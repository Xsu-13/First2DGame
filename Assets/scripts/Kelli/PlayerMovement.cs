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

    [Header("Kelli settings")]
    [Header("Урон Келли")]
    [SerializeField]int kelliDamage = 20;

    [Header("Shon settings")]
    [SerializeField] Transform attackPoint;
    [Header("Урон Шона")]
    [SerializeField]int shonDamage = 20;
    [Header("Радиус атаки мечом")]
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;
    [Header("Кривая разгона суперспособности")]
    [SerializeField] AnimationCurve forceAnim;
    [Header("Кривая удара оленя(отскок)")]
    [SerializeField] public AnimationCurve deerForce;

    [SerializeField] int attackMana = 20;

    //изменения
   //[Header("Скорость способности разгоняться")]
   [SerializeField] [Range(0f, 10f)] float runSpeed;
    bool run = false;
    [Header("Таймер щита")]
    [SerializeField] float timerShield = 10f;
    public bool shield = false;
    float shieldTimer;

    [SerializeField]public  Material mat;
    [SerializeField] GameObject regenObj;
    Regen regen;
    //[SerializeField] float timerLimit;



    [Header("-----Inside set-----")]
    public GameObject jumpCheck;
    public LayerMask ground;
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

    PlayerMana mana;

    BoxCollider2D collider;
    Vector2 colOffset;
    Vector2 colSize;
    Vector2 prizeColOffset;
    Vector2 prizeColSize;
    Vector2 spawnerPos;

    [SerializeField] GameObject mate;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GetComponent<KelliController>();
        //player = new Player(new Kelli());
        selectPlayer = FindObjectOfType<SelectPlayer>();
        mana = GetComponent<PlayerMana>();
        regen = regenObj.GetComponent<Regen>();
        collider = GetComponent<BoxCollider2D>();

        colOffset = collider.offset;
        colSize = collider.size;
        prizeColOffset = new Vector2(0.6435308f, 1.654645f);
        prizeColSize = new Vector2(1.840971f, 2.164669f);

        spawnerPos = spawner.transform.localPosition;
        shere.GetComponent<sphere>().damage = kelliDamage;
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
                mat.SetFloat("_FillPhase", 0f);
            }
        }

        if (shield == true)
        {
            if(shieldTimer <= timerShield)
            {
                shieldTimer += Time.deltaTime;
            }
            else
            {
                //gameObject.layer = 8;
                shield = false;
                mat.SetFloat("_FillPhase", 0f);
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
                if (mana.currentMana >= attackMana)
                {
                    //animator.SetTrigger("Attack");
                    animator.SetTrigger("attack1");
                    Attack();
                }
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

            if (Input.GetKeyDown(KeyCode.S) && gcheck.isGrounded == true)
            {
                rb.velocity = new Vector2(0,0);
                animator.SetTrigger("Sqade");
                canMove = false;
                collider.offset = prizeColOffset;
                collider.size = prizeColSize;
                spawner.transform.localPosition = new Vector2(2.51f, 1.75f);
            }

            if (Input.GetKeyUp(KeyCode.S) )
            {
                animator.ResetTrigger("Sqade");
                canMove = true;
                collider.offset = colOffset;
                collider.size = colSize;
                spawner.transform.localPosition = spawnerPos;
            }

            
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                Switch();
            }


            //Суперспособности
            if (Input.GetKeyDown(KeyCode.R) && regen.regenImg.fillAmount >= 1)
            {
                //Рывок
                if (selectPlayer.player.currentCharacter == Characters.KelliCharacter && run == false && shield == false)
                {
                    //Debug.Log("Рывок");
                    gameObject.layer = 9;
                    run = true;
                    force = forceAnim.Evaluate(0);
                    regen.regenImg.fillAmount = 0;
                }
                //Щит
                if (selectPlayer.player.currentCharacter == Characters.ShonCharacter && shield == false && run == false)
                {
                    //Debug.Log("Щит");
                    //gameObject.layer = 9;
                    shield = true;
                    shieldTimer = 0;
                    mat.SetFloat("_FillPhase", 0.4f);
                    regen.regenImg.fillAmount = 0;
                }
            }


            if(jump == true)
            {
                if(Physics.CheckSphere(jumpCheck.transform.position, 0.2f, ground))
                {
                    animator.SetTrigger("land");
                }
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
            if (selectPlayer.player.currentCharacter == Characters.KelliCharacter)
                Instantiate(shere, spawner.transform.position, spawner.transform.rotation);
            else
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    EnemyHealth enemyH = enemy.GetComponent<EnemyHealth>();
                    enemyH.TakeDamage(shonDamage);
                }
            }

            mana.TakeDamage(attackMana);
        

    }
    void Jump()
    {
        animator.SetBool("Jump", true);
        jump = true;
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
        if (type == PotionType.type1)
        {
            GetComponent<PlayerMana>().AddMana(20);
        }
        if (type == PotionType.type2)
        {
            Debug.Log("FORCE!!");
            if (selectPlayer.player.currentCharacter == Characters.KelliCharacter)
            {
                kelliDamage += 15;
                shere.GetComponent<sphere>().damage = kelliDamage;
                Invoke("ResetPotionKelli", 20f);
            }
                //sphere.
            else
            {
                shonDamage += 15;
                Invoke("ResetPotionShon", 20f);
            }
        }
        if (type == PotionType.type3)
        {
            gameObject.tag = "invisPlayer";
            mate.tag = "invisPlayer";
            mat.SetFloat("_FillPhase", 0.6f);
            mate.GetComponent<PlayerMovement>().mat.SetFloat("_FillPhase", 0.6f);
            Invoke("ResetPot3", 15f);
        }
        if (type == PotionType.type4)
        {

        }
        if (type == PotionType.type5)
        {

        }
        if (type == PotionType.type6)
        {

        }
        if (type == PotionType.type7)
        {

        }
        if (type == PotionType.type8)
        {

        }
        if (type == PotionType.type9)
        {

        }
        if (type == PotionType.type10)
        {

        }
        if (type == PotionType.type11)
        {

        }

    }
    private void ResetPotionKelli()
    {
        Debug.Log("reset");
        kelliDamage -= 15;
        shere.GetComponent<sphere>().damage = kelliDamage;
    }
    private void ResetPotionShon()
    {
        Debug.Log("reset");
        shonDamage -= 15;
    }
    private void ResetPot3()
    {
        gameObject.tag = "Player";
        mate.tag = "Player";
        mat.SetFloat("_FillPhase", 0f);
        mate.GetComponent<PlayerMovement>().mat.SetFloat("_FillPhase", 0f);
    }
}
