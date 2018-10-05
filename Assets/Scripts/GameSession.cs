using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	public int playerLives = 3;
	public int starsCollected = 0;
	public int[][] starsTracking;

	[SerializeField] Text livesText;
	[SerializeField] Text scoreText;

	int coinAmount = 0;
	int totalStars;

	private void Awake()
	{
		int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
		if (numberOfGameSessions > 1)
		{
			Destroy(gameObject);
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
		print("starTracking[0]: " + starsTracking[0][0] + " " + starsTracking[0][1] + " " + starsTracking[0][2]);
		print("starTracking[1]: " + starsTracking[1][0] + " " + starsTracking[1][1] + " " + starsTracking[1][2]);
		print("starTracking[2]: " + starsTracking[2][0] + " " + starsTracking[2][1] + " " + starsTracking[2][2]);
		print("starTracking[3]: " + starsTracking[3][0] + " " + starsTracking[3][1] + " " + starsTracking[3][2]);
		print("starTracking[4]: " + starsTracking[4][0] + " " + starsTracking[4][1] + " " + starsTracking[4][2]);
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
		}
		else
		{
			SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
		}
	}

}
