using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

	[SerializeField] PlayerDeath playerDeath;
	[SerializeField] ParticleSystem enemyParticleSystem;
	bool enemyDefeated = false;
	bool jumpedOnEnemy;
	int bumpUplift = 1150;
	float bumpUpliftDuration = 0.25f;
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

		if (jumpedOnEnemy)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, bumpUplift * Time.deltaTime);
			Invoke("EnemyBumpUpliftOff", bumpUpliftDuration);
		}
	}

	void EnemyBumpUpliftOff()
	{
		jumpedOnEnemy = false;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == 13 && !collider.isTrigger)
		{
			Destroy(collider.gameObject);
			Instantiate(enemyParticleSystem, collider.gameObject.transform.position, Quaternion.identity);
			enemyDefeated = true;
			jumpedOnEnemy = true;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.collider.gameObject.layer == 13 && !enemyDefeated) || collision.collider.gameObject.layer == 14)
		{
			StartCoroutine(CheckEnemyStillExists(collision.collider.gameObject));
		}

		enemyDefeated = false;
	}

	IEnumerator CheckEnemyStillExists (GameObject enemy)
	{
		yield return new WaitForSeconds(0.1f);
		if (enemy != null)
		{
			Instantiate(playerDeath, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
