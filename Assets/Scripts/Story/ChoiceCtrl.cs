using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles the choice button its and logic.
/// </summary>

public class ChoiceCtrl : MonoBehaviour
{
    State choice;               // the state of this choice

    Button choiceBtn;           // the button component of this choice
    TextMeshProUGUI choiceTxt;  // the text component of the button

    private void Awake()
    {
        choiceBtn = GetComponent<Button>();
        choiceTxt = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// Setup of the choice button spawned by the choice manager.
    /// </summary>

    public void SetupChoice(State state)
    {
        choice = state;

        choiceTxt.text = choice.GetChoiceText();
    }

    /// <summary>
    /// Handles button clicks and sends choice to the story manager.
    /// </summary>

    public void ChoiceMade()
    {
        StoryManager.instance.AdvanceStory(choice);
        ChoiceManager.instance.ResetScroll();
    }
}
