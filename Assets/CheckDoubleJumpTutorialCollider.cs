using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDoubleJumpTutorialCollider : MonoBehaviour {

	Level2Tutorial level2Tutorial;

	// Use this for initialization
	void Start () {
		level2Tutorial = FindObjectOfType<Level2Tutorial>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		level2Tutorial.firstSegmentPassed = true;
		Destroy(gameObject);
	}
}
