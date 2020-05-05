using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of all equipment items.
/// </summary>

    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [Header("Equipment Type")]

    [SerializeField] EquipmentType equipmentType;           // the type of equipment this item has

    [Header("Modifiers")]

    [SerializeField] int armourModifier;                    // modifies the defence ability

    [SerializeField] int damageModifier;                    // modifies the attack ability

    /// <summary>
    /// Returns the equipment type of this inventory item.
    /// </summary>

    public EquipmentType GetEquipmentType()
    {
        return equipmentType;
    }

    /// <summary>
    /// Returns the armour modifier for this piece of equipment.
    /// </summary>

    public int GetArmourModifier()
    {
        return armourModifier;
    }

    /// <summary>
    /// Returns the damage modifier for this piece of equipment.
    /// </summary>

    public int GetDamageModifier()
    {
        return damageModifier;
    }

    /// <summary>
    /// Overrides the Item use function, to equip item.
    /// </summary>

    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);

        RemoveFromInventory();

        Debug.Log("Equiping " + name);
    }
}
