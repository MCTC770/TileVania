using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	
	public void LoadFirstLevel()
	{
		int currentScene = SceneManager.GetActiveScene().buildIndex + 1;
		SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
	}
}