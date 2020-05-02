using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the inventory UI.
/// </summary>

public class InventoryUI : MonoBehaviour
{
    public Transform equipmentParent;           // the parent transform of the equipment slots
    public Transform usablesParent;             // the parent transform of the usable item slots
    public Transform questParent;               // the parent transform of the quest item slots

    InventoryManager inventory;                 // reference to the inventory manager

    InventorySlot[] equipmentSlots;             // an array of equipment slots
    InventorySlot[] usablesSlots;               // an array of usable item slots
    InventorySlot[] questSlots;                 // an array of quest item slots

    void Start()
    {
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateUI;

        equipmentSlots = equipmentParent.GetComponentsInChildren<InventorySlot>();
        usablesSlots = usablesParent.GetComponentsInChildren<InventorySlot>();
        questSlots = questParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    /// <summary>
    /// Iterates through all inventory items and updates the UI.
    /// </summary>

    void UpdateUI()
    {
        // EQUIPMENT

        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (i < inventory.equipment.Count)
            {
                equipmentSlots[i].AddItem(inventory.equipment[i]);
            }
            else
            {
                equipmentSlots[i].ClearSlot();
            }
        }

        // USABLE ITEMS

        for (int i = 0; i < usablesSlots.Length; i++)
        {
            if (i < inventory.usableItems.Count)
            {
                usablesSlots[i].AddItem(inventory.usableItems[i]);
            }
            else
            {
                usablesSlots[i].ClearSlot();
            }
        }

        // QUEST ITEMS

        for (int i = 0; i < questSlots.Length; i++)
        {
            if (i < inventory.questItems.Count)
            {
                questSlots[i].AddItem(inventory.questItems[i]);
            }
            else
            {
                questSlots[i].ClearSlot();
            }
        }
    }
}
