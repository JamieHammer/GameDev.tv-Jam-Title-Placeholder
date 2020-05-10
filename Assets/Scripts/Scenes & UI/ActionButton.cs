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

    InventoryManager inventoryManager;      // reference to the inventory manager
    EquipmentManager equipmentManager;      // reference to the equipment manager

    // Debug
    bool weaponDebug = false;

    void Start()
    {
        inventoryManager = InventoryManager.instance;
        equipmentManager = EquipmentManager.instance;
        slot = GetComponent<InventorySlot>();
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!slot.IsEmpty)
        {
            switch (type)
            {
                case ActionButtonType.WeaponSlot:
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
                    break;

                case ActionButtonType.QuickUseSlot:
                    if (eventData.button == PointerEventData.InputButton.Right)
                    {
                        slot.UseItem();
                    }
                    break;
            }
        }
        else
        {
            if (MoveUI.instance.moveable == null)
            {
                switch (type)
                {
                    case ActionButtonType.WeaponSlot:
                        OpenEquipmentBag();
                        break;

                    case ActionButtonType.QuickUseSlot:
                        OpenUsableItemsBag();
                        break;
                }
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
