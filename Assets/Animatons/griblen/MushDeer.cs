using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushDeer : MonoBehaviour
{
    [Header("Custom settings")]
    public List<GameObject> waypoints = new List<GameObject>();
    [Header("Радиус атаки")]
    [SerializeField] float attackRange;
    [Header("Расстояние, на котором враг видит игрока")]
    public float visDist = 20f;
    [Header("Расстояние начала атаки(уже нормально выставлено, не может быть меньше 3)")]
    public float visAttack = 3f;
    [Header("Скорость хотьбы")]
    public float walkSpeed = 3f;
    [Header("Скорость бега")]
    public float runSpeed = 8f;
    [Header("Сила толчка")]
    [SerializeField] float force = 50f;
    [Header("Урон")]
    [SerializeField] int damage = 10;

    
   
    [Header("-----Inside set-----")]
    public Transform attackPoint;
    public string state;
    public Transform groungDetection;
    [SerializeField] LayerMask playerLayer;
    Animator anim;
    public bool finishedAttack;
    PlayerMovement[] players;
    Transform player1;
    Transform player2;
    public State currentState;

    void Start()
    {
        players = Resources.FindObjectsOfTypeAll<PlayerMovement>();
        player1 = players[0].transform;
        player2 = players[1].transform;

        currentState = new Idle(gameObject, player1, player2);
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        currentState = currentState.Process();
        state = currentState.name.ToString();
    }


    public void AttackPlayer()
    {
        //finishedAttack = false;
        //anim.SetTrigger("attack");
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D player in players)
        {
            PlayerMovement pm =  player.GetComponent<PlayerMovement>();
            if(transform.localScale.x >0)
            {
                pm.forceee = -force;
            }
            else
            {
                pm.forceee = force;
            }
            pm.fff = true;
            pm.deerforce = pm.deerForce.Evaluate(0);
            PlayerHealth playerH = player.GetComponent<PlayerHealth>();
            playerH.TakeDamage(damage);
        }
    }



    public class State
    {
        public enum STATE
        {
            IDLE, PATROL, PURSUE, ATTACK
        }
        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        }

        public STATE name;
        protected EVENT stage;
        protected Transform player1;
        protected Transform player2;
        protected GameObject enemy;
        protected State nextState;
        protected List<GameObject> waypoints;
        protected Rigidbody2D rb;
        protected int currentWaypoint = 0;
        protected float timer;
        protected Transform groundDetection;
        protected GameObject castPoint;
        protected Transform player;
        protected MushDeer deer;
        protected Animator anim;

        protected float runSpeed;
        protected float walkSpeed;
        float visDist = 20f;
        float visAttack = 3f;

        public State(GameObject _enemy, Transform _player1, Transform _player2)
        {
            enemy = _enemy;
            player1 = _player1;
            player2 = _player2;
            stage = EVENT.ENTER;
            deer = enemy.GetComponent<MushDeer>();
            groundDetection = deer.groungDetection;
            waypoints = deer.waypoints;
            rb = enemy.GetComponent<Rigidbody2D>();
            anim = deer.anim;
            visAttack = deer.visAttack;
            visDist = deer.visDist;
            runSpeed = deer.runSpeed;
            walkSpeed = deer.walkSpeed;
            //groundDetection = enemyfsm.groungDetection;
            //castPoint = enemyfsm.castPoint;

        }

        public virtual void Enter() { stage = EVENT.UPDATE; }
        public virtual void Update() { stage = EVENT.UPDATE; }
        public virtual void Exit() { stage = EVENT.EXIT; }

        public State Process()
        {
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update();
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState;
            }
            return this;
        }

        public bool CanSeePlayer()
        {
            Vector3 direction = player.position - enemy.transform.position;
            if (direction.magnitude < visDist)
            {
                return true;
            }
            return false;
        }

        public bool CanAttackPlayer(Transform player)
        {

            if (CanSeePlayer())
            {
                Vector3 direction = player.position - enemy.transform.position;
                if (direction.magnitude < visAttack)
                {
                    return true;
                }
                return false;

            }
            return false;
        }

    }

    public class Idle : State
    {

        public Idle(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
        {
            name = STATE.IDLE;

        }

        public override void Enter()
        {
            //???
            //anim.SetTrigger("idle");
            base.Enter();
            timer = Random.Range(1f, 2.5f);
        }

        public override void Update()
        {
            if (player1.gameObject.activeInHierarchy)
                player = player1;
            else
                player = player2;

            if (CanAttackPlayer(player) && Mathf.Abs(player.transform.position.y - enemy.transform.position.y) <= 2f)
            {
                anim.SetTrigger("attack");
                nextState = new Attack(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else if (CanSeePlayer() && Mathf.Abs(player.transform.position.y - enemy.transform.position.y) <= 2f)
            {
                nextState = new Pursue(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            

            if (timer > 0)
            {
                rb.velocity = new Vector2(0, 0);
                timer -= Time.deltaTime;

            }
            else if (Random.Range(0, 1000) < 10)
            {
                nextState = new Patrol(enemy, player1, player2);
                stage = EVENT.EXIT;
            }

        }

        public override void Exit()
        {
            base.Exit();
            currentWaypoint = Random.Range(0, 1);
        }
    }

    public class Pursue : State
    {
        public Pursue(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
        {
            name = STATE.PURSUE;
        }

        public override void Update()
        {
            if (player1.gameObject.activeInHierarchy)
                player = player1;
            else
                player = player2;

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);

            if (groundInfo.collider == false)
            {
                if (CanAttackPlayer(player))
                {
                    nextState = new Attack(enemy, player1, player2);
                    stage = EVENT.EXIT;
                }
                else
                {
                    nextState = new Idle(enemy, player1, player2);
                    stage = EVENT.EXIT;
                }
            }

            if (CanAttackPlayer(player) && Mathf.Abs(player.transform.position.y - enemy.transform.position.y)<=3f)
            {
                nextState = new Attack(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer() || Mathf.Abs(player.transform.position.y - enemy.transform.position.y) >= 3f)
            {
                nextState = new Patrol(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else if (player.position.x > enemy.transform.position.x)
            {
                rb.velocity = new Vector2(runSpeed, 0);
                enemy.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                rb.velocity = new Vector2(-runSpeed, 0);
                enemy.transform.localScale = new Vector2(1, 1);
            }
        }

        public override void Enter()
        {
            anim.SetTrigger("run");
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Patrol : State
    {

        public Patrol(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
        {
            name = STATE.PATROL;
        }

        public override void Update()
        {
            if (player1.gameObject.activeInHierarchy)
                player = player1;
            else
                player = player2;

            if (CanSeePlayer() && Mathf.Abs(player.transform.position.y - enemy.transform.position.y) <= 2f)
            {
                nextState = new Pursue(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else if (Random.Range(0, 30000) < 10)
            {
                nextState = new Idle(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else
            {
                Move();

                if (Mathf.Abs(enemy.transform.position.x - waypoints[currentWaypoint].transform.position.x) <= 1f)
                {
                    currentWaypoint += 1;
                    if (currentWaypoint == waypoints.Count)
                        currentWaypoint = 0;
                    Move();
                }
            }
        }
        public override void Enter()
        {
            anim.SetTrigger("move");
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }

        public void Move()
        {
            if (waypoints[currentWaypoint].transform.position.x < enemy.transform.position.x)
            {
                rb.velocity = new Vector2(-walkSpeed, 0);
                enemy.transform.localScale = new Vector2(1, 1);
            }
            else
            {
                rb.velocity = new Vector2(walkSpeed, 0);
                enemy.transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    public class Attack : State
    {
        public Attack(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
        {
            name = STATE.ATTACK;
            timer = 0f;
        }

        public override void Update()
        {
            if (player1.gameObject.activeInHierarchy)
                player = player1;
            else
                player = player2;

            rb.velocity = new Vector2(0, 0);
            
            deer.AttackPlayer();
            
            
            


            if (player.position.x > enemy.transform.position.x)
            {
                enemy.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                enemy.transform.localScale = new Vector2(1, 1);
            }

            //if (timer <= 0)
            //{
                nextState = new Idle(enemy, player1, player2);
                stage = EVENT.EXIT;
            //}
            /*
                if (!canSee())
                {
                    if (player.transform.position.x - enemy.transform.position.x < 18)
                    {
                        enemy.transform.localScale = new Vector2(-enemy.transform.localScale.x, 15);

                        if (!canSee())
                        {
                            nextState = new Idle(enemy, player1, player2);
                            stage = EVENT.EXIT;
                        }

                    }

                }
            */



            /*
            if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) > 18f)
            {
                nextState = new Patrol(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            */
            /*
            if (CanSeePlayer())
            {
                //&& deer.finishedAttack
                if (!CanAttackPlayer(player) )
                {
                    nextState = new Pursue(enemy, player1, player2);
                    stage = EVENT.EXIT;
                }

            }
            */
            /*
            if (!canSee() && (player.transform.position - enemy.transform.position).magnitude <18)
            {
                enemy.transform.localScale = new Vector2(-enemy.transform.localScale.x, 15);
                nextState = new Pursue(enemy, player1, player2);
                stage = EVENT.EXIT;
            }*/

        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}
