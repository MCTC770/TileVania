using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

	public bool wasSpawned = false;

	[SerializeField] float raycastLengthUp = 0.376f;
	[SerializeField] float raycastLengthDown = 0.376f;
	[SerializeField] float raycastLengthLeft = 0.376f;
	[SerializeField] float raycastLengthRight = 0.376f;
	[SerializeField] float adjustmentValue = 0.1f;
	[SerializeField] LayerMask layer;
	[SerializeField] CapsuleCollider2D playerCollider;

	bool grounded = false;
	LayerMask foregroundLayer = 9;
	Rigidbody2D playerPickupRigidbody;

	// Use this for initialization
	void Start () {
		playerPickupRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//transform.position = new Vector2(transform.position.x, transform.position.y + adjustmentValue);
		Raycasting("up");
		Raycasting("down");
		Raycasting("left");
		Raycasting("right");

		if (!grounded)
		{
			Physics2D.IgnoreCollision(playerCollider, GetComponent<CapsuleCollider2D>(), true);
		}
		else
		{
			Physics2D.IgnoreCollision(playerCollider, GetComponent<CapsuleCollider2D>(), true);
		}
	}

	private bool Raycasting(string castDirection)
	{
		Vector2 position = transform.position;
		Vector2 direction = new Vector2();
		float distance = direction.y;

		if (castDirection == "up")
		{
			direction = Vector2.up * raycastLengthUp;
			distance = direction.y;
			//print("up: " + distance);
		}
		else if (castDirection == "down")
		{
			direction = Vector2.down * raycastLengthDown;
			distance = -direction.y;
			//print("down: " + distance);
		}
		else if (castDirection == "left")
		{
			direction = Vector2.left * raycastLengthLeft;
			distance = -direction.x;
			//print("left: " + distance);
		}
		else if (castDirection == "right")
		{
			direction = Vector2.right * raycastLengthRight;
			distance = direction.x;
			//print("right: " + distance);
		}
		else
		{
			Debug.LogWarning("Invalid castDirection value");
		}

		Debug.DrawRay(position, direction, Color.green);

		if (Physics2D.Raycast(position, direction, distance, layer))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (collision.gameObject.layer == 9)
		{
			grounded = true;
		}

		/*if (collision.gameObject.layer == foregroundLayer && wasSpawned)
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + adjustmentValue);
			if (Raycasting("down"))
			{
				playerPickupRigidbody.velocity = Vector2.up * adjustmentValue;
			}
		}*/
	}

	/*private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.layer == foregroundLayer)
		{
			wasSpawned = false;
		}
	}*/
}
