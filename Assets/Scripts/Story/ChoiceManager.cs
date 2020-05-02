using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles everything related to story choices.
/// </summary>

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    [SerializeField] GameObject choice;     // reference to the choice button prefab

    [SerializeField] List<GameObject> currentChoices;   // list of currently spawned choices

    [SerializeField] Scrollbar scroll;      // reference to the scrollbar

    StoryManager storyManager;              // reference to the story manager

    State[] nextStates;                     // array of state choices

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
    }

    // Start is called before the first frame update
    void Start()
    {
        storyManager = StoryManager.instance;

        LoadCurrentChoices();
    }

    /// <summary>
    /// Responsible for loading all of the current choices from the story manager.
    /// </summary>

    public void LoadCurrentChoices()
    {
        ResetChoices();

        var currentState = storyManager.GetCurrentStoryState();

        nextStates = currentState.GetNextStates();

        if (nextStates.Length > 0)
        {
            for (int i = 0; i < nextStates.Length; i++)
            {
                SpawnChoice(i);
            }
        }
    }

    /// <summary>
    /// Handles the spawn of any choice.
    /// </summary>

    void SpawnChoice(int index)
    {
        GameObject choiceInstance = Instantiate(choice, this.transform);

        if (currentChoices == null)
        {
            currentChoices = new List<GameObject>();
        }

        currentChoices.Add(choiceInstance);

        SetupChoice(index);
    }

    /// <summary>
    /// Responsible for setting up the spawned choice button.
    /// </summary>

    void SetupChoice(int index)
    {
        currentChoices[index].GetComponent<ChoiceCtrl>().
            SetupChoice(nextStates[index]);
    }

    /// <summary>
    /// Resets and clears the list of current choices.
    /// </summary>

    private void ResetChoices()
    {
        foreach (GameObject oldChoice in currentChoices)
        {
            Destroy(oldChoice);
        }

        currentChoices.Clear();
    }

    /// <summary>
    /// Resets the scrollbar on choice made.
    /// </summary>

    public void ResetScroll()
    {
        scroll.value = 1;
    }
}
