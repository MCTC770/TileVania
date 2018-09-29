using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	[SerializeField] Transform coinPickupSound;
	[SerializeField] int coinScore = 100;
	GameSession addScore;

	private void Start()
	{
		addScore = FindObjectOfType<GameSession>();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Player")
		{
			Transform coinSoundInstance = Instantiate(coinPickupSound, transform.position, Quaternion.identity);
			coinSoundInstance.parent = transform.parent;

			addScore.CalculateScore(coinScore);

			Destroy(gameObject);
		}
	}
}
