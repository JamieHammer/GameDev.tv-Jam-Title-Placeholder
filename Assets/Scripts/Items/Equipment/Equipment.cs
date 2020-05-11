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

    [Header("Equipment Type")]

    [SerializeField] EquipmentType equipmentType;           // the type of equipment this item has

    #region Modifiers

    [Header("Modifiers")]

    [SerializeField] int[] Modifiers;                       // an array of ability modifiers, 0 = strength, 1 = stamina, 2 = intelligence,
                                                            // 3 = dexterity, 4 = charisma, 5 = luck, 6 = attack, 7 = defence

    /// <summary>
    /// Returns the strength modifier for this piece of equipment.
    /// </summary>

    public int GetStrengthModifier()
    {
        return Modifiers[0];
    }

    /// <summary>
    /// Returns the stamina modifier for this piece of equipment.
    /// </summary>

    public int GetStaminaModifier()
    {
        return Modifiers[1];
    }

    /// <summary>
    /// Returns the intelligence modifier for this piece of equipment.
    /// </summary>

    public int GetIntelligenceModifier()
    {
        return Modifiers[2];
    }

    /// <summary>
    /// Returns the dexterity modifier for this piece of equipment.
    /// </summary>

    public int GetDexterityModifier()
    {
        return Modifiers[3];
    }

    /// <summary>
    /// Returns the charisma modifier for this piece of equipment.
    /// </summary>

    public int GetCharismaModifier()
    {
        return Modifiers[4];
    }

    /// <summary>
    /// Returns the luck modifier for this piece of equipment.
    /// </summary>

    public int GetLuckModifier()
    {
        return Modifiers[5];
    }

    /// <summary>
    /// Returns the attack modifier for this piece of equipment.
    /// </summary>

    public int GetAttackModifier()
    {
        return Modifiers[6];
    }

    /// <summary>
    /// Returns the defence modifier for this piece of equipment.
    /// </summary>

    public int GetDefenceModifier()
    {
        return Modifiers[7];
    }

#endregion

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
