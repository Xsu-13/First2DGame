using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Potion : MonoBehaviour
{
    public int potionScore;
    public string description;
    public int count;
    [SerializeField] TMP_Text countText;
    public string Name;
    
    void Start()
    {
        countText.text = count.ToString();
    }

    
    void Update()
    {
        countText.text = count.ToString();
    }
}
