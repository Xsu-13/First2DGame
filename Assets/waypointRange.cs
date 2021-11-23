using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointRange : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float range;

    private void Start()
    {
        pos1 = transform.position;
        pos2 = transform.position + new Vector3(range,0,0);
    }
    public float RandomPos()
    {
        return Random.Range(pos1.x,pos2.x);
    }

}
