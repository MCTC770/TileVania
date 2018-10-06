using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

	[SerializeField] float deathGravity = 5f;
	[SerializeField] float deathAnimationUplift = 0.1f;
	[SerializeField] float invokeDeathFall = 0.5f;
	[SerializeField] float deathSequenceTime = 0.8f;
	GameSession gameSession;

	// Use this for initialization
	void Start () {
		gameSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().gravityScale = 1f;
		GetComponent<Rigidbody2D>().velocity = Vector2.up * deathAnimationUplift;
		Invoke("DeathFall", invokeDeathFall);
		Invoke("LoseALive", deathSequenceTime);
	}

	private void LoseALive()
	{
		gameSession.playerLives -= 1;
		Destroy(gameObject);
	}

	private void DeathFall()
	{
		GetComponent<Rigidbody2D>().gravityScale = deathGravity;
	}
}
