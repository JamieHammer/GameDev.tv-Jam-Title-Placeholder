using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    public InventoryType type;                  // reference to the type of this inventory bag

    [SerializeField] Transform chestParent;     // reference to the chest slot parent

    private void Start()
    {
        AddSlots();
    }

    void AddSlots()
    {
        switch(type)
        {
            case InventoryType.None:
                for (int i = 0; i < chestParent.childCount; i++)
                {
                    InventorySlot slot = chestParent.GetChild(i).GetComponent<InventorySlot>();
                    slots.Add(slot);
                }
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

    /// <summary>
    /// Returns the list of slots in this bag.
    /// </summary>

    public List<Item> GetItems()
    {
        List<Item> items = new List<Item>();

        foreach (var slot in slots)
        {
            if (!slot.IsEmpty)
            {
                foreach (var item in slot.GetItems)
                {
                    items.Add(item);
                }
            }
        }

        return items;
    }

    /// <summary>
    /// Clears the slots of this bag.
    /// </summary>

    public void Clear()
    {
        foreach (var slot in slots)
        {
            slot.Clear();
        }
    }
}
