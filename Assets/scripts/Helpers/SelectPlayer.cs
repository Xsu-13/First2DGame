using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player = new Player(new Kelli());
    }


    void Update()
    {
        
    }
}
