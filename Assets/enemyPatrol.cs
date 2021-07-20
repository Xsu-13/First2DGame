using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : StateMachineBehaviour
{
    float speed = 1f;
    float rayDist = 2f;
    bool movingRight = false;
    [SerializeField] Transform groungDetection;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groungDetection.position, Vector2.down, rayDist);

        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                animator.transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                animator.transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
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
}
