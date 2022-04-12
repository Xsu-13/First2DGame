using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackCell : MonoBehaviour
{
    public HackCellType type;
    public Image image;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateColor()
    {
        switch (type)
        {
            case HackCellType.Wall:
                image.color = Color.white;
                break;
            case HackCellType.Danger:
                image.color = Color.red;
                break;
            case HackCellType.Common:
                image.color = Color.gray;
                break;
            case HackCellType.Phase:
                image.color = Color.blue;
                break;
            case HackCellType.Begin:
                image.color = Color.green;
                break;
            default:
                image.color = Color.magenta;
                break;
        }
    }
}
