using UnityEngine;

/// <summary>
/// An interface for making something moveable.
/// </summary>

public interface Moveable
{
    /// <summary>
    /// The icon of the UI element, so that the player can see the item being moved.
    /// </summary>

    Sprite icon
    {
        get;
    }
}
