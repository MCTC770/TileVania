using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

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
}
