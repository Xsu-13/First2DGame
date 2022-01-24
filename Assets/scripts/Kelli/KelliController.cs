using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KelliController : MonoBehaviour
{
    [Header("-----Inside set-----")]
    public bool craftIsActive;
    [SerializeField] GameObject craft;
    [SerializeField] GameObject platformInterface;
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetButtonDown("Craft"))
        {
            if (!craftIsActive)
            {
                craft.SetActive(true);
                craftIsActive = true;
                platformInterface.SetActive(false);
            }
            else
            {
                craft.SetActive(false);
                craftIsActive = false;
                platformInterface.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }
}
