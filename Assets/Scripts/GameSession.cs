﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	public int playerLives = 3;
	public int starsCollected = 0;

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
		totalStars = FindObjectsOfType<StarPickup>().Length;
	}

	private void Update()
	{
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
