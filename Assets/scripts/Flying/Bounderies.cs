using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounderies : MonoBehaviour
{
    /*private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;*/
    [SerializeField] private float delta = 2;
    void Start()
    {
        /*screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;*/
    }

    
    void LateUpdate()
    {
        Vector2 camPos = Camera.main.transform.position;
        

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, camPos.x - delta, camPos.x + delta), Mathf.Clamp(transform.position.y, -1f, 1f), transform.position.z);
        /*Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
        transform.position = viewPos;*/
    }
}
