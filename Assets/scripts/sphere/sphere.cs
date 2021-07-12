using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{

    public float speed;
    GameObject spawner;
    GameObject player;
    float scale;
    int damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("spawner");
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.localScale.x > 0)
            scale = 1;
        else scale = -1;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(scale * speed * Time.deltaTime, 0 , 0);
        float delta = Mathf.Abs(spawner.transform.position.x - transform.position.x);
        if(delta > 25)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
