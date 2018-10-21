using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointCheck : MonoBehaviour {

	bool checkedForGameSession = false;
	GameSession gameSession;
	Checkpoint[] checkpoints;
	Checkpoint currentCheckpoint;

	void Start () {
		int currentScene = SceneManager.GetActiveScene().buildIndex;
		gameSession = FindObjectOfType<GameSession>();
		if (gameSession != null)
		{
			if (gameSession.currentCheckpointNumber != 0)
			{
				StartCoroutine(ReloadLevel(currentScene));
			}
		}
	}

	private void Update()
	{
		if (gameSession == null && !checkedForGameSession)
		{
			int currentScene = SceneManager.GetActiveScene().buildIndex;
			gameSession = FindObjectOfType<GameSession>();
			if (gameSession != null)
			{
				if (gameSession.currentCheckpointNumber != 0)
				{
					StartCoroutine(ReloadLevel(currentScene));
				}
			}
			checkedForGameSession = true;
		}
	}

	IEnumerator ReloadLevel(int currentScene)
	{
		yield return new WaitForSeconds(0f);

		checkpoints = FindObjectsOfType<Checkpoint>();
		for (var i = 0; i <= checkpoints.Length - 1; i++)
		{
			if (checkpoints[i].checkpointInLevel == gameSession.currentCheckpointNumber)
			{
				currentCheckpoint = checkpoints[i];
			}
		}

		FindObjectOfType<PlayerMovement>().transform.position = currentCheckpoint.transform.position;
	}
}
