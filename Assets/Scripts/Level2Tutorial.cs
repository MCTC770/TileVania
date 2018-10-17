using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tutorial : MonoBehaviour {

	public bool firstSegmentPassed = false;

	[SerializeField] GameObject collectPlayerTutorial;
	[SerializeField] GameObject doubleJumpTutorial;
	[SerializeField] GameObject noPlayerTutorial;
	[SerializeField] GameObject collectMorePlayerTutorial;
	[SerializeField] GameObject multiJumpTutorial;

	PlayerMovement playerMovement;

	private void Start()
	{
		playerMovement = FindObjectOfType<PlayerMovement>();
		collectPlayerTutorial.SetActive(true);
	}

	private void Update()
	{
		if (playerMovement.currentMaxJumps >= 2 && !firstSegmentPassed)
		{
			collectPlayerTutorial.SetActive(false);
			doubleJumpTutorial.SetActive(true);
		} else if (playerMovement.currentMaxJumps < 2 && !firstSegmentPassed)
		{
			collectPlayerTutorial.SetActive(true);
			doubleJumpTutorial.SetActive(false);
		}
		else
		{
			collectPlayerTutorial.SetActive(false);
			doubleJumpTutorial.SetActive(false);
		}
	}

	/*void Start () {
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
	}*/
}
