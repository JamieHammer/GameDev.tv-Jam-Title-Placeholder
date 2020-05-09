/// <summary>
/// 
/// </summary>

public enum GameState
{

}

/// <summary>
/// The state of a battle.
/// </summary>

public enum BattleState
{
    Start, PlayerTurn, EnemyTurn, Won, Lost
}

/// <summary>
/// To control which type the state has.
/// </summary>

public enum StateType
{
    Story, Battle, Buy
}

/// <summary>
/// To define a gender of a character.
/// </summary>

public enum Gender
{
    None, Male, Female
}

/// <summary>
/// To define the race of a character.
/// </summary>

public enum Race
{
    Human
}

/// <summary>
/// To distinct between the inventory types.
/// </summary>

public enum InventoryType
{
    None, Equipment, Usable, Quest
}

/// <summary>
/// To clarify which type of equipment the item is.
/// </summary>

public enum EquipmentType
{
    Weapon, Armour, Shield
}

/// <summary>
/// To clarify which type of usable the item is.
/// </summary>

public enum UsableItemType
{
    HealthBoost, ExperienceBoost
}

/// <summary>
/// To clarify which action button type the butten is.
/// </summary>

public enum ActionButtonType
{
    WeaponSlot, QuickUseSlot
}

