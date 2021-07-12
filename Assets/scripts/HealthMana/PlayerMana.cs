using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    private int maxMana = 100;
    public int currentMana;
    public ManaBar manaBar;
    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TakeDamage(int cost)
    {
        currentMana -= cost;
        manaBar.SetMana(currentMana);
    }
}
