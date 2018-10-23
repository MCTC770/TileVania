using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalPlayer : MonoBehaviour {

	public Collider2D additionalPlayerCollided;
	public Collider2D additionalPlayerCollidedWith;
	public Collider2D additionalPlayerTriggeredWith;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		additionalPlayerCollided = collision.otherCollider;
		additionalPlayerCollidedWith = collision.collider;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		additionalPlayerTriggeredWith = collision;

		if (collision.name == "Ladders")
		{
			GetComponent<CapsuleCollider2D>().enabled = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.name == "Ladders")
		{
			GetComponent<CapsuleCollider2D>().enabled = true;
		}
	}
}
