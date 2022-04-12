using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackCore : MonoBehaviour
{
    GameObject[,] cells = new GameObject[5, 5];
    [SerializeField] GameObject hackParent;
    List<int> activatedPhase = new List<int>();
    int phaseCount = 0;

    [SerializeField] Slider progresSlider;
    [SerializeField] List<GameObject> phaseProgress;
    
    //pubic for test
    public int startX;
    public int startY;

    public int currentX;
    public int currentY;

    int moveY;
    int moveX;
    // Start is called before the first frame update
    void Start()
    {
        List< Transform> horizontals = new List<Transform>();
        for(int i=0; i<hackParent.transform.childCount;i++)
        {
            Transform horizontal = hackParent.transform.GetChild(i);
            horizontals.Add(horizontal);
        }
        int k = 0;
        foreach(Transform hor in horizontals)
        {
            for( int i=0; i<5; i++)
            {
                GameObject child = hor.transform.GetChild(i).gameObject;
                HackCell cell = child.GetComponent<HackCell>();
                cell.index = k*5 + i;

                if(cell.type == HackCellType.Begin)
                {
                    startX = i;
                    startY = k;
                }
                if(cell.type == HackCellType.Phase)
                {
                    //activatedPhase.Add(cell.index);
                    phaseCount += 1;
                }
                cells[k, i] = child;
            }
            k += 1;
        }
        currentX = startX;
        currentY = startY;
        progresSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveY = currentY+ 1;
            moveX = currentX;
            Move(moveX, moveY);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveY = currentY- 1;
            moveX = currentX;
            Move(moveX, moveY);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveX =currentX+ 1;
            moveY = currentY;
            Move(moveX, moveY);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveX = currentX - 1;
            moveY = currentY;
            Move(moveX, moveY);
        }
    }

    void Move(int x, int y)
    {
        if(x<=4 & x>=0 & y>=0 & y<=4)
        {
            HackCell cell = cells[y, x].GetComponent<HackCell>();
            switch(cell.type)
            {
                case HackCellType.Wall:
                    {
                        Debug.Log("Wall");
                        break;
                    }
                case HackCellType.Common:
                    {
                        currentX = x;
                        currentY = y;
                        UpdateEachColor();
                        cell.image.color = Color.black;
                        break;
                    }
                case HackCellType.Danger:
                    {
                        currentX = startX;
                        currentY = startY;
                        activatedPhase.Clear();
                        progresSlider.value = 0;
                        Debug.Log("Danger");
                        break;
                    }
                case HackCellType.Phase:
                    {
                        currentX = x;
                        currentY = y;
                        UpdateEachColor();
                        cell.image.color = Color.black;

                        if(!activatedPhase.Contains(cell.index))
                        {
                            activatedPhase.Add(cell.index);
                        }
                        LeanTween.value(progresSlider.gameObject, progresSlider.value, (float)activatedPhase.Count / phaseCount, 1f).setEaseInOutQuart()
                            .setOnUpdate((value) => progresSlider.value = value);
                        //progresSlider.value = (float)activatedPhase.Count / phaseCount;
                        Debug.Log(activatedPhase.Count);
                        if (activatedPhase.Count == phaseCount )
                        {
                            progresSlider.value = 1;
                            Debug.Log("Winnnn");
                        }
                        break;
                    }
                default:
                    Debug.Log("----");
                    break;
            }
            
            /*
            if(cell.type != HackCellType.Wall)
            {
                currentX = x;
                currentY = y;
            }
            */
            
        }
        else
        {
            Debug.Log("No way");
        }
        
    }
    void UpdateEachColor()
    {
        foreach(GameObject cell in cells)
        {
            cell.GetComponent<HackCell>().UpdateColor();
        }
    }

    void CheckDuration()
    {

    }
}
public enum HackCellType
{
    Wall,
    Danger,
    Common,
    Begin,
    Phase
}
