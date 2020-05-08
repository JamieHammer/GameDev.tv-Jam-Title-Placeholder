using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Responsible for the inventory slot the script is attached to.
/// </summary>

public class InventorySlot : MonoBehaviour, IPointerClickHandler
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
        item.Slot = this;
        return true;
    }

    /// <summary>
    /// Responsible for clearing this inventory slot.
    /// </summary>

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            items.Pop();
        }

        if (IsEmpty)
        {
            icon.sprite = null;
            icon.enabled = false;
            removeBtn.SetActive(false);
        }
    }

    /// <summary>
    /// To peek at the top item of the stack, returns null if stack is empty.
    /// </summary>

    public Item item
    {
        get
        {
            if (!IsEmpty)
            {
                return items.Peek();
            }

            return null;
        }
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
            RemoveItem(item);
        }
    }

    /// <summary>
    /// 
    /// </summary>

    public void OnPointerClick(PointerEventData eventData)
    {
        // if right mouse click use item

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }

        // todo if left mouse click
    }
}
