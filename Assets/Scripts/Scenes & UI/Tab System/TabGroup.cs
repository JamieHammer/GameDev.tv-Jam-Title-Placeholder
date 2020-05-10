using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Responsible for the whole tab group.
/// </summary>

public class TabGroup : MonoBehaviour
{
    [SerializeField] List<TabButton> tabButtons;        // a list of all the tab buttons

    [Space]

    [SerializeField] Color32 tabIdle;                   // the colour of the idle tab button
    [SerializeField] Color32 tabHover;                  // the colour of the hovered tab button
    [SerializeField] Color32 tabActive;                 // the colour of the selected tab button
    [SerializeField] Color32 tabExit;                   // the colour of the selected exit button

    [Space]

    [SerializeField] Color32 txtLight;                  // the colour of the light button text
    [SerializeField] Color32 txtDark;                   // the colour of the dark button text

    [Space]

    [SerializeField] TabButton selectedTab;             // reference to the active tab

    [Space]

    [SerializeField] PanelGroup panelGroup;             // reference to the panel group

    private void Start()
    {
        if (selectedTab == null)
        {
            OnTabSelected(tabButtons[0]);
        }
    }

    /// <summary>
    /// Handles mouse over events.
    /// </summary>
    /// <param name="button">The tab button highlighted</param>

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();

        if (selectedTab == null || button != selectedTab)
        {
            button.UpdateBackground(tabHover);
            button.UpdateText(txtLight);
        }
    }

    /// <summary>
    /// Handles mouse exit events.
    /// </summary>
    /// <param name="button">The tab button highlighted</param>

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    /// <summary>
    /// Handles mouse click events.
    /// </summary>
    /// <param name="button">The tab button highlighted</param>

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;

        selectedTab.Select();

        ResetTabs();

        button.UpdateBackground(tabActive);
        button.UpdateText(txtLight);

        if (panelGroup != null)
        {
            panelGroup.SetPageIndex(selectedTab.transform.GetSiblingIndex());
        }
    }

    /// <summary>
    /// Responsible for reseting the tab buttons.
    /// </summary>

    public void ResetTabs()
    {
        for (int i = 0; i < tabButtons.Count; i++)
        {
            TabButton button = tabButtons[i];

            if (selectedTab != null && button == selectedTab) { continue; }

            if (i == (tabButtons.Count - 1))
            {
                button.UpdateBackground(tabExit);
                button.UpdateText(txtLight);
            }
            else
            {
                button.UpdateBackground(tabIdle);
                button.UpdateText(txtDark);
            }
        }
    }
}
