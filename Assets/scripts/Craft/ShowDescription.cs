using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ShowDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text description;
    [SerializeField] GameObject descriptionPanel;
    Potion potion;
    //Text scoreText;
    //Text descriptionText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        score.text = potion.potionScore.ToString();
        description.text = potion.description;
        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }


    void Start()
    {
        potion = GetComponent<Potion>();
        //scoreText = score.GetComponent<Text>();
        //descriptionText = description.GetComponent<Text>();
    }


    void Update()
    {
        
    }
}
