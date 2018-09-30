using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : MonoBehaviour {

	GameSession addStars;
	GameObject player;
	CapsuleCollider2D[] playerColliders;

	// Use this for initialization
	void Start () {
		addStars = FindObjectOfType<GameSession>();
		player = GameObject.Find("Player");
		playerColliders = player.GetComponents<CapsuleCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player" /*&& collision == playerColliders[0]*/)
		{
			addStars.TrackStarAmount();
			Destroy(gameObject);
		}
	}
}
