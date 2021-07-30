using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : StateMachineBehaviour
{
    float speed = 3f;
    float rayDist = 2f;
    bool movingRight = false;
    public GameObject groungDetection;
    public List<Transform> wayPoints = new List<Transform>();
    Rigidbody2D rb;
    int currentWayPoint = 0;
    int size = 0;
    [SerializeField] GameObject castPoint;
    float dist = 10f;
    public bool canSeeBool;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        rb = animator.GetComponent<Rigidbody2D>();
        Transform wayPointsObject = GameObject.FindGameObjectWithTag("wayPoints").transform;

        foreach (Transform t in wayPointsObject)
        {
            wayPoints.Add(t);
            size += 1;
        }

        
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.position = Vector2.Lerp(animator.transform.position, wayPoints[currentWayPoint].position, speed * Time.deltaTime);
        Move(animator);

        if (Mathf.Abs(animator.transform.position.x - wayPoints[currentWayPoint].position.x) <= 1f)
        {
            currentWayPoint += 1;
            if (currentWayPoint == size)
                currentWayPoint = 0;

            
            Move(animator);
        }

        if (canSee(dist))
        {
            Debug.Log("can see");
            canSeeBool = true;
        }
        else
            canSeeBool = false;
        
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    void Move(Animator animator)
    {
        //Vector3 move = wayPoints[currentWayPoint].position - animator.transform.position ;
        //rb.MovePosition(animator.transform.position + move * speed* Time.deltaTime);
        if(wayPoints[currentWayPoint].position.x < animator.transform.position.x)
            rb.velocity = new Vector2(-speed, 0);
        else
            rb.velocity = new Vector2(speed, 0);
    }

    bool canSee(float dist)
    {
        bool val = false;

        RaycastHit2D hit = Physics2D.Raycast(castPoint.transform.position, -castPoint.transform.right, dist);

        if (hit.collider != null)
        {           
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else val = false;
        }
            
        return val;
    }
}
