using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "Player")
		{
			Destroy(gameObject);
		}
	}
}
