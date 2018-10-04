using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Tutorial : MonoBehaviour {

	[SerializeField] GameObject walkTutorial;
	[SerializeField] GameObject jumpTutorial;
	[SerializeField] GameObject runTutorial;
	[SerializeField] GameObject longJumpTutorial;
	[SerializeField] BoxCollider2D runTutorialCollider;

	bool runTutorialTriggered = false;

	// Use this for initialization
	void Start () {
		walkTutorial.SetActive(true);
		jumpTutorial.SetActive(true);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 11 && runTutorialTriggered == true && runTutorialCollider == null)
		{
			runTutorial.SetActive(false);
			longJumpTutorial.SetActive(false);
		}
		else if (collision.gameObject.layer == 11 && runTutorialTriggered == false)
		{
			Destroy(runTutorialCollider);
			walkTutorial.SetActive(false);
			jumpTutorial.SetActive(false);
			runTutorial.SetActive(true);
			longJumpTutorial.SetActive(true);

			runTutorialTriggered = true;
		}
	}
}
