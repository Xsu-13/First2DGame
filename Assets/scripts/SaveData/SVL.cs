using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public delegate void OnSaveDel();
public delegate void OnLoadDel();

public class SVL : MonoBehaviour
{
    //Data
    [SerializeField] List<ScObjPotion> potions = new List<ScObjPotion>();
    [SerializeField] List<ScObjIngredient> ingredients = new List<ScObjIngredient>();
    [SerializeField] GameObject KelliCharacter, ShonCharacter;
    PlayerHealth KelliHealthComponent, ShonHealthComponent;
    PlayerMana KelliManaComponent, ShonManaComponent;
    //int KelliHealth, KelliMana;
    //int ShonHealth, ShonMana;
    Inventory inventory;
    //test
    public List<int> potInSlots;
    //Save Config
    string SaveFileName = "data.json";
    string SavePath;

    //Singletone with events
    public static SVL instance = null;

    public event OnSaveDel OnSave;
    public event OnLoadDel OnLoad;



    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        inventory = FindObjectOfType<Inventory>();

        SavePath = Path.Combine(Application.dataPath, SaveFileName);

        KelliHealthComponent = KelliCharacter.GetComponent<PlayerHealth>();
        ShonHealthComponent = ShonCharacter.GetComponent<PlayerHealth>();

        KelliManaComponent = KelliCharacter.GetComponent<PlayerMana>();
        ShonManaComponent = ShonCharacter.GetComponent<PlayerMana>();

        Load();
    }

    public void Save()
    {
        //test

        potInSlots = inventory.GetPotInSlots();
        foreach(int pot in potInSlots)
        {
            //Debug.Log(pot);
        }
        //OnSave?.Invoke();
        List<int> countPot = new List<int>();
        List<int> countIng = new List<int>();

        foreach (ScObjPotion p in potions)
        {
            countPot.Add(p.count);
        }
        foreach(ScObjIngredient i in ingredients)
        {
            countIng.Add(i.count);
        }

        GameCoreStruct gameCore = new GameCoreStruct
        {

            potionCount = countPot,
            ingCount = countIng,
            potInSlot = potInSlots,
            KelliHealth = KelliHealthComponent.currentHelth,
            ShonHealth = ShonHealthComponent.currentHelth,
            KelliMana = KelliManaComponent.currentMana,
            ShonMana = ShonManaComponent.currentMana,
        };

        string json = JsonUtility.ToJson(gameCore, true);
        try
        {
            File.WriteAllText(SavePath, json);
        }
        catch(Exception e)
        {
            Debug.Log("Error of saving data");
        }


        
    }

    public void Load()
    {
        if(!File.Exists(SavePath))
        {
            Debug.Log(" Data File does not exist");
            return;
        }
        
        string json = File.ReadAllText(SavePath);
        GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

        int i = 0;
        foreach(ScObjPotion p in potions)
        {
            p.count = gameCoreFromJson.potionCount[i];
            i++;
        }
        i = 0;
        foreach (ScObjIngredient ing in ingredients)
        {
            ing.count = gameCoreFromJson.ingCount[i];
            i++;
        }


        //–¿¡Œ“€
        KelliHealthComponent.SetHealth(gameCoreFromJson.KelliHealth);
        ShonHealthComponent.SetHealth(gameCoreFromJson.ShonHealth);
        KelliManaComponent.SetMana(gameCoreFromJson.KelliMana);
        ShonManaComponent.SetMana(gameCoreFromJson.ShonMana);
        

        //test
        GameObject[] slots = inventory.slots;
        i = 0;
        //test
        

        for (int j = 0; j <= potInSlots.Count-1; j++)
        {
            if (gameCoreFromJson.potInSlot[j] > 0)
            {
                GameObject potBut = Instantiate(inventory.potButtons[gameCoreFromJson.potInSlot[j]-1]);
                potBut.transform.SetParent(slots[j].transform);
                potBut.transform.position = slots[j].transform.position;
            }
        }

    }

    void Update()
    {
        
    }
}
[System.Serializable]
public struct GameCoreStruct
{
    public List<int> potionCount;
    public List<int> ingCount;
    public List<int> potInSlot;
    public int KelliHealth;
    public int ShonHealth;
    public int KelliMana;
    public int ShonMana;
}
