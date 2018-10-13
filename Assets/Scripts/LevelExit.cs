using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

	public bool loadingNextLevel = false;
	public int[] starsInLevel;

	[SerializeField] float delayUntilNextSceneLoad = 1.0f;
	[SerializeField] float LevelExitSloMoFactor = 0.2f;

	GameSession gameSession;
	StarCounter starCounter;
	int starOne;
	int starTwo;
	int starThree;
	bool addedStars = false;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		gameSession = FindObjectOfType<GameSession>();
		starCounter = FindObjectOfType<StarCounter>();
		int currentScene = SceneManager.GetActiveScene().buildIndex;

		CalculateStarIntValue();
		WriteStarsInProgressTracker(currentScene);
		StartCoroutine(LoadNextScene());
	}

	private void WriteStarsInProgressTracker(int currentScene)
	{
		gameSession.starsTracking[currentScene - 1] = new int[] { starOne, starTwo, starThree };
		if (addedStars == false)
		{
			gameSession.starsFromAllLevels += starOne + starTwo + starThree;
			addedStars = true;
		}
	}

	private void CalculateStarIntValue()
	{
		if (starCounter.firstStar == null)
		{
			starOne = 1;
		}
		else
		{
			starOne = 0;
		}
		if (starCounter.secondStar == null)
		{
			starTwo = 1;
		}
		else
		{
			starTwo = 0;
		}
		if (starCounter.thirdStar == null)
		{
			starThree = 1;
		}
		else
		{
			starThree = 0;
		}
	}

	IEnumerator LoadNextScene()
	{
		gameSession.currentCheckpointNumber = 0;
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
