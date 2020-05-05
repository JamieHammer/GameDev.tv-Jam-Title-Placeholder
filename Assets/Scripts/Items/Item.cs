using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of all inventory items.
/// </summary>

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Inventory Type")]

    [SerializeField] InventoryType inventoryType;       // the type of this inventory item

    [Header("Name")]

    [SerializeField] string itemName;                   // the name of this inventory item

    [Header("Icon")]

    [SerializeField] Sprite itemIcon;                   // the icon of this inventory item

    /// <summary>
    /// Returns the inventory type of this item.
    /// </summary>

    public InventoryType GetInventoryType()
    {
        return inventoryType;
    }

    /// <summary>
    /// Returns the inventory type of this item.
    /// </summary>

    public string GetName()
    {
        return itemName;
    }

    /// <summary>
    /// Returns the inventory type of this item.
    /// </summary>

    public Sprite GetIcon()
    {
        return itemIcon;
    }

    /// <summary>
    /// Handles clicks on items in the inventory.
    /// </summary>

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    /// <summary>
    /// Responsible for removing an inventory item.
    /// </summary>

    public void RemoveFromInventory()
    {
        InventoryManager.instance.Remove(this);
    }
}
