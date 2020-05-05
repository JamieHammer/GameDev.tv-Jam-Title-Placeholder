using System.Collections;
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

    [SerializeField] Item item;                              // reference to the item currently held by this slot

    /// <summary>
    /// Responsible for adding an item to this inventory slot.
    /// </summary>
    /// <param name="newItem">the new item to add</param>

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.GetIcon();
        icon.enabled = true;
        removeBtn.SetActive(true);
    }

    /// <summary>
    /// Responsible for clearing this inventory slot.
    /// </summary>

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeBtn.SetActive(false);
    }

    /// <summary>
    /// Handles click events for the remove item button.
    /// </summary>

    public void OnRemoveButton()
    {
        InventoryManager.instance.Remove(item);
    }

    /// <summary>
    /// Handles button clicks on inventory items.
    /// </summary>

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
