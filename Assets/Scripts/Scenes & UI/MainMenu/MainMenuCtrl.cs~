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

        menuButtons.ForEach(x => x.gameObject.SetActive(false));
    }

    #endregion

    public List<Button> menuButtons;                // list of menu buttons

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

    }

    /// <summary>
    /// Handles callbacks on the new game button.
    /// </summary>

    public void NewGame()
    {

    }

    /// <summary>
    /// Handles callbacks on the game options button.
    /// </summary>

    public void Options()
    {

    }

    /// <summary>
    /// Handles callbacks on the quit game button.
    /// </summary>

    public void Quit()
    {
        Application.Quit();
    }
}
