using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] count;
    public int[] countInt;
    public GameObject[] craftInventory;
    public PotionType[] types;
    public GameObject[] potButtons;
    [SerializeField] Sprite selectSprite;
    [SerializeField] Sprite notSelectSprite;
    KeyDown key;
    int i;
    [SerializeField]GameObject KelliPlayer;
    [SerializeField]GameObject ShonPlayer;
    PlayerMovement kelli;
    PlayerMovement shon;

    private void Start()
    {
        kelli = KelliPlayer.GetComponent<PlayerMovement>();
        shon = ShonPlayer.GetComponent<PlayerMovement>();

        //Screen.fullScreenMode = FullScreenMode.Windowed;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)
            || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                key = KeyDown.one;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                key = KeyDown.two;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                key = KeyDown.three;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                key = KeyDown.four;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                key = KeyDown.five;
            }
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
        five
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
            default:
                Debug.Log("Что-то пошло не так c инвентврем пояса");
                break;
        }

        slots[i].GetComponent<Image>().sprite = selectSprite;

        if (slots[i].transform.childCount >1)
        {
            forSaving currentPotion = slots[i].transform.GetChild(1).gameObject.GetComponent<forSaving>();
            if(currentPotion.PotionLink.count>0)
            {
                currentPotion.PotionLink.count -= 1;
                int c = currentPotion.PotionLink.count;
                count[i].GetComponent<TMP_Text>().text = c.ToString();
                if (KelliPlayer.activeInHierarchy)
                {
                    kelli.PotionActivate(currentPotion.PotionLink.type);
                }
                else
                {
                    shon.PotionActivate(currentPotion.PotionLink.type);
                }
                if (currentPotion.PotionLink.count == 0)
                {
                    //ДОДЕЛАТЬ!!!
                    Destroy(slots[i].transform.GetChild(1).gameObject);
                    craftInventory[i].GetComponent<PotionSlot>().Return();
                }
            }
            

        }
    }

    public List<int> GetPotInSlots()
    {
        List<int> potInSlot = new List<int>();
        int i = 0;
        foreach(GameObject slot in slots)
        {            
            if(slot.transform.childCount > 1)
            {
                potInSlot.Add(slot.transform.GetChild(1).gameObject.GetComponent<forSaving>().indexForSave);
            }
            else
            {
                potInSlot.Add(0);
            }
            i++;
        }
        return potInSlot;
    }
}
