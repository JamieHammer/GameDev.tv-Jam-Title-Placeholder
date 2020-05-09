using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles equipment.
/// </summary>

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

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

    public Equipment[] currentEquipment;           // an array of currently equipped equipment

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    InventoryManager inventory;             // reference to the inventory manager

    public int equipmentSlots = 4;          // the currently supported number of equipped items

    private void Start()
    {
        inventory = InventoryManager.instance;

        currentEquipment = new Equipment[equipmentSlots];
    }

    /// <summary>
    /// Responsible for equipping an item.
    /// </summary>

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.GetEquipmentType();

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        // todo if the equipment isn't removed, use RemoveFromInventory();
    }

    /// <summary>
    /// Responsible for unequipping an item.
    /// </summary>
    /// <param name="slotIndex">the item slot index to unequip</param>

    public void Unequip(int slotIndex)
    {
        Debug.Log(slotIndex);

        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            currentEquipment[slotIndex] = null;
        }
    }

    /// <summary>
    /// Returns the equipment of the requested index.
    /// </summary>
    /// <param name="index">the index of the requesten position</param>

    public Equipment GetEquipedItem(int index)
    {
        return currentEquipment[index];
    }
}
