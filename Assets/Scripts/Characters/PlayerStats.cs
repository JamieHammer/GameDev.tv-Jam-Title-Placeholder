using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To manage everything related to the player.
/// </summary>

public class PlayerStats : CharacterStats
{
    Transform playerTransform;          // reference to the player transform

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            defence.AddModifier(newItem.GetArmourModifier());
            attack.AddModifier(newItem.GetDamageModifier());
        }

        if (oldItem != null)
        {
            defence.RemoveModifier(oldItem.GetArmourModifier());
            attack.RemoveModifier(oldItem.GetDamageModifier());
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            // INFO

            characterName = data.name;

            // LEVEL

            // todo update level system

            //level = player.levelSystemAnimation.GetLevelNumber();
            //experience = player.levelSystemAnimation.GetExperience();

            // HEALTH

            maxHealth = data.maxHealth;
            LoadCurrentHealth(data.currentHealth);

            // ABILITIES

            strength = data.strength;
            stamina = data.stamina;
            intelligence = data.intelligence;
            dexterity = data.dexterity;
            charisma = data.charisma;
            luck = data.luck;
            attack = data.attack;
            defence = data.defence;

            // POSITION

            float x = data.position[0];
            float y = data.position[1];
            float z = data.position[2];

            playerTransform.position.Set(x, y, z);
        }
    }
}
