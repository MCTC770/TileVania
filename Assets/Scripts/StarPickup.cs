using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : MonoBehaviour {

	GameSession addStars;
	LayerMask playerLayer = 11;

	// Use this for initialization
	void Start () {
		addStars = FindObjectOfType<GameSession>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == playerLayer && !collision.isTrigger)
		{
			addStars.TrackStarAmount();
			Destroy(gameObject);
		}
	}
}
