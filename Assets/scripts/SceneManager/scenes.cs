using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;
public class scenes : MonoBehaviour
{
    public Button FlyButton;
    public Button PlatformerButton;
    [SerializeField] GameObject optionPanel;
    [SerializeField] DialogueRunner dilR;
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

    public void OptionActive()
    {
        optionPanel.SetActive(true);
    }

    public void OptionLRu()
    {
        dilR.textLanguage = "ru";
        optionPanel.SetActive(false);
    }
    public void OptionLEn()
    {
        dilR.textLanguage = "en";
        optionPanel.SetActive(false);
    }
}
