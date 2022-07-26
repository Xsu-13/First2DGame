using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
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
        if(Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("dash");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("run");
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("idle");
        }

    }
}
