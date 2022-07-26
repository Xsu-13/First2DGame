using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phaseSlider : MonoBehaviour
{
    public GameObject cell;
    HackCell cellPlace;
    public GameObject core;
    HackCore hackCore;
    public Slider slider;
    public bool WasComplete;

    // Start is called before the first frame update
    void Start()
    {
        cellPlace = cell.GetComponent<HackCell>();
        hackCore = core.GetComponent<HackCore>();
        slider = GetComponent<Slider>();
        WasComplete = false;
        hackCore.ResetHack += Reset;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cellPlace.wasActivated)
        {
            slider.value = slider.maxValue - Vector2.Distance(cell.transform.position, hackCore.cells[hackCore.currentY, hackCore.currentX].transform.position);
        }
        else
        {
            slider.value = slider.maxValue;
        }
        

    }
    private void Reset()
    {
        slider.value = 0;
        cellPlace.wasActivated = false;
    }
}
