using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolftest : MonoBehaviour
{
     Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.U))
        {
            animator.SetBool("Run",true);
        }
        if(Input.GetKey(KeyCode.I))
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            animator.SetTrigger("attack");
        }
    }
}
