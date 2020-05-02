using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MenuManager handles the setup and switching between canvas views.
/// </summary>

public class MainMenuCtrl : MonoBehaviour
{
    #region Singleton
    public static MainMenuCtrl instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion

    public GameObject main;                     // the main menu
    public GameObject options;                  // the options menu

    void Start()
    {
        //dataCtrl = DataCtrl.instance;

        //SwitchCanvas(MenuType.MainMenu);

        //ButtonLayout();
    }

    /// <summary>
    /// Handles callbacks on the continue game button.
    /// </summary>

    public void Continue()
    {
        Debug.Log("Continue Game");
    }

    /// <summary>
    /// Handles callbacks on the new game button.
    /// </summary>

    public void NewGame()
    {
        Debug.Log("New Game");
    }

    /// <summary>
    /// Handles callbacks on the game options button.
    /// </summary>

    public void Options()
    {
        options.SetActive(true);
        main.SetActive(false);
    }

    /// <summary>
    /// Handles callbacks on the back button.
    /// </summary>

    public void Back()
    {
        main.SetActive(true);
        options.SetActive(false);
    }

    /// <summary>
    /// Handles callbacks on the quit game button.
    /// </summary>

    public void Quit()
    {
        Application.Quit();
    }
}
