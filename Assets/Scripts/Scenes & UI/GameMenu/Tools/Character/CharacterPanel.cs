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

    [Header("Equipment Slots")]

    [SerializeField] EquipmentSlot head;         // the equipment slots of the panel

    [SerializeField] EquipmentSlot torso, legs,
        primaryWeapon, secondaryWeapon, shield;

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


        // todo take the secondary weapon and move back to inventory
        // clear secondary slot
        // take the primary weapon and move to secondary
        // clear the primary slot
        // take the equipment and equip it as primary weapon

        Debug.Log("Forced the equipment as primary weapon.");
    }
}
