using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySphere : MonoBehaviour
{
    public float speed;
    GameObject spawner;
    GameObject enemy;
    float scale;
    int damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("spawner");
        enemy = GameObject.FindGameObjectWithTag("enemy");
        if (enemy.transform.localScale.x > 0)
            scale = 1;
        else scale = -1;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(scale * speed * Time.deltaTime, 0, 0);
        float delta = Mathf.Abs(spawner.transform.position.x - transform.position.x);
        if (delta > 25)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
