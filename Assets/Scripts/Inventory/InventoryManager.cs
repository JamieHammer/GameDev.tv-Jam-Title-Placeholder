using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the inventory system.
/// </summary>

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> equipment = new List<Item>();     // a list of inventory equipment
    public List<Item> usableItems = new List<Item>();   // a list of usable items
    public List<Item> questItems = new List<Item>();    // a list of quest items

    public int space = 16;                              // the limited space of a list

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

    /// <summary>
    /// Responsible for adding items to the inventory system.
    /// </summary>
    /// <param name="item">item to add to inventory</param>

    public void Add(Item item)
    {
        switch (item.GetInventoryType())
        {
            case InventoryType.Equipment:
                if (equipment.Count < space)
                {
                    equipment.Add(item);

                    if (onItemChangedCallback != null)
                    {
                        onItemChangedCallback.Invoke();
                    }
                }
                break;

            case InventoryType.Usable:
                if (usableItems.Count < space)
                {
                    usableItems.Add(item);

                    if (onItemChangedCallback != null)
                    {
                        onItemChangedCallback.Invoke();
                    }
                }
                break;

            case InventoryType.Quest:
                if (questItems.Count < space)
                {
                    questItems.Add(item);

                    if (onItemChangedCallback != null)
                    {
                        onItemChangedCallback.Invoke();
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Responsible for removing items from the inventory system.
    /// </summary>
    /// <param name="item">item to remove from inventory</param>

    public void Remove(Item item)
    {
        switch (item.GetInventoryType())
        {
            case InventoryType.Equipment:
                equipment.Remove(item);
                break;

            case InventoryType.Usable:
                usableItems.Remove(item);
                break;

            case InventoryType.Quest:
                questItems.Remove(item);
                break;
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
