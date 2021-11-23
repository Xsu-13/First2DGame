using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();
    public Transform player1;
    public Transform player2;
    State currentState;
    public GameObject sphere;
    public GameObject spawner;
    public string state;
    public Transform groungDetection;
    public GameObject castPoint;

    //public NavMeshAgent agent;

    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        currentState = new Idle(gameObject, player1,player2);

    }


    void Update()
    {
        currentState = currentState.Process();
        state = currentState.name.ToString();
    }

    public void Attack()
    {
        Instantiate(sphere, spawner.transform.position, spawner.transform.rotation);
    }

}

public class State
{ 
    public enum STATE
    {
        IDLE,PATROL,PURSUE,ATTACK
    }
    public enum EVENT
    {
        ENTER,UPDATE,EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected Transform player1;
    protected Transform player2;
    protected GameObject enemy;
    protected State nextState;
    protected List<GameObject> waypoints;
    protected Rigidbody2D rb;
    protected NavMeshAgent agent;
    protected int currentWaypoint = 0;
    protected float timer;
    protected Transform groundDetection;
    protected GameObject castPoint;
    protected Transform player;

    float visDist = 20f;
    float visAttack = 12f;

    public State(GameObject _enemy, Transform _player1, Transform _player2)
    {
        enemy = _enemy;
        player1 = _player1;
        player2 = _player2;
        stage = EVENT.ENTER;
        EnemyFSM enemyfsm = enemy.GetComponent<EnemyFSM>();
        waypoints = enemyfsm.waypoints;
        rb = enemy.GetComponent<Rigidbody2D>();
        groundDetection = enemyfsm.groungDetection;
        castPoint = enemyfsm.castPoint;
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
        if (canSee())
        {
            Vector3 direction = player.position - enemy.transform.position;
            if (direction.magnitude < visAttack)
            {
                Debug.Log("attack");
                return true;

            }
            Debug.Log("not attack");
            return false;

        }
        return false;
    }
    public bool canSee()
    {
        bool val = false;

        RaycastHit2D hit = Physics2D.Raycast(castPoint.transform.position, Vector2.right * new Vector2(enemy.transform.localScale.x,0), 18);
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

}

public class Idle : State
{

    public Idle(GameObject _enemy, Transform _player1, Transform _player2):base(_enemy, _player1, _player2)
    {
        name = STATE.IDLE;
        //agent.speed = 0f;
        
    }

    public override void Enter()
    {
        base.Enter();
        timer = Random.Range(1f, 4f);
    }

    public override void Update()
    {
        if (player1.gameObject.activeInHierarchy)
            player = player1;
        else
            player = player2;


        if (canSee())
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

public class Pursue: State
{
    public Pursue(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
    {
        name = STATE.PURSUE;
        //agent.speed = 4f;
    }

    public override void Update()
    {
        //
        if(player1.gameObject.activeInHierarchy)
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
        Debug.Log("fffffff");
        //agent.SetDestination(player.position);
        

        if (CanAttackPlayer(player))
        {
            nextState = new Attack(enemy, player1, player2);
            stage = EVENT.EXIT;
        }
        else if (!canSee())
        {
            nextState = new Patrol(enemy, player1, player2);
            stage = EVENT.EXIT;
        }
        else if (player.position.x > enemy.transform.position.x)
        {
            rb.velocity = new Vector2(5f, 0);
            enemy.transform.localScale = new Vector2(15, 15);
        }
        else
        {
            rb.velocity = new Vector2(-5f, 0);
            enemy.transform.localScale = new Vector2(-15, 15);
        }
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

        if (canSee())
        {
            nextState = new Pursue(enemy, player1,player2);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 30000) < 10)
        {
            nextState = new Idle(enemy, player1,player2);
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
                Vector2 pos = waypoints[currentWaypoint].transform.position;
                waypoints[currentWaypoint].transform.position = new Vector2(waypoints[currentWaypoint].GetComponent<waypointRange>().RandomPos(), pos.y);
                Move();
            }
        }
    }
    public override void Enter()
    {
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
            rb.velocity = new Vector2(-2, 0);
            //agent.SetDestination(waypoints[currentWaypoint].transform.position);
            enemy.transform.localScale = new Vector2(-15, 15);
        }
        else
        {
            rb.velocity = new Vector2(2, 0);
            //agent.SetDestination(waypoints[currentWaypoint].transform.position);
            enemy.transform.localScale = new Vector2(15, 15);
        }
    }
}

public class Attack : State
{
    public Attack(GameObject _enemy, Transform _player1, Transform _player2) : base(_enemy, _player1, _player2)
    {
        name = STATE.ATTACK;
        //agent.speed = 0;
        timer = 0f;
    }

    public override void Update()
    {
        //
        if (player1.gameObject.activeInHierarchy)
            player = player1;
        else
            player = player2;

        rb.velocity = new Vector2(0, 0);
        if (timer <= 0)
        {
            enemy.GetComponent<EnemyFSM>().Attack();
            timer = 1.5f;
        }
        else timer -= Time.deltaTime;

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
        if (canSee())
        {
            if (!CanAttackPlayer(player))
            {
                nextState = new Pursue(enemy, player1, player2);
                stage = EVENT.EXIT;
            }

        }
        if((player.transform.position - enemy.transform.position).magnitude < 18 && !canSee())
        {
            
            int sc = 15;
            if (player.transform.position.x - enemy.transform.position.x <= 0)
                sc = -15;
            
            enemy.transform.localScale = new Vector2(sc,15);


            nextState = new Idle(enemy, player1, player2);
            stage = EVENT.EXIT;            
        }
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
