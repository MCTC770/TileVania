﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	[SerializeField] Transform coinPickupSound;
	GameObject player;
	CapsuleCollider2D[] playerColliders;
	GameSession addCoins;
	LayerMask playerLayer = 11;

	private void Start()
	{
		player = GameObject.Find("Player");
		playerColliders = player.GetComponents<CapsuleCollider2D>();
		addCoins = FindObjectOfType<GameSession>();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == playerLayer && !collider.isTrigger)
		{
			Transform coinSoundInstance = Instantiate(coinPickupSound, transform.position, Quaternion.identity);
			coinSoundInstance.parent = transform.parent;

			addCoins.TrackCoinAmount(1);

			Destroy(gameObject);
		}
	}
}
