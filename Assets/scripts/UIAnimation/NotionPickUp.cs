using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotionPickUp : MonoBehaviour
{
    //bool scaled = false;
    TMP_Text textValue;

    // Start is called before the first frame update
    void Start()
    {
        textValue = GetComponent<TMP_Text>();
         
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeOutExpo);
        Invoke("NotionEnd", 2.5f);
        //LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 0f, 0.2f).setOnComplete(NotionComplete);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.G))
        {
            Scale();
        }
        */
    }

    /*
    void Scale()
    {
        if(!scaled)
        {
            LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeOutExpo);
        }
        else
        {
            LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 0f, 0.2f).setOnComplete(NotionComplete);
        }
        scaled = !scaled;
    }
    */
    void NotionEnd()
    {
        //Destroy(gameObject); .setOnComplete(NotionComplete);
       // LeanTween.alphaVertex(gameObject, 0f, 2f).setOnComplete(NotionComplete);
        LeanTween.value(gameObject, updateValue, textValue.alpha, 0f, 0.2f).setOnComplete(NotionComplete);
    }

    void NotionComplete()
    {
        Destroy(gameObject);
    }
    void updateValue(float val)
    {
        textValue.alpha = val;
    }
}
