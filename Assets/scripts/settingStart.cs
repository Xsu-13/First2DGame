using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingStart : MonoBehaviour
{
    [SerializeField] GameObject Shon;
    [SerializeField] GameObject Kelli;
    // Start is called before the first frame update
    void Start()
    {
        Shon.GetComponent<PlayerMovement>().mat.SetFloat("_FillPhase", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
