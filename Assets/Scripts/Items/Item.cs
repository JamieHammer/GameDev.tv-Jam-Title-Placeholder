﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of all inventory items.
/// </summary>

public abstract class Item : ScriptableObject, Moveable
{
    [Header("Info")]

    [SerializeField] string itemName;                   // the name of this inventory item

    [SerializeField] InventoryType inventoryType;       // the type of this inventory item

    [SerializeField] Sprite itemIcon;                   // the icon of this inventory item

    [SerializeField] int stackSize;                     // amount of items that can be stacked

    [SerializeField] InventorySlot slot;                // reference to this item's inventory slot

    /// <summary>
    /// Returns the inventory type of this item.
    /// </summary>

    public InventoryType GetInventoryType()
    {
        return inventoryType;
    }

    /// <summary>
    /// Returns the name of this item.
    /// </summary>

    public string GetName()
    {
        return itemName;
    }

    /// <summary>
    /// Returns the icon of this item.
    /// </summary>

    public Sprite GetIcon()
    {
        return itemIcon;
    }

    /// <summary>
    /// Returns the number of this item, that can be stacked in a slot.
    /// </summary>
    /// <returns></returns>

    public int GetStackSize()
    {
        return stackSize;
    }

    /// <summary>
    /// Returns the item's slot, also possible to set it on pickup.
    /// </summary>

    public InventorySlot Slot
    {
        get { return slot; } set { slot = value; }
    }

    public Sprite icon
    {
        get { return itemIcon; }
    }

    /// <summary>
    /// Handles clicks on items in the inventory.
    /// </summary>

    public virtual void Use()
    {
        // should be overwritten
    }

    /// <summary>
    /// Responsible for removing an inventory item.
    /// </summary>

    public void RemoveFromInventory()
    {
        if (slot != null)
        {
            slot.RemoveItem(this);
        }
    }
}
