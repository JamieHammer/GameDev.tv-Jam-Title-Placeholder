using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// Responsible for the inventory slot the script is attached to.
/// </summary>

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;            // reference to the icon image component of this slot
    [SerializeField] GameObject removeBtn;  // reference to the remove item button of this slot
    [SerializeField] TextMeshProUGUI stackCount;    // reference to the stack count text component

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

        UpdateSlot();
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

        UpdateSlot();
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
        // probably doesn't work anymore - todo reimplement

        InventoryManager.instance.Remove(item);

        UpdateSlot();
    }

    /// <summary>
    /// Handles button clicks on inventory items.
    /// </summary>

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            //RemoveItem(item);
        }

        UpdateSlot();
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

        // todo if left mouse click show tooltip
    }

    /// <summary>
    /// Responsible for trying to stack an item into this bag slot.
    /// </summary>
    /// <param name="item">the item to stack</param>
    /// <returns>whether or not the stack was succesful</returns>

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.GetName() == this.item.GetName() && items.Count < this.item.GetStackSize())
        {
            items.Push(item);
            item.Slot = this;
            UpdateSlot();
            return true;
        }

        return false;
    }

    public void UpdateStackSize()
    {
        if (items.Count > 1)
        {
            stackCount.text = items.Count.ToString();
        }
        else
        {
            stackCount.text = "";
        }
    }

    void UpdateSlot()
    {
        UpdateStackSize();
    }
}
