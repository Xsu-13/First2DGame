using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Ingredient : MonoBehaviour
{
    public int score;
    public int count;
    public TMP_Text countText;
    public Transform startParent;
    public string Name;
    public bool inCreateSlot = false;
    // должен оставаться public
    public TypeIng type;

    [SerializeField] ScObjIngredient scIng;

    private void Start()
    {
        countText.text = count.ToString();
        startParent = transform.parent;

        
        if (scIng == null)
            return;
        else
        {
            score = scIng.score;
            count = scIng.count;
            type = scIng.type;
            Name = scIng.Name;
        }
        
    }

    private void Update()
    {
        countText.text = count.ToString();
    }

    private void OnEnable()
    {
        if (scIng == null)
        {
            return;
        }
        else
        {
            count = scIng.count;
        }
        
    }

    private void OnDisable()
    {
        if (scIng == null)
            return;
        else
            scIng.count = count;
    }

}

public enum TypeIng
{
    flyAway,
    invisiblePotion,
    tropinchik,
    sinesvet,
    mehasvet,
    grisovic,
    ognelist,
    pepelisa,
    oscolok,
    plod,
    vseyadka,
    yantarTears,
    zubPremudrosti,
    mohnatoeSerdses,
    dushevosk,
    poganka
}
