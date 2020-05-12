using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the logic of the action buttons.
/// </summary>

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    public ActionButtonType type;           // to set a button type

    InventorySlot slot;                     // reference to the inventory slot component of this button
    EquipmentSlot equipmentSlot;            // reference to the equipment slot component of this button

    InventoryManager inventoryManager;      // reference to the inventory manager
    EquipmentManager equipmentManager;      // reference to the equipment manager

    // Debug
    bool weaponDebug = false;

    void Start()
    {
        inventoryManager = InventoryManager.instance;
        equipmentManager = EquipmentManager.instance;

        switch (type)
        {
            case ActionButtonType.WeaponSlot:
                equipmentSlot = GetComponent<EquipmentSlot>();
                break;

            case ActionButtonType.QuickUseSlot:
                slot = GetComponent<InventorySlot>();
                break;
        }
    }

    /// <summary>
    /// Responsible for handling click events.
    /// </summary>

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (type)
        {
            case ActionButtonType.WeaponSlot:
                WeaponSlot(eventData);
                break;

            case ActionButtonType.QuickUseSlot:
                QuickUseSlot(eventData);
                break;
        }
    }

    /// <summary>
    /// Responsible for weapon slot specific click events.
    /// </summary>

    void WeaponSlot(PointerEventData eventData)
    {
        if (equipmentSlot.GetEquipment() != null)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (weaponDebug)   // check if already equipped
                {
                    DequipWeapon();
                }
                else
                {
                    EquipWeapon();
                }
            }
        }
        else
        {
            if (MoveUI.instance.moveable == null)
            {
                OpenEquipmentBag();
            }
        }
    }

    /// <summary>
    /// Responsible for quick use slot specific click events.
    /// </summary>

    void QuickUseSlot(PointerEventData eventData)
    {
        if (!slot.IsEmpty)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                slot.UseItem();
            }
        }
        else
        {
            if (MoveUI.instance.moveable == null)
            {
                OpenUsableItemsBag();
            }
        }
    }

    /// <summary>
    /// Equips the selected weapon.
    /// </summary>

    void EquipWeapon()
    {
        // todo equip weapon
        Debug.Log("Equipping weapon");
        weaponDebug = true;
    }

    /// <summary>
    /// Dequips the selected weapon.
    /// </summary>

    void DequipWeapon()
    {
        // todo dequip weapon
        Debug.Log("Dequipping weapon");
        weaponDebug = false;
    }

    /// <summary>
    /// Opens the equipment bag, in case the slot is empty.
    /// </summary>

    void OpenEquipmentBag()
    {
        // todo
        Debug.Log("Open equipment inventory bag");
    }

    /// <summary>
    /// Opens the usable items bag, in case the slot is empty.
    /// </summary>

    void OpenUsableItemsBag()
    {
        // todo
        Debug.Log("Open usable items inventory bag");
    }
}
