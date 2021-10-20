using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    
    [SerializeField] float agroRange = 30f;
    [SerializeField] float speed = 1;
    [SerializeField] float groundRayDist = 2f;
    public bool movingRight = false;
    public Transform groungDetection;
    GameObject player;
    Rigidbody2D rb;
    bool isFacingLeft;
    public GameObject sphere;
    public GameObject spawner;
    public GameObject castPoint;
    public Vector2 endPos;
    

    public List<Transform> wayPoints = new List<Transform>();
    int currentWayPoint = 0;
    int size = 0;
    float dist = 10f;
    bool isPatrolling = true;
    bool isChasing = false;
    float patrolSpeed;
    float attackTimer = 0;
    public Vector3 right;
   

   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        patrolSpeed = speed;
        right = -transform.right;

        Transform wayPointsObject = GameObject.FindGameObjectWithTag("wayPoints").transform;

        foreach (Transform t in wayPointsObject)
        {
            wayPoints.Add(t);
            size += 1;
        }
    }


    void FixedUpdate()
    {
        attackTimer += Time.deltaTime;

        if (transform.localScale.x >= 0)
            right = transform.right;
        else
            right = -transform.right;


        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        #region patrolling
        if (isPatrolling)
        {
            Move();

            if (Mathf.Abs(transform.position.x - wayPoints[currentWayPoint].position.x) <= 1f)
            {
                currentWayPoint += 1;
                if (currentWayPoint == size)
                    currentWayPoint = 0;


                Move();
            }
        }
        #endregion


        if (canSee(dist))
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 10f)
            {
                StopChasingPlayer();
                if (attackTimer >= 1)
                {
                    AttackPlayer();
                    attackTimer = 0;
                }
            }
            else if (distToPlayer <= agroRange)
                ChasePlayer();
            else if (distToPlayer > agroRange)
                StopChasingPlayer();


        }
        else
        {
            speed = patrolSpeed;
            isPatrolling = true;
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groungDetection.position, Vector2.down, groundRayDist);

        if (groundInfo.collider == false)
        {
            isPatrolling = true;
            isChasing = false;
        }
        //else if (!isChasing)
        //StopChasingPlayer();

        //if (health >= 0)
        //Instantiate(sphere, spawner.transform.position, spawner.transform.rotation);
    }

    void ChasePlayer()
    {
        isPatrolling = false;
        isChasing = true;
        speed = speed + 1f;

        if (transform.position.x < player.transform.position.x )
        {
            rb.velocity = new Vector2(speed, 0);
            //transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = true;
            
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.localScale = new Vector2(15, 15);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            //transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector2(-15, 15);
        }
    }

    void StopChasingPlayer()
    {
        isChasing = false;
        rb.velocity = new Vector2(0, 0);
    }


#region canSee
    bool canSee(float dist)
    {
        bool val = false;
        
        RaycastHit2D hit = Physics2D.Raycast(castPoint.transform.position, right, dist);

        if (hit.collider != null)
        {
            Debug.DrawLine(castPoint.transform.position, hit.point, Color.red);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else val = false;
        }

        return val;
    }
    #endregion

    void Move()
    {
        
        if (wayPoints[currentWayPoint].position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            //transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector2(-15, 15);
            movingRight = false;
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
            //transform.eulerAngles = new Vector3(0, -180, 0);
            transform.localScale = new Vector2(15, 15);
            movingRight = true;
        }
    }

    void AttackPlayer()
    {
        //isChasing = false;
        Instantiate(sphere, spawner.transform.position, spawner.transform.rotation);
    }
}
