using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modifiers to the base stats, for an item.
/// </summary>

[System.Serializable]
public class Modifiers
{

    [Header("Abilities")]

    public int strength;

    public int stamina;

    public int intelligence;

    public int dexterity;

    public int charisma;

    public int luck;

    public int attack;

    public int defence;

    [Header("HP & EXP")]

    public int health;

    public int experience;

}
