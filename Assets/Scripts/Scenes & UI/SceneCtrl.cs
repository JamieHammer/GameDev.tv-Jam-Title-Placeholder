using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Scene controller handles the scene loading and scene navigation requests.
/// </summary>

public class SceneCtrl : MonoBehaviour
{
	#region Singleton
	public static SceneCtrl instance;           // to create a singleton

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);

		DontDestroyOnLoad(this);
	}

	#endregion

	//DataCtrl dataCtrl;                          // reference to the data controller

	//SelectType selectType;                      // reference to the prefered select type

	int currentScene;                           // reference to the current scene
	int lastActiveScene;                        // reference to the last active scene

	void Start()
    {
		//dataCtrl = DataCtrl.instance;

		//selectType = SelectType.Categories;

		currentScene = 0;
	}


}
