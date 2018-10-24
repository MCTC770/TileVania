using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastLevelExit : MonoBehaviour
{

	public bool loadingNextLevel = false;
	[SerializeField] float delayUntilNextSceneLoad = 1.0f;
	[SerializeField] float LevelExitSloMoFactor = 0.2f;
	GameSession gameSession;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		gameSession = FindObjectOfType<GameSession>();
		StartCoroutine(LoadNextScene());
	}

	IEnumerator LoadNextScene()
	{
		loadingNextLevel = true;

		gameSession.playerLives = 3;
		gameSession.starsCollected = 0;
		gameSession.starsTracking = null;
		gameSession.starsFromAllLevels = 0;

		Time.timeScale = LevelExitSloMoFactor;

		yield return new WaitForSeconds(delayUntilNextSceneLoad);
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
