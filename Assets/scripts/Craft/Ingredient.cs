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
    // должен оставаться public
    public Type type;

    private void Start()
    {
        countText.text = count.ToString();
        startParent = transform.parent;
    }

    private void Update()
    {
        countText.text = count.ToString();
    }

    public enum Type
    {
        flyAway,
        invisiblePotion
    }
}
