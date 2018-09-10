using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] float moveSpeed = 50f;

	Rigidbody2D enemyRigidbody;
	BoxCollider2D checkFallCollider;
	Vector2 enemyMoveSpeed;

	// Use this for initialization
	void Start () {
		enemyRigidbody = GetComponent<Rigidbody2D>();
		checkFallCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		enemyMoveSpeed = enemyRigidbody.velocity = Vector2.right * moveSpeed * Time.deltaTime;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.name == "Foreground")
		{
			moveSpeed *= -1;

			if (moveSpeed > 0)
			{
				transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
			}
			else if (moveSpeed < 0)
			{
				transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
			}
		}
		//print(collision);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		print(other);
	}
}
