using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class OrderIndex : MonoBehaviour
{
    public int index;
    private void Awake()
    {
        transform.SetSiblingIndex(index);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
