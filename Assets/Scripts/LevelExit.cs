using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

	[SerializeField] float delayUntilNextSceneLoad = 1.0f;
	[SerializeField] float LevelExitSloMoFactor = 0.2f;
	public bool loadingNextLevel = false;

	private void Start()
	{

	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		StartCoroutine(LoadNextScene());
	}

	IEnumerator LoadNextScene()
	{
		loadingNextLevel = true;
		int sceneCount = SceneManager.sceneCountInBuildSettings - 1;
		int currentScene = SceneManager.GetActiveScene().buildIndex + 1;

		if (currentScene > sceneCount)
		{
			currentScene = sceneCount;
		}

		Time.timeScale = LevelExitSloMoFactor;

		yield return new WaitForSeconds(delayUntilNextSceneLoad);
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
	}

}
