using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    [SerializeField] GameObject[] panels;           // an array of content panels
    [SerializeField] TabGroup tabGroup;             // reference to the tab group

    [SerializeField] int panelIndex;                // the index of the panel

    private void Awake()
    {
        ShowCurrentPanel();
    }

    /// <summary>
    /// Show current panel and hide the rest.
    /// </summary>

    void ShowCurrentPanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelIndex)
            {
                panels[i].gameObject.SetActive(true);
            }
            else
            {
                panels[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Responsible for setting the active page and show it.
    /// </summary>
    /// <param name="index"></param>

    public void SetPageIndex(int index)
    {
        panelIndex = index;
        ShowCurrentPanel();
    }
}
