using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

	int startingScene;

	private void Awake()
	{
		int numberOfScenePersists = FindObjectsOfType<ScenePersist>().Length;

		if (numberOfScenePersists > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		startingScene = SceneManager.GetActiveScene().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
		if (startingScene != SceneManager.GetActiveScene().buildIndex)
		{
			Destroy(gameObject);
		}
	}
}
