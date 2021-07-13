using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] float agroRange = 3;
    [SerializeField] float speed = 1;
    [SerializeField] float rayDist = 2f;
    private bool movingRight = false;
    public Transform groungDetection;
    GameObject player;
    Rigidbody2D rb;
    bool isFacingLeft;
    public GameObject deathEffect;
    public GameObject potion;
    public GameObject sphere;
    public GameObject spawner;
    public GameObject castPoint;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(potion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groungDetection.position, Vector2.down, rayDist);

        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        /*if (canSee(agroRange))
            ChasePlayer();
        else StopChasingPlayer();*/

        //if (health >= 0)
            //Instantiate(sphere, spawner.transform.position, spawner.transform.rotation);
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(15, 15);
            isFacingLeft = false;
        }
        else 
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-15, 15);
            isFacingLeft = true;
        }
    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);
    }

    bool canSee(float dist)
    {
        bool val = false;
        float castDist = dist;
        if (isFacingLeft)
            castDist = -dist;
        Vector2 endPos = castPoint.transform.position + Vector3.right * dist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.transform.position, endPos, 1<<LayerMask.NameToLayer("Action"));
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
                val = true;
            else val = false;
        }
        return val;
    }
}
