using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushDeer : MonoBehaviour
{
    public bool finishedAttack;


    public List<GameObject> waypoints = new List<GameObject>();
    public Transform player1;
    public Transform player2;
    State currentState;
    public Transform attackPoint;
    public string state;
    public Transform groungDetection;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayer;
    Animator anim;

    //public NavMeshAgent agent;

    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
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
        finishedAttack = false;
        anim.SetTrigger("attack");
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D player in players)
        {
            PlayerMovement pm =  player.GetComponent<PlayerMovement>();
            if(transform.localScale.x >0)
            {
                pm.forceee = -50f;
            }
            else
            {
                pm.forceee = 50f;
            }
            pm.fff = true;
            pm.deerforce = pm.deerForce.Evaluate(0);
            PlayerHealth playerH = player.GetComponent<PlayerHealth>();
            playerH.TakeDamage(20);
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

            //groundDetection = enemyfsm.groungDetection;
            //castPoint = enemyfsm.castPoint;
            //agent = enemyfsm.agent;

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
            //agent.speed = 0f;

        }

        public override void Enter()
        {
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


            if (CanSeePlayer())
            {
                nextState = new Pursue(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else if (CanAttackPlayer(player))
            {
                nextState = new Attack(enemy, player1, player2);
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


            if (CanAttackPlayer(player))
            {
                nextState = new Attack(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else if (player.position.x > enemy.transform.position.x)
            {
                rb.velocity = new Vector2(8f, 0);
                enemy.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                rb.velocity = new Vector2(-8f, 0);
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
            //agent.speed = 2f;
        }

        public override void Update()
        {
            //скорректировать
            if (player1.gameObject.activeInHierarchy)
                player = player1;
            else
                player = player2;

            if (CanSeePlayer())
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
                rb.velocity = new Vector2(-3, 0);
                enemy.transform.localScale = new Vector2(1, 1);
            }
            else
            {
                rb.velocity = new Vector2(3, 0);
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
            if (timer <= 0)
            {
                deer.AttackPlayer();
                timer = 1.5f;
            }
            else timer -= Time.deltaTime;


            if (player.position.x > enemy.transform.position.x)
            {
                enemy.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                enemy.transform.localScale = new Vector2(1, 1);
            }

            if(deer.finishedAttack == true)
            {
                nextState = new Idle(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
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
