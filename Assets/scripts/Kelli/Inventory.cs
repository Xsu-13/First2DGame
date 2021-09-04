using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] count;
    public int[] countInt;
    public GameObject[] craftInventory;
    public PotionType[] types;
    [SerializeField] Sprite selectSprite;
    [SerializeField] Sprite notSelectSprite;
    KeyDown key;
    int i;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            key = KeyDown.one;
            ClearSlots();
            SelectSlot(key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            key = KeyDown.two;
            ClearSlots();
            SelectSlot(key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            key = KeyDown.three;
            ClearSlots();
            SelectSlot(key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            key = KeyDown.four;
            ClearSlots();
            SelectSlot(key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            key = KeyDown.five;
            ClearSlots();
            SelectSlot(key);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            key = KeyDown.six;
            ClearSlots();
            SelectSlot(key);
        }

        
    }

    enum KeyDown
    {
        one,
        two,
        three,
        four,
        five,
        six
    }

    void ClearSlots()
    {
        for(int y=0; y <= 5 ; y++)
        {
            slots[i].GetComponent<Image>().sprite = notSelectSprite;
        }
    }

    void SelectSlot(KeyDown key)
    {
        
        switch(key)
        {
            case KeyDown.one:
                i = 0;
                break;
            case KeyDown.two:
                i = 1;
                break;
            case KeyDown.three:
                i = 2;
                break;
            case KeyDown.four:
                i = 3;
                break;
            case KeyDown.five:
                i = 4;
                break;
            case KeyDown.six:
                i = 5;
                break;
            default:
                Debug.Log("Что-то пошло не так");
                break;
        }

        slots[i].GetComponent<Image>().sprite = selectSprite;
    }
}
