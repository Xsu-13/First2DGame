using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class scenes : MonoBehaviour
{
    public Button FlyButton;
    public Button PlatformerButton;
    
    void Start()
    {
        PlatformerButton.onClick.AddListener(RunOn);
        FlyButton.onClick.AddListener(FlyOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FlyOn()
    {
        SceneManager.LoadScene("Demo2");
    }
    void RunOn()
    {
        SceneManager.LoadScene("Demo");
    }
}
