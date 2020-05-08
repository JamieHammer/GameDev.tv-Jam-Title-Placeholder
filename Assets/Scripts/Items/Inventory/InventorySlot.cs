﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for the inventory slot the script is attached to.
/// </summary>

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image icon;            // reference to the icon image component of this slot
    [SerializeField] GameObject removeBtn;  // reference to the remove item button of this slot

    Stack<Item> items = new Stack<Item>();  // a stack of items in this slot

    /// <summary>
    /// Returns whether or not the slot is empty.
    /// </summary>

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    /// <summary>
    /// Responsible for adding an item to this inventory slot.
    /// </summary>
    /// <param name="item">the new item to add</param>

    public bool AddItem(Item item)
    {
        items.Push(item);

        icon.sprite = item.GetIcon();
        icon.enabled = true;
        removeBtn.SetActive(true);

        return true;
    }

    /// <summary>
    /// Responsible for clearing this inventory slot.
    /// </summary>

    public void ClearSlot()
    {
        //item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeBtn.SetActive(false);
    }

    /// <summary>
    /// Handles click events for the remove item button.
    /// </summary>

    public void OnRemoveButton()
    {
        //InventoryManager.instance.Remove(item);
    }

    /// <summary>
    /// Handles button clicks on inventory items.
    /// </summary>

    public void UseItem()
    {
        //if (item != null)
        //{
            //item.Use();
        //}
    }
}
