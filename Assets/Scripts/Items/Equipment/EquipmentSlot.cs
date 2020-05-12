using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Manages the equipment part of the slot.
/// </summary>

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] EquipmentType equipmentType;       // to determine which type of equipment belongs to this slot

    [SerializeField] Equipment equipment;               // reference to the equipment item of this slot

    [SerializeField] Image icon;                        // reference to the icon image component on this slot

    [SerializeField] Image emptySlot;                   // reference to the empty slot icon

    /// <summary>
    /// To handle click events on this slot.
    /// </summary>

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (MoveUI.instance.moveable is Equipment)
            {
                Equipment tmp = (Equipment)MoveUI.instance.moveable;

                if (tmp.GetEquipmentType() == equipmentType)
                {
                    EquipItem(tmp);
                }

                UIManager.instance.RefreshTooltip();
            }
            else if (MoveUI.instance.moveable == null && equipment != null)
            {
                MoveUI.instance.TakeMoveable(equipment);
                CharacterPanel.instance.currentlySelectedSlot = this;
                icon.color = Color.grey;
            }
        }
    }

    /// <summary>
    /// Handles mouse enter events on this slot button.
    /// </summary>

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equipment != null)
        {
            UIManager.instance.ShowTooltip(new Vector2(0, 0), transform.position, equipment);
        }
    }

    /// <summary>
    /// Handles mouse exit events on this slot button.
    /// </summary>

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.HideTooltip();
    }

    /// <summary>
    /// Returns the current equipment.
    /// </summary>

    public Equipment GetEquipment()
    {
        return equipment;
    }

    /// <summary>
    /// Responsible for equiping an item.
    /// </summary>
    /// <param name="newEquipment">the item to equip</param>

    public void EquipItem(Equipment newEquipment)
    {
        newEquipment.RemoveFromInventory();

        if (equipment != null)
        {
            if (equipment != newEquipment)
            {
                equipment.Slot.AddItem(equipment);
            }

            UIManager.instance.RefreshTooltip();
        }
        else
        {
            UIManager.instance.HideTooltip();
        }

        emptySlot.enabled = false;

        icon.enabled = true;
        icon.sprite = newEquipment.icon;
        icon.color = Color.white;
        equipment = newEquipment;
        equipment.equipmentSlot = this;

        if (MoveUI.instance.moveable == (newEquipment as Moveable))
        {
            MoveUI.instance.Drop();
        }
    }

    /// <summary>
    /// Responsible for dequiping an item.
    /// </summary>

    public void DequipItem()
    {
        emptySlot.enabled = true;
        icon.color = Color.white;
        icon.enabled = false;
        equipment = null;
    }
}
