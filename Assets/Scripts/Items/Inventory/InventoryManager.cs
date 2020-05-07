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

    public List<Item> equipment = new List<Item>();     // a list of inventory equipment
    public List<Item> usableItems = new List<Item>();   // a list of usable items
    public List<Item> questItems = new List<Item>();    // a list of quest items

    public int space = 16;                              // the limited space of a list

    public GameObject inventoryWindow;                  // reference to the inventory window
    InventoryUI inventoryUI;                            // reference to the inventory UI

    public InventoryType currentInventory;              // the current type of inventory selected

    public TextMeshProUGUI titleText;                   // the text component of the title

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

        inventoryUI = inventoryWindow.GetComponent<InventoryUI>();
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
