using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (collision.gameObject.layer == 9)
		{
			gameObject.layer = 17;
		}
	}
}
