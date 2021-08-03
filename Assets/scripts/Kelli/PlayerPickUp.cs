using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] TMP_Text notion;

    void Start()
    {
        
    }

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
            notion.text = ingredientSc.Name + " x 1";
            ingredientSc.count += 1;
            Destroy(collision.gameObject);

            Invoke("HideNotion", 2f);
        }
    }

    void HideNotion()
    {
        notion.text = "";
    }
}
