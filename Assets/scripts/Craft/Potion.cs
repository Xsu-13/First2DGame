using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    
    public int potionScore;
    public string description;
    int countVal;
    public ObservableVariable<int> count;
    private ObserverbleLogger logger;
    
    [SerializeField] TMP_Text countText;
    public string Name;
    public GameObject inventorySprite;
    public PotionType type;

    public ScObjPotion scPot;


    private void Awake()
    {
        count = new ObservableVariable<int>(countVal);
        logger = new ObserverbleLogger(count, countText);
    }

    void Start()
    {
        

        if (scPot == null)
            return;
        else
        {
            potionScore = scPot.potionScore;
            description = scPot.description;
            count.Value = scPot.count;
            type = scPot.type;
            Name = scPot.Name;
        }
        
    }


    private void OnEnable()
    {
        if (scPot == null)
            return;
        else
        {
           count.Value = scPot.count;
        }
    }
    private void OnDisable()
    {
        if (scPot == null)
            return;
        else
        {
           scPot.count = count.Value;
        }
    }
    
}

public enum PotionType
{
    type0,
    type1,
    type2,
    type3,
    type4,
    type5,
    type6,
    type7,
    type8,
    type9,
    type10,
    type11
}
