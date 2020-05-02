using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the inventory UI.
/// </summary>

public class EquipmentUI : MonoBehaviour
{
    public Transform equipmentParent;           // the parent transform of the equipment slots

    EquipmentManager equipment;                 // reference to the equipment manager
    InventoryManager inventory;                 // reference to the inventory manager

    public EquipmentSlot[] equipmentSlots;      // an array of equipment slots

    // Start is called before the first frame update
    void Start()
    {
        equipment = EquipmentManager.instance;
        inventory = InventoryManager.instance;

        equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
        inventory.onItemChangedCallback += UpdateUI;

        UpdateUI();
    }

    /// <summary>
    /// Iterates through all equipment inventory items and updates the UI.
    /// </summary>

    void UpdateUI()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipment.GetEquipedItem(i) != null)
            {
                equipmentSlots[i].AddItem(equipment.GetEquipedItem(i));
            }
            else
            {
                equipmentSlots[i].ClearSlot();
            }
        }
    }
}
