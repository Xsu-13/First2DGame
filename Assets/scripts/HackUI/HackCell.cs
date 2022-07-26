using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackCell : MonoBehaviour
{
    public HackCellType type;
    public int y;
    public int x;
    public Image image;
    public int index;

    public bool wasActivated = false;
    bool _see;
    public bool SeeColor
    {
        set
        {
            _see = value;
            UpdateColor();
        }
    }
    
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
        image = GetComponent<Image>();
        if (_see)
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
        else
        {

            image.color = new Color(0,0,0,0);
        }
    }
}
