using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To create story states from the inspector.
/// </summary>

[CreateAssetMenu(fileName = "New Story State", menuName = "Story/State")]
public class State : ScriptableObject
{
    [Header("State Type")]

    [SerializeField] StateType stateType;                   // the type of this state

    /// <summary>
    /// Returns the state type of this state.
    /// </summary>

    public StateType GetStateType()
    {
        return stateType;
    }



    [Header("Story")]

    [TextArea(10,14)] [SerializeField] string storyTxt;     // the story text to show in the game UI

    /// <summary>
    /// Returns the current story line as a string. 
    /// </summary>

    public string GetStateStory()
    {
        return storyTxt;
    }



    [Header("Choice")]

    [SerializeField] string choiceTxt;                      // the choice text to show on the button

    /// <summary>
    /// Returns the text of the choice for the button.
    /// </summary>

    public string GetChoiceText()
    {
        return choiceTxt;
    }



    [Header("Branches")]

    [SerializeField] State[] nextStates;                    // the options to advance in the story

    /// <summary>
    /// Returns an array of the next state options.
    /// </summary>

    public State[] GetNextStates()
    {
        return nextStates;
    }



    [Header("Buy states")]

    [SerializeField] Item itemToBuy;                        // the item to buy

    [SerializeField] int price;                             // the cost of the item

    [SerializeField] bool giveToPlayer;                     // whether or not player should have the item

    /// <summary>
    /// Returns the item bought by the player.
    /// </summary>

    public Item GetItem()
    {
        return itemToBuy;
    }

    /// <summary>
    /// Returns the price of the item bought.
    /// </summary>

    public int GetPrice()
    {
        return price;
    }

    /// <summary>
    /// Returns whether or not the player should get the item.
    /// </summary>

    public bool GiveToPlayer()
    {
        return giveToPlayer;
    }



    [Header("Rewards")]

    [SerializeField] Item[] rewards;                        // an array of item rewards

    /// <summary>
    /// Returns an array of rewards for reaching this state.
    /// </summary>

    public Item[] GetRewards()
    {
        return rewards;
    }

    public bool isAnyRewards()
    {
        return rewards.Length > 0;
    }



    [Header("Battle")]

    [SerializeField] GameObject[] enemyPrefabs;

    /// <summary>
    /// Returns an array of enemies to fight.
    /// </summary>
    /// <returns></returns>

    public GameObject[] GetEnemyPrefabs()
    {
        return enemyPrefabs;
    }
}
