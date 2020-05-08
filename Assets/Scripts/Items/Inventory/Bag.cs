using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    public InventoryType type;

    private void Start()
    {
        AddSlots();
    }

    void AddSlots()
    {
        switch(type)
        {
            case InventoryType.None:

                break;

            case InventoryType.Equipment:
                slots = InventoryUI.instance.equipmentSlots;
                break;

            case InventoryType.Usable:
                slots = InventoryUI.instance.usableItemSlots;
                break;

            case InventoryType.Quest:
                slots = InventoryUI.instance.questItemSlots;
                break;
        }
    }

    /// <summary>
    /// Responsible for adding the item to the bag.
    /// </summary>
    /// <param name="item">the item to add if available space</param>
    /// <returns>whether or not the item was succesfully added to a slot in the bag</returns>

    public bool AddItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);

                return true;
            }
        }

        return false;
    }
}
