using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public Characters currentCharacter;
    public Character character { get; set; }
    public int maxhealth { get; set; }
    public Player(Character character)
    {
        this.character = character;
        this.maxhealth = character.maxhealth;
    }
    public void Switch()
    {
        this.character.Switch(this);
        currentCharacter = character.currentCharacter;
    }
    public void Attack()
    {
        this.character.Attack();
    }
}

public abstract class Character
{
    public abstract Characters currentCharacter { get; set; }
    public abstract void Attack();
    public abstract void Switch(Player player);

    public abstract int maxhealth { get; set; }
}

class Kelli : Character
{
    public override Characters currentCharacter { get; set; } = Characters.KelliCharacter;
    public override void Attack()
    {
        Debug.Log("Kelli attack");
    }

    public override void Switch(Player player)
    {
        player.character = new Shon();
    }

    public override int maxhealth { get; set; }
}

class Shon : Character
{
    public override Characters currentCharacter { get; set; } = Characters.ShonCharacter;
    public override void Attack()
    {
        Debug.Log("Shon attack");
    }

    public override void Switch(Player player)
    {
        player.character = new Kelli();
    }

    public override int maxhealth { get; set; }
}

public enum Characters
{
    KelliCharacter,
    ShonCharacter
}
