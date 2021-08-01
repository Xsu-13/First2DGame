using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingOfIngredient : MonoBehaviour
{
    Vector3 destinationPoint1;
    Vector3 destinationPoint2;
    float speed = 0.5f;
    bool inPoint1;
    // Start is called before the first frame update
    void Start()
    {
        destinationPoint1 = transform.position;
        destinationPoint2 = transform.position;
        destinationPoint2.y += 1f;

    }

    // Update is called once per frame
    void Update()
    {

        
        if(inPoint1 == false)
        {
            Move(destinationPoint1);
            if (Mathf.Abs(transform.position.y - destinationPoint1.y) <= 0.2f)
                inPoint1 = true;
        }
        else if (inPoint1 == true)
        {
            Move(destinationPoint2);
            if (Mathf.Abs(transform.position.y - destinationPoint2.y) <= 0.2f)
                inPoint1 = false;
        }
    }

    void Move(Vector3 destination)
    {
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
    }
}
