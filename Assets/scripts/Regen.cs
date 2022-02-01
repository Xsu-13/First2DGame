using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Regen : MonoBehaviour
{
    [SerializeField] float speed = 0.001f;
    public Image regenImg;

    void Start()
    {
        regenImg = GetComponent<Image>();
    }


    void Update()
    {
        if(regenImg.fillAmount < 1)
        {
            regenImg.fillAmount += speed;
        }
    }
}
