using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of all quest items.
/// </summary>

[CreateAssetMenu(fileName = "New Quest Item", menuName = "Inventory/Quest Item")]
public class QuestItem : Item
{
    /// <summary>
    /// Overrides the Item use function, to equip item.
    /// </summary>

    public override void Use()
    {
        base.Use();

        // check if can use

        // if so use

        // ...and remove from inventory if item is to be removed
    }
}
