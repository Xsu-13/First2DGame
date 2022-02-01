using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class platformerSceneManager : MonoBehaviour
{
    [SerializeField] Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(LoadMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadMenu();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
