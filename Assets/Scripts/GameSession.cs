using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	public int playerLives = 3;
	public int starsCollected = 0;
	public int[][] starsTracking;
	public int starsFromAllLevels;
	public int currentCheckpointNumber = 0;

	[SerializeField] Text livesText;
	[SerializeField] Text scoreText;
	[SerializeField] float waitTime = 0.5f;

	int coinAmount = 0;
	int totalStars;
	bool updateTotalStarTracker = true;
	Checkpoint[] checkpoints;
	Checkpoint currentCheckpoint;

	private void Awake()
	{
		int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
		if (numberOfGameSessions > 1)
		{
			Destroy(gameObject);
			Debug.Log("here");
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start () {
		starsTracking = new int[SceneManager.sceneCountInBuildSettings][];
		for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			starsTracking[i] = new int[3] {0,0,0};
		}
		totalStars = FindObjectsOfType<StarPickup>().Length;
	}

	private void Update()
	{
		print("currentCheckpointNumber: " + currentCheckpointNumber + " currentCheckpoint: " + currentCheckpoint);
		UpdateUITexts();
	}

	private void UpdateUITexts()
	{
		livesText.text = playerLives.ToString();
		scoreText.text = coinAmount.ToString();
	}

	public void TrackCoinAmount(int coinsToAdd)
	{
		coinAmount += coinsToAdd;
		if (coinAmount >= 100)
		{
			playerLives += 1;
			coinAmount -= 100;
		}

	}

	public void TrackStarAmount()
	{
		starsCollected += 1;
	}

	public void ProcessPlayerDeath()
	{
		if (playerLives < 0)
		{
			playerLives = 0;
		}

		int currentScene = SceneManager.GetActiveScene().buildIndex;
		if (playerLives < 1)
		{
			SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
			Destroy(gameObject);
			Debug.Log("there");
		}
		else
		{
			StartCoroutine(ReloadLevel(currentScene));
		}
	}

	IEnumerator ReloadLevel(int currentScene)
	{
		SceneManager.LoadScene(currentScene, LoadSceneMode.Single);

		yield return new WaitForSeconds(waitTime);

		checkpoints = FindObjectsOfType<Checkpoint>();
		for (var i = 0; i <= checkpoints.Length - 1; i++)
		{
			if (checkpoints[i].checkpointInLevel == currentCheckpointNumber)
			{
				currentCheckpoint = checkpoints[i];
				print("currentCheckpoint: " + currentCheckpoint);
			}
		}

		print("player: " + FindObjectOfType<PlayerMovement>());
		FindObjectOfType<PlayerMovement>().transform.position = currentCheckpoint.transform.position;
		print("player.transform.position: " + FindObjectOfType<PlayerMovement>().transform.position + " currentCheckpoint.transform.position: " + currentCheckpoint.transform.position);
		print(currentCheckpointNumber);
	}
}
