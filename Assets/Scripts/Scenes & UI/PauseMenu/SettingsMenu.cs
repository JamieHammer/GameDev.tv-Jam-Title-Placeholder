using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

/// <summary>
/// Responsible for saving user input from the settings menu.
/// </summary>

public class SettingsMenu : MonoBehaviour
{
    [Header("References")]

    public AudioMixer audioMixer;               // reference to the main audio mixer
    public TMP_Dropdown resolutionDropdown;     // reference to the resolution dropdown

    Resolution[] resolutions;                   // an array of available resolutions

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    #region Resolution

    /// <summary>
    /// Responsible for setting the resolution when changed by the player.
    /// </summary>

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Responsible for toggling fullscreen when changed by the player.
    /// </summary>

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    #endregion

    #region Graphics

    /// <summary>
    /// Responsible for setting the quality when changed by the player.
    /// </summary>

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    #endregion

    #region Volume

    /// <summary>
    /// Responsible for setting the volume when changed by the player.
    /// </summary>

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    #endregion
}
