using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemLayerContr : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if(collision.gameObject.CompareTag("enemy"))
        {
            rb.gravityScale = 0f;
            col.isTrigger = true;
        }
        */
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        /*
        if (collision == null || !collision.gameObject.CompareTag("enemy"))
        {
            rb.gravityScale = 3f;
            col.isTrigger = false;
        }
        */
    }

}
