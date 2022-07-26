using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMushroom : MonoBehaviour
{
    Animator animator;
    [Header("����������� ���� ������")]
    [SerializeField] float k;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("groundCheck"))
        {
            animator.SetTrigger("Bounce");

            PlayerMovement player = collision.transform.parent.GetComponent<PlayerMovement>();
            player.Jump(player.jumpForce * k);
        }
    }
}
