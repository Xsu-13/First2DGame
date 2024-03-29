using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Head : MonoBehaviour
{
    [Header("Custom settings")]
    public List<GameObject> waypoints = new List<GameObject>();
    [Header("��������")]
    public float speed;
    [Header("���������� �� ����� ����(��������� �������)")]
    public float nextWaypointDistance = 3f;
    [Header("����������, �� ������� ���� ����� ������")]
    public float visDist = 20f;
    [Header("���������� ������ �����")]
    public float visAttack = 3f;
    [Header("����")]
    int damage = 30;
    [Header("-----Inside set------")]
    Transform player1;
    Transform player2;
    public State currentState;
    public GameObject sphere;
    public GameObject spawner;
    //public string state;
    //public Transform groungDetection;
    //public GameObject castPoint;
    Animator anime;
    bool pursue;
    Rigidbody2D rb;
    public Transform target;
    Path path;
    int currentWaypointSeeker = 0;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    enemySphere enemySphere;
    PlayerMovement[] players;

    void Start()
    {
        players = Resources.FindObjectsOfTypeAll<PlayerMovement>();
        player1 = players[0].transform;
        player2 = players[1].transform;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        currentState = new Idle(gameObject, player1, player2);
        enemySphere= sphere.GetComponent<enemySphere>();
        enemySphere.damage = damage;
    }


    void FixedUpdate()
    {
        
        if(pursue)
        {
            PursuePlayer();
        }
        

        currentState = currentState.Process();

    }
    
    public void PursuePlayer()
    {
        if (path == null)
            return;
        if (currentWaypointSeeker >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypointSeeker] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypointSeeker]);

        if (distance < nextWaypointDistance)
        {
            currentWaypointSeeker++;
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypointSeeker = 0;
        }
    }

    void UpdatePath()
    {
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    public void Repeate()
    {
          InvokeRepeating("UpdatePath", 0, 0.5f);
    }

    public void AttackPlayer()
    {
        Transform player;
        if (player1.gameObject.activeInHierarchy)
            player = player1;
        else
            player = player2;

        Quaternion rot = Quaternion.LookRotation( player.position - transform.position);
        Instantiate(sphere, spawner.transform.position, spawner.transform.rotation);
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
        //protected Transform groundDetection;
        //protected GameObject castPoint;
        protected Transform player;


        public Transform target;
        public float speed;
        public float nextWaypointDistance = 3f;

        //int currentWaypoint = 0;

        float visDist = 20f;
        float visAttack = 12f;

        public State(GameObject _enemy, Transform _player1, Transform _player2)
        {
            enemy = _enemy;
            player1 = _player1;
            player2 = _player2;
            stage = EVENT.ENTER;
            Head head = enemy.GetComponent<Head>();
            waypoints = head.waypoints;
            rb = enemy.GetComponent<Rigidbody2D>();
            target = head.target;
            visDist = head.visDist;
            visAttack = head.visAttack;
            //groundDetection = head.groungDetection;
            //castPoint = head.castPoint;

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
            if (player.tag == "invisPlayer")
            {
                return false;
            }
            Vector3 direction = player.position - enemy.transform.position;
            if (direction.magnitude < visDist)
            {
                return true;
            }
            return false;
        }

        public bool CanAttackPlayer(Transform player)
        {            
                Vector3 direction = player.position - enemy.transform.position;
                if (direction.magnitude < visAttack)
                {
                    return true;

                }
                return false;
        }

        /*
        public bool canSee()
        {
            bool val = false;

           // RaycastHit2D hit = Physics2D.Raycast(castPoint.transform.position, Vector2.right * new Vector2(enemy.transform.localScale.x, 0), 18);
           
            if (hit.collider != null)
            {
                //Debug.DrawLine(castPoint.transform.position,hit.transform.position, Color.red);
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    val = true;
                    //Debug.Log("seeeee");
                }
                else val = false;
            }
           
            return val;
           
        }
        */
    }

    public class Idle : State
    {

        public Idle(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
        {
            name = STATE.IDLE;
        }

        public override void Enter()
        {
            base.Enter();
            timer = Random.Range(1f, 2.5f);
        }

        public override void Update()
        {
            if (player1.gameObject.activeInHierarchy)
                player = player1;
            else
                player = player2;


            if (CanAttackPlayer(player))
            {
                nextState = new Attack(enemy, player1, player2);
                stage = EVENT.EXIT;
            }
            else if (CanSeePlayer())
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
        Head enemyHead;

        public Pursue(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
        {
            name = STATE.PURSUE;
        }

        
        public override void Update()
        {

           

            if (player1.gameObject.activeInHierarchy)
            {
                player = player1;
                enemyHead.target = player1;
            }
            else
            {
                player = player2;
                enemyHead.target = player2;
            }

            //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);
            /*
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
            */

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
                enemy.transform.localScale = new Vector2(-1, 1);
            }
            else
            { 
                enemy.transform.localScale = new Vector2(1, 1);
            }
        }

        public override void Enter()
        {
            enemyHead = enemy.GetComponent<Head>();
            enemyHead.pursue = true;
            enemyHead.Repeate();
            base.Enter();

        }
        public override void Exit()
        {
            enemyHead.pursue = false;
            base.Exit();
        }
    }

    public class Patrol : State
    {
        Head enemyHead;

        public Patrol(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
        {
            name = STATE.PATROL;
            //agent.speed = 2f;
            enemyHead = enemy.GetComponent<Head>();
        }

        public override void Update()
        {
            
            //���������������
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
                enemyHead.target = enemyHead.waypoints[currentWaypoint].transform;
                if (Mathf.Abs(enemy.transform.position.x - waypoints[currentWaypoint].transform.position.x) <= 4f)
                {
                    currentWaypoint += 1;                    
                    if (currentWaypoint == waypoints.Count)
                        currentWaypoint = 0;
                    if((enemyHead.waypoints[currentWaypoint].transform.position.x>enemy.transform.position.x))
                        enemy.transform.localScale = new Vector2(-1, 1);
                    else
                        enemy.transform.localScale = new Vector2(1, 1);

                    enemyHead.currentWaypoint = currentWaypoint;
                    enemyHead.target = enemyHead.waypoints[currentWaypoint].transform;

                }
                if ((enemyHead.waypoints[currentWaypoint].transform.position.x > enemy.transform.position.x))
                    enemy.transform.localScale = new Vector2(-1, 1);
                else
                    enemy.transform.localScale = new Vector2(1, 1);
            }
        }
        public override void Enter()
        {
            enemyHead.pursue = true;
            enemyHead.Repeate();
            base.Enter();
        }
        public override void Exit()
        {
            enemyHead.pursue = false;
            base.Exit();
        }

        public void Move()
        {
           
          
            /*
            if (waypoints[currentWaypoint].transform.position.x < enemy.transform.position.x)
            {
                rb.velocity = new Vector2(-2, 0);
                enemy.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                rb.velocity = new Vector2(2, 0);
                enemy.transform.localScale = new Vector2(1, 1);
            }
            */
        }
    }

    public class Attack : State
    {
        Head enemyHead;

        public Attack(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
        {
            name = STATE.ATTACK;
            //agent.speed = 0;
            enemyHead = enemy.GetComponent<Head>();
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
                enemyHead.anime.SetTrigger("Attack");
                enemyHead.AttackPlayer();
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
            if (CanSeePlayer())
            {
                if (!CanAttackPlayer(player))
                {
                    nextState = new Pursue(enemy, player1, player2);
                    stage = EVENT.EXIT;
                }

            }

            /*
            if ((player.transform.position - enemy.transform.position).magnitude < 18 && !CanSeePlayer())
            {

                int sc = 1;
                if (player.transform.position.x - enemy.transform.position.x <= 0)
                    sc = -1;

                enemy.transform.localScale = new Vector2(sc, 1);


                nextState = new Idle(enemy, player1, player2);
                stage = EVENT.EXIT;
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
