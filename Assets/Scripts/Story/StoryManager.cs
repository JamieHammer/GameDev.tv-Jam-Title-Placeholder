using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Handles the story related titbits.
/// </summary>

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;

    [SerializeField] TextMeshProUGUI storyComponent;        // reference to the story text component
    [SerializeField] State startingState;                   // reference to the starting story state

    ChoiceManager choiceManager;                            // reference to the choice manager
    Player  player;                                         // reference to the player instance

    State currentState;                                     // reference to the current state of the story

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

        currentState = startingState;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;

        UpdateStory();
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    /// <summary>
    /// Allows for keyboard input to control.
    /// </summary>

    private void ManageState()
    {
        var nextStates = currentState.GetNextStates();

        for (int i = 0; i < nextStates.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                AdvanceStory(nextStates[i]);
            }
        }
    }

    /// <summary>
    /// Advances the story when receiving a choice from the choice controller
    /// </summary>

    public void AdvanceStory(State newState)
    {
        currentState = newState;

        switch (currentState.GetStateType())
        {
            case StateType.Buy:
                ItemState();
                break;
        }

        if (currentState.isAnyRewards())
        {
            GetRewards();
        }

        UpdateStory();

        if (choiceManager == null)
        {
            choiceManager = ChoiceManager.instance;
        }

        choiceManager.LoadCurrentChoices();
    }

    /// <summary>
    /// Responsible for updating the story text.
    /// </summary>

    void UpdateStory()
    {
        string storyLine = currentState.GetStateStory();
        storyLine = storyLine.Replace("$playerName", player.characterName);

        storyComponent.text = storyLine;
    }

    /// <summary>
    /// Returns the current story state.
    /// </summary>

    public State GetCurrentStoryState()
    {
        return currentState;
    }

    public Player  GetPlayer()
    {
        return player;
    }

    void GetRewards()
    {
        foreach (Item reward in currentState.GetRewards())
        {
            InventoryManager.instance.Add(reward);

            Debug.Log("player should have received: " + reward);
        }
    }

    public void ItemState()
    {
        if (currentState.GiveToPlayer())
        {
            if (currentState.GetItem() != null)
            {
                InventoryManager.instance.Add(currentState.GetItem());

                Debug.Log("player should have received: " + currentState.GetItem());
            }
        }
    }
}
