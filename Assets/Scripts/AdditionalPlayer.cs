using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalPlayer : MonoBehaviour {

	public Collider2D additionalPlayerCollided;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		additionalPlayerCollided = collision.otherCollider;
	}
}
