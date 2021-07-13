using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    Vector2 movementInput;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 3f;
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
        Vector2 pos = transform.position;
        Vector2 camPos = Camera.main.transform.position;

        rb.MovePosition(new Vector2(pos.x + 0.015f + diraction.x * speed * Time.deltaTime, pos.y + diraction.y * speed * Time.deltaTime));
    }

}
