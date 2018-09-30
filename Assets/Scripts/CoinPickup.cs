using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	[SerializeField] Transform coinPickupSound;
	GameObject player;
	CapsuleCollider2D[] playerColliders;
	GameSession addCoins;

	private void Start()
	{
		player = GameObject.Find("Player");
		playerColliders = player.GetComponents<CapsuleCollider2D>();
		addCoins = FindObjectOfType<GameSession>();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		print("playerCollides");

		if (collider.gameObject.name == "Player" && collider == playerColliders[0])
		{
			Transform coinSoundInstance = Instantiate(coinPickupSound, transform.position, Quaternion.identity);
			coinSoundInstance.parent = transform.parent;

			addCoins.TrackCoinAmount(1);

			Destroy(gameObject);
		}
	}
}
