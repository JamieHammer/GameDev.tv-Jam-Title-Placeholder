using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
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
}
