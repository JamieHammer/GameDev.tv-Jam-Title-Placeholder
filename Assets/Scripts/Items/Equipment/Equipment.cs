using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of all equipment items.
/// </summary>

    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [Space]

    [Header("Equipment")]

    [SerializeField] EquipmentType equipmentType;           // the type of equipment this item has

    /// <summary>
    /// Returns the equipment type of this inventory item.
    /// </summary>

    public EquipmentType GetEquipmentType()
    {
        return equipmentType;
    }

    /// <summary>
    /// Overrides the Item use function, to equip item.
    /// </summary>

    public override void Use()
    {
        base.Use();

        CharacterPanel.instance.EquipItem(this);
    }
}
