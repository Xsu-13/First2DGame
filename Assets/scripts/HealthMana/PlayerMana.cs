using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Custom settings")]
    [Header("Максимальное количество маны")]
    [SerializeField] int maxMana = 100;
    [Header("-----Inside set-----")]
    public int currentMana;
    public ManaBar manaBar;
    // Start is called before the first frame update
    void Start()
    {
        //currentMana = maxMana;
        //manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int cost)
    {
        currentMana -= cost;
        manaBar.SetMana(currentMana);
    }

    public void SetMana(int mana)
    {
        currentMana = mana;
        manaBar.SetMana(currentMana);
    }
}
