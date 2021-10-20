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
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
        spawner = GameObject.FindGameObjectWithTag("spawner");
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

        if (player.transform.localScale.x > 0)
        {
            scale = 1;
            //animation.Play(animStates[0].name);
            anim.SetInteger("scaleAnim", 1);
        }
        else
        {
            scale = -1;
            anim.SetInteger("scaleAnim", -1);

            //animation.clip = animation.GetClip("ShereSpawnBack");

            //animation.Play(animStates[1].name);
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(scale * speed * Time.deltaTime, 0 , 0);
        //transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        float delta = Mathf.Abs(spawner.transform.position.x - transform.position.x);
        if(delta > 25)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public void Turn()
    {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        Debug.Log("Turn!");
    }
}
