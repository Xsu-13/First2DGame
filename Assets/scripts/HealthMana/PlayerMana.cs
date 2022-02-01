using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Custom settings")]
    [Header("Максимальное количество маны")]
    [SerializeField] float maxMana = 100;
    [Header("Скорость восстановления маны")]
    [SerializeField] float speed = 0.05f;
    [Header("Процент маны, ниже которого может восстанавливаться мана(0.3)")]
    [SerializeField] float limit = 0.3f;
    [Header("-----Inside set-----")]
    public float currentMana;
    public ManaBar manaBar;

    public GameObject mate;
     PlayerMana mateMana;
    // Start is called before the first frame update
    void Start()
    {
        //currentMana = maxMana;
        //manaBar.SetMaxMana(maxMana);
        mateMana = mate.GetComponent<PlayerMana>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMana < maxMana*limit)
        {
            currentMana += speed;
            manaBar.SetMana(currentMana);
        }
        if (mateMana.currentMana < mateMana.maxMana * limit)
        {
            mateMana.currentMana += speed;
            mateMana.manaBar.SetMana(mateMana.currentMana);
        }
    }
    public void TakeDamage(float cost)
    {
        currentMana -= cost;
        manaBar.SetMana(currentMana);
    }

    public void SetMana(float mana)
    {
        currentMana = mana;
        manaBar.SetMana(currentMana);
    }
    public void AddMana(float mana)
    {
        currentMana += mana;
        manaBar.SetMana(currentMana);
    }
}
