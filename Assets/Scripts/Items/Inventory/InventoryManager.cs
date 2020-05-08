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

    public Item testItem;

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
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddItem(testItem);
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
    /// Responsible for showing the inventory window.
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
    }

    /// <summary>
    /// Inserts the item in the proper bag.
    /// </summary>

    void AddToBag(Item item, Bag bag)
    {
        if (bag.AddItem(item))
        {
            Debug.Log(item + " was added in " + bag);
        }
        else
        {
            Debug.Log("Something went wrong, " + item + " was not put in " + bag);
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
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
                //equipment.Remove(item);
                break;

            case InventoryType.Usable:
                //usableItems.Remove(item);
                break;

            case InventoryType.Quest:
                //questItems.Remove(item);
                break;
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
