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
    public Image icon;                      // reference to the icon image component of this slot

    [SerializeField] GameObject removeBtn;  // reference to the remove item button of this slot
    [SerializeField] TextMeshProUGUI stackCount;                // reference to the stack count text component

    ObservableStack<Item> items = new ObservableStack<Item>();  // a stack of items in this slot

    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
    }

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
    /// Returns whether or not the slot is full.
    /// </summary>

    public bool IsFull
    {
        get
        {
            if (IsEmpty || items.Count < item.GetStackSize())
            {
                return false;
            }

            return true;
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
        RemoveItem(item);
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

    /// <summary>
    /// Handles click events on this slot button.
    /// </summary>

    public void OnPointerClick(PointerEventData eventData)
    {
        // if right mouse click, use item

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }

        // if left mouse click, enable move

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // if we don't have anything to move

            if (InventoryManager.instance.MovingSlot == null && !IsEmpty)
            {
                MoveUI.instance.TakeMovable(item as Moveable);
                InventoryManager.instance.MovingSlot = this;
            }
            else if (InventoryManager.instance.MovingSlot != null)      // if we do have something to move
            {
                // if the same slot is clicked, item is to be put back

                if (PutItemBack() || SwapItems(InventoryManager.instance.MovingSlot) ||
                    AddItems(InventoryManager.instance.MovingSlot.items))
                {
                    MoveUI.instance.Drop();  
                    InventoryManager.instance.MovingSlot = null;
                }
            }
        }
    }

    /// <summary>
    /// Returns whether or not the moved item should be put back.
    /// </summary>

    bool PutItemBack()
    {
        if (InventoryManager.instance.MovingSlot == this)
        {
            InventoryManager.instance.MovingSlot.icon.color = Color.white;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns whether or not it was possible to swap the two item stacks.
    /// </summary>
    /// <param name="from">the first clicked slot</param>

    bool SwapItems(InventorySlot from)
    {
        if (IsEmpty)
        {
            return false;
        }

        if (from.item.GetInventoryType() != item.GetInventoryType() ||
            from.items.Count + items.Count > item.GetStackSize())
        {
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);

            from.items.Clear();
            from.AddItems(items);

            items.Clear();
            AddItems(tmpFrom);

            Debug.Log("Item Swap");
            return true;
        }

        Debug.Log("Swap failed");

        return false;
    }

    /// <summary>
    /// Responsible for trying to add the items to the slot.
    /// </summary>
    /// <param name="newItems">the stack of new items</param>
    /// <returns></returns>

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetInventoryType() == item.GetInventoryType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }

            return true;
        }

        return false;
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

    /// <summary>
    /// Responsible for updating the stack size.
    /// </summary>

    public void UpdateStackSize()
    {
        if (items.Count > 1)
        {
            stackCount.text = items.Count.ToString();
            stackCount.color = Color.white;
            icon.color = Color.white;
        }
        else
        {
            stackCount.color = new Color(0, 0, 0, 0);
        }

        if (IsEmpty)
        {
            icon.color = new Color(0, 0, 0, 0);
            stackCount.color = new Color(0, 0, 0, 0);
            removeBtn.SetActive(false);
        }
    }

    /// <summary>
    /// Updates the slot whenever any changes are made to it.
    /// </summary>

    void UpdateSlot()
    {
        UpdateStackSize();
    }
}
