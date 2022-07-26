using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] GameObject chestBreaker;
    [SerializeField] GameObject[] treasure;
    bool isOpened = false;
    bool playerIn = false;
    PlayerPickUp pick;
    // Start is called before the first frame update
    void Start()
    {
        chestBreaker.GetComponent<HackCore>().Win += OpenChestAfterWin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            pick = collision.transform.GetComponent<PlayerPickUp>();
            if (Input.GetKeyDown(KeyCode.K))
            {
                OpenChestBreaker();
            }
        }

    }
    public void OpenChestBreaker()
    {

        isOpened = !isOpened;
        
        chestBreaker.SetActive(isOpened);
        
    }
    private void OpenChestAfterWin()
    {
        Debug.Log("WinOpen");
        //yield return new WaitForSeconds(2f);

        Invoke("Close", 1f);
        foreach(GameObject thing in treasure)
        {
            if(thing.CompareTag("potion"))
            {
                pick.AddPotion(thing);
            }
            if(thing.CompareTag("ingredient"))
            {
                pick.AddIngredient(thing);
            }
            //yield return new WaitForSeconds(0.2f);
        }
        //Invoke(CloseChest => { chestBreaker.SetActive(false); });
    }
    void Close()
    {
        chestBreaker.SetActive(false);
    }

}
