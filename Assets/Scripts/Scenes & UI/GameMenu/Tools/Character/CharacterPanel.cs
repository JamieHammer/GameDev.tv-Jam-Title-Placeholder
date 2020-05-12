using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class CharacterPanel : MonoBehaviour
{
    #region Singleton

    public static CharacterPanel instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Equipment

    [Header("Equipment Slots")]

    [SerializeField] EquipmentSlot head;            // the equipment slots of the panel

    [SerializeField] EquipmentSlot torso, legs,
        primaryWeapon, secondaryWeapon, shield;

    public EquipmentSlot currentlySelectedSlot;     // reference to the currently selected slot

    /// <summary>
    /// Responsible for equiping, equipable items.
    /// </summary>
    /// <param name="equipment">the item to equip</param>

    public void EquipItem(Equipment equipment)
    {
        switch (equipment.GetEquipmentType())
        {
            case EquipmentType.Weapon:
                if(EquipPrimary(equipment)) { break; }
                if(EquipSecondary(equipment)) { break; }

                ForceFirst(equipment);
                break;

            case EquipmentType.Shield:
                shield.EquipItem(equipment);
                break;

            case EquipmentType.Legs:
                legs.EquipItem(equipment);
                break;

            case EquipmentType.Torso:
                torso.EquipItem(equipment);
                break;

            case EquipmentType.Head:
                head.EquipItem(equipment);
                break;
        }
    }

    /// <summary>
    /// Responsible for trying to equip a weapon in the primary slot.
    /// </summary>

    bool EquipPrimary(Equipment equipment)
    {
        if (primaryWeapon.GetEquipment() == null)
        {
            primaryWeapon.EquipItem(equipment);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Responsible for trying to equip a weapon in the secondary slot.
    /// </summary>

    bool EquipSecondary(Equipment equipment)
    {
        if (secondaryWeapon.GetEquipment() == null)
        {
            secondaryWeapon.EquipItem(equipment);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Responsible for force-equiping a weapon in the primary slot, moving the second.
    /// </summary>

    void ForceFirst(Equipment equipment)
    {
        // reference the secondary weapon slot as tmp and clear the slot

        Equipment tmpEquipment = secondaryWeapon.GetEquipment();

        secondaryWeapon.DequipItem();

        // move primary weapon to secondary slot and clear slot

        EquipSecondary(primaryWeapon.GetEquipment());

        primaryWeapon.DequipItem();

        // equip the item as primary weapon

        EquipPrimary(equipment);

        // finally, add the tmp back to the inventory

        InventoryManager.instance.AddItem(tmpEquipment);

        Debug.Log("Forced the equipment as primary weapon.");
    }

    #endregion
}
