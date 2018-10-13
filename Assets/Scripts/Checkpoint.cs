using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public int checkpointInLevel = 1;
	[SerializeField] Sprite checkpointChecked;

	GameSession gameSession;

	// Use this for initialization
	void Start () {
		gameSession = FindObjectOfType<GameSession>();
		if (checkpointInLevel <= gameSession.currentCheckpointNumber)
		{
			GetComponent<SpriteRenderer>().sprite = checkpointChecked;
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameSession.currentCheckpointNumber = checkpointInLevel;
		GetComponent<SpriteRenderer>().sprite = checkpointChecked;
		GetComponent<BoxCollider2D>().enabled = false;
	}
}
