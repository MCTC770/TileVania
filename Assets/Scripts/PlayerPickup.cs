using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

	[SerializeField] PlayerDeath playerDeath;
	bool enemyDefeated = false;
	PlayerMovement player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerMovement>();
	}

	private void Update()
	{
		if(player.grounded)
		{
			gameObject.layer = 17;
		}
		else
		{
			gameObject.layer = 18;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == 13 && !collider.isTrigger)
		{
			Destroy(collider.gameObject);
			enemyDefeated = true;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.collider.gameObject.layer == 13 && !enemyDefeated) || collision.collider.gameObject.layer == 14)
		{
			Instantiate(playerDeath, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
			Destroy(gameObject);
		}

		enemyDefeated = false;
	}

}
