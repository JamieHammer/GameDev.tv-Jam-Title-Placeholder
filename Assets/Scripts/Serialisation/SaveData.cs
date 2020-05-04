using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region SaveFile

/// <summary>
/// A save file to hold game data.
/// </summary>

[Serializable]
public class SaveData
{
    public PlayerData playerData { get; set; }

    /// <summary>
    /// The save data file construct.
    /// </summary>

    public SaveData()
    {

    }
}

#endregion

#region Data Holders

/// <summary>
/// Helps with saving player data.
/// </summary>

[Serializable]
public class PlayerData
{
    [Header("Info")]

    public string name;                 // name of the player
    // date and time of save

    [Header("Level")]

    public int level;                   // level of the player
    public int experience;              // the amount of experience gained already

    [Header("Health")]

    public int maxHealth;               // max health of the player
    public int currentHealth;           // remaining health of the player

    [Header("Abilities")]

    public Stat strength;               // the base strength
    public Stat stamina;                // the base stamina
    public Stat intelligence;           // the base intelligence
    public Stat dexterity;              // the base dexterity
    public Stat charisma;               // the base charisma
    public Stat luck;                   // the base luck
    public Stat attack;                 // the base offensive ability for dealing more damage
    public Stat defence;                // the base defensive ability for avoiding taking damage

    [Header("Position")]

    public float[] position;            // an array of floats corresponding to a player position

    public PlayerData(Player player)
    {
        // INFO

        name = player.characterName;

        // LEVEL

        level = player.levelSystemAnimation.GetLevelNumber();
        experience = player.levelSystemAnimation.GetExperience();

        // HEALTH

        maxHealth = player.maxHealth;
        currentHealth = player.currentHealth;

        // ABILITIES

        strength = player.strength;
        stamina = player.stamina;
        intelligence = player.intelligence;
        dexterity = player.dexterity;
        charisma = player.charisma;
        luck = player.luck;
        attack = player.attack;
        defence = player.defence;

        // POSITION

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

    #endregion
}
