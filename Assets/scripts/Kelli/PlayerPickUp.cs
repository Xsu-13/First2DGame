using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ingredient"))
        {
           Debug.Log("ingredient");
           ingredientObj ingredientObjSc = collision.GetComponent<ingredientObj>();

           Ingredient ingredientSc = ingredientObjSc.craftObj.GetComponent<Ingredient>();
           ingredientSc.count += 1;
            Destroy(collision.gameObject);
        }
    }
}
