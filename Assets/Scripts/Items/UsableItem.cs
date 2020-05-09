using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class of all usable items.
/// </summary>

[CreateAssetMenu(fileName = "New Usable Item", menuName = "Inventory/Usable Item ")]
public class UsableItem : Item
{
    [Space]

    [Header("Usable Item Type")]

    [SerializeField] UsableItemType usableItemType;     // the type of usable this item is

    [Header("Modifier")]

    [SerializeField] int amount;                        // the amount to boost

    Player player;

    /// <summary>
    /// Overrides the Item use function, to equip item.
    /// </summary>

    public override void Use()
    {
        base.Use();

        if (player == null)
        {
            player = Player.instance;
        }

        switch (usableItemType)
        {
            case UsableItemType.HealthBoost:
                BoostHealth();
                break;

            case UsableItemType.ExperienceBoost:
                BoostExperience();
                break;
        }
    }

    /// <summary>
    /// Heals the player by the amount of the item.
    /// </summary>

    void BoostHealth()
    {
        if (player.healthSystemAnimation.GetCurrentHealth() <
            player.healthSystemAnimation.GetMaxHealth())
        {
            player.Heal(amount);
            RemoveFromInventory();
        }
        else
        {
            Debug.Log("Player is already at full health");
        }
    }

    /// <summary>
    /// Heals the player by the amount of the item.
    /// </summary>

    void BoostExperience()
    {
        player.AddExperience(amount);
        RemoveFromInventory();
    }
}
