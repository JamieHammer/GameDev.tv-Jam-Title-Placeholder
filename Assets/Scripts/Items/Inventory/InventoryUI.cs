﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the inventory UI.
/// </summary>

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;         // singleton

    public Transform equipmentParent;           // the parent transform of the equipment slots
    public Transform usableItemsParent;         // the parent transform of the usable item slots
    public Transform questItemsParent;          // the parent transform of the quest item slots

    public TextMeshProUGUI titleText;           // the title text component

    InventoryManager inventory;                 // reference to the inventory manager

    public List<InventorySlot> equipmentSlots = new List<InventorySlot>();      // a list of equipment slots
    public List<InventorySlot> usableItemSlots = new List<InventorySlot>();  // a list of equipment slots
    public List<InventorySlot> questItemSlots = new List<InventorySlot>();      // a list of equipment slots

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

        SetupSlots();
    }

    private void OnEnable()
    {
        Debug.Log("Hello there");
    }

    /// <summary>
    /// Responsible for setting up the slots before deactivating itself.
    /// </summary>

    private void SetupSlots()
    {
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateUI;

        SetupBag(equipmentParent, equipmentSlots);
        SetupBag(usableItemsParent, usableItemSlots);
        SetupBag(questItemsParent, questItemSlots);

        gameObject.SetActive(false);
    }

    void SetupBag(Transform parent, List<InventorySlot> slots)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            InventorySlot slot = parent.GetChild(i).GetComponent<InventorySlot>();
            slots.Add(slot);
        }
    }

    /// <summary>
    /// Iterates through all inventory items and updates the UI.
    /// </summary>

    public void UpdateUI()
    {
        HideAll();

        switch(inventory.currentInventory)
        {
            case InventoryType.None:

                break;

            case InventoryType.Equipment:
                UpdateTitle("Equipment");
                equipmentParent.gameObject.SetActive(true);
                //PopulateSlots(inventory.equipment);
                break;

            case InventoryType.Usable:
                UpdateTitle("Usables");
                usableItemsParent.gameObject.SetActive(true);
                //PopulateSlots(inventory.usableItems);
                break;

            case InventoryType.Quest:
                UpdateTitle("Quest items");
                questItemsParent.gameObject.SetActive(true);
                //PopulateSlots(inventory.questItems);
                break;
        }
    }

    /// <summary>
    /// Hides all of the inventory slots.
    /// </summary>

    void HideAll()
    {
        equipmentParent.gameObject.SetActive(false);
        usableItemsParent.gameObject.SetActive(false);
        questItemsParent.gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates the title of the inventory window.
    /// </summary>
    /// <param name="title">the active title</param>

    void UpdateTitle(string title)
    {
        titleText.text = title;
    }

    /// <summary>
    /// Responsible for populating theinventory slots.
    /// </summary>
    /// <param name="items">a list of items to populate the slots with</param>

    void PopulateSlots(List<Item> items)
    {
        /*

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.questItems.Count)
            {
                inventorySlots[i].AddItem(items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
        */
    }
}
