using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pickUp : MonoBehaviour
{
    public Inventory inventory;
    public GameObject craftObj;
    public GameObject itemButton;
    public TMP_Text countText;
    public int count = 0;
    public PotionType type;
    Potion potionSc;
    public ScObjPotion potSc;

    
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("inventory").GetComponent<Inventory>();
        potionSc = craftObj.GetComponent<Potion>();
    }

    private void Update()
    {
        /*
        if(potionSc.count != null)
            count = potionSc.count.Value;
        */

        if (potSc.count != null)
            count = potSc.count;
    }

}
