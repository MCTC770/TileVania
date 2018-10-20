using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLevel2TutorialCollider : MonoBehaviour {

	Level2Tutorial level2Tutorial;
	GameSession gameSession;

	// Use this for initialization
	void Start () {
		level2Tutorial = FindObjectOfType<Level2Tutorial>();
		gameSession = FindObjectOfType<GameSession>();

		if (gameObject.name == "DoubleJumpCollider" && gameSession.currentCheckpointNumber > 0)
		{
			level2Tutorial.firstSegmentPassed = true;
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (gameObject.name == "DoubleJumpCollider")
		{
			level2Tutorial.firstSegmentPassed = true;
			Destroy(gameObject);
		}
		else if (gameObject.name == "NoPlayerCollectColliderEnter")
		{
			level2Tutorial.secondSegmentEnter = true;
		}
		else if (gameObject.name == "NoPlayerCollectColliderExit")
		{
			level2Tutorial.secondSegmentExit = true;
			Destroy(gameObject);
		}
		else if (gameObject.name == "TutorialEnd")
		{
			level2Tutorial.tutorialEnd = true;
			Destroy(gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (gameObject.name == "NoPlayerCollectColliderEnter")
		{
			level2Tutorial.secondSegmentEnter = false;
		}
	}
}
