using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of all usable items.
/// </summary>

[CreateAssetMenu(fileName = "New Usable Item", menuName = "Inventory/Usable Item ")]
public class UsableItem : Item
{
    Player  player;

    /// <summary>
    /// Overrides the Item use function, to equip item.
    /// </summary>

    public override void Use()
    {
        base.Use();

        player = StoryManager.instance.GetPlayer();

        // todo if usable item is of type heal todo create usable item types
        // heal the amount instead of the hardcoded 5 below

        player.Heal(5);

        RemoveFromInventory();
    }
}
