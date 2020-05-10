using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Responsible for the inventory system.
/// </summary>

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public Bag equipmentBag;                            // a bag for equipment
    public Bag usableItemsBag;                          // a bag for usable items
    public Bag questItemsBag;                           // a bag for quest items

    public GameObject inventoryWindow;                  // reference to the inventory window
    InventoryUI inventoryUI;                            // reference to the inventory UI

    public InventoryType currentInventory;              // the current type of inventory selected

    public TextMeshProUGUI titleText;                   // the text component of the title

    public InventorySlot movingSlot;                           // reference to the slot being moved

    // DEBUG

    public Item[] weapons;
    public Item[] clothes;
    public Item[] usables;

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

        inventoryUI = InventoryUI.instance;
    }

    private void Update()
    {
        #region Debug

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddItem(weapons[Random.Range(0, weapons.Length - 1)]);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddItem(usables[Random.Range(0, usables.Length - 1)]);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddItem(clothes[Random.Range(0, clothes.Length - 1)]);
        }

        #endregion
    }

    /// <summary>
    /// Handles the slot when items are being moved.
    /// </summary>

    public InventorySlot MovingSlot
    {
        get
        {
            return movingSlot;
        }

        set
        {
            movingSlot = value;

            if (value != null)
            {
                movingSlot.icon.color = Color.grey;
            }
        }
    }

    /// <summary>
    /// To show the title of the hovered inventory bag, while hovered.
    /// </summary>
    /// <param name="newTitle">The new title to show</param>

    public void UpdateTitle(string newTitle)
    {
        titleText.text = newTitle;
    }

    /// <summary>
    /// To revert back to the title after hover has ended.
    /// </summary>

    public void RevertTitle()
    {
        titleText.text = "Inventory";
    }

    /// <summary>
    /// Responsible for showing equipment.
    /// </summary>

    public void ToggleEquipment()
    {
        if (currentInventory != InventoryType.Equipment)
        {
            currentInventory = InventoryType.Equipment;
            ShowInventory();
        }
        else
        {
            HideInventory();
        }
    }

    /// <summary>
    /// Responsible for showing equipment.
    /// </summary>

    public void ToggleUsables()
    {
        if (currentInventory != InventoryType.Usable)
        {
            currentInventory = InventoryType.Usable;
            ShowInventory();
        }
        else
        {
            HideInventory();
        }
    }

    /// <summary>
    /// Responsible for showing equipment.
    /// </summary>

    public void ToggleQuestItems()
    {
        if (currentInventory != InventoryType.Quest)
        {
            currentInventory = InventoryType.Quest;
            ShowInventory();
        }
        else
        {
            HideInventory();
        }
    }

    /// <summary>
    /// Responsible for showing the inventory window.
    /// </summary>

    void ShowInventory()
    {
        if (inventoryUI == null)
        {
            inventoryUI = InventoryUI.instance;
        }

        inventoryWindow.SetActive(true);
        inventoryUI.UpdateUI();
    }

    /// <summary>
    /// Responsible for hiding the inventory window.
    /// </summary>

    public void HideInventory()
    {
        inventoryWindow.SetActive(false);
        currentInventory = InventoryType.None;
    }

    /// <summary>
    /// Responsible for adding items to the inventory system.
    /// </summary>
    /// <param name="item">item to add to inventory</param>

    public void AddItem(Item item)
    {
        // if item is stackable try to stack it first

        if (item.GetStackSize() > 1)
        {
            switch (item.GetInventoryType())
            {
                case InventoryType.Equipment:
                    if (PlaceInStack(item, equipmentBag))
                    {
                        return;
                    }
                    break;

                case InventoryType.Usable:
                    if (PlaceInStack(item, usableItemsBag))
                    {
                        return;
                    }
                    break;

                case InventoryType.Quest:
                    if (PlaceInStack(item, questItemsBag))
                    {
                        return;
                    }
                    break;
            }
        }

        // else try to add the item to an empty slot of the bag

        switch (item.GetInventoryType())
        {
            case InventoryType.Equipment:
                AddToBag(item, equipmentBag);
                break;

            case InventoryType.Usable:
                AddToBag(item, usableItemsBag);
                break;

            case InventoryType.Quest:
                AddToBag(item, questItemsBag);
                break;
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    /// <summary>
    /// Responsible for trying to put an item into an empty slot in the bag.
    /// </summary>
    /// <param name="item">The item to check</param>
    /// <param name="bag">The bag to check</param>

    void AddToBag(Item item, Bag bag)
    {
        if (bag.AddItem(item))
        {
            Debug.Log(item + " was added in " + bag);
            return;
        }

        Debug.Log("Something went wrong, " + item + " was not put in " + bag);
    }

    /// <summary>
    /// Responsible for trying to stack an item.
    /// </summary>
    /// <param name="item">The item to check</param>
    /// <param name="bag">The bag to check</param>
    /// <returns>whether or not the stack attempt was sucesful</returns>

    bool PlaceInStack(Item item, Bag bag)
    {
        foreach (var slot in bag.slots)
        {
            if (slot.StackItem(item))
            {
                return true;
            }
        }

        return false;
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
                equipmentBag.RemoveItem(item);
                break;

            case InventoryType.Usable:
                usableItemsBag.RemoveItem(item);
                break;

            case InventoryType.Quest:
                questItemsBag.RemoveItem(item);
                break;
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
