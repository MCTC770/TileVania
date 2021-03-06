﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	public int playerLives = 3;

	[SerializeField] Text livesText;
	[SerializeField] Text scoreText;

	int score = 0;

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

	}

	private void Update()
	{
		UpdateUITexts();
	}

	private void UpdateUITexts()
	{
		livesText.text = playerLives.ToString();
		scoreText.text = score.ToString();
	}

	public void CalculateScore(int pointsToAdd)
	{
		score += pointsToAdd;
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
