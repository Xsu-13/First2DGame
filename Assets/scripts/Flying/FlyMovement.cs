using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{    
    Vector2 movementInput;
    public Rigidbody2D rb;
    public float speed = 3f;
    public Camera cam;
    bool LeftClickWasClicked = false;
    public GameObject shere;
    public GameObject spawner;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    void FixedUpdate()
    {

        MoveCharacter(movementInput);

        if (Input.GetMouseButtonDown(0) && LeftClickWasClicked == false)
        {
            LeftClickWasClicked = true;
            Attack();
            //animator.SetTrigger("Attack");
        }
        if (Input.GetMouseButtonUp(0))
        {
            //animator.ResetTrigger("Attack");
        }

        if (Input.GetMouseButtonDown(1) && LeftClickWasClicked == true)
        {
            LeftClickWasClicked = false;
            Attack();
            //animator.SetTrigger("Attack");
        }
        if (Input.GetMouseButtonUp(1))
        {
            //animator.ResetTrigger("Attack");
        }

    }
    void Attack()
    {
        Instantiate(shere, spawner.transform.position, spawner.transform.rotation);
    }

    void MoveCharacter(Vector2 diraction)
    {
        
        rb.MovePosition(new Vector2(transform.position.x + 0.015f  + diraction.x*speed*Time.deltaTime, transform.position.y + diraction.y * speed * Time.deltaTime));
    }

}
