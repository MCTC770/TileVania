using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Tutorial : MonoBehaviour {

	public bool firstSegmentPassed = false;
	public bool secondSegmentEnter = false;
	public bool secondSegmentExit = false;
	public bool tutorialEnd = false;

	[SerializeField] GameObject collectPlayerTutorial;
	[SerializeField] GameObject doubleJumpTutorial;
	[SerializeField] GameObject noPlayerTutorial;
	[SerializeField] GameObject collectMorePlayerTutorial;
	[SerializeField] GameObject multiJumpTutorial;

	PlayerMovement playerMovement;
	GameSession gameSession;
	bool hadThreePlayersOrMore = false;

	private void Start()
	{
		playerMovement = FindObjectOfType<PlayerMovement>();
		gameSession = FindObjectOfType<GameSession>();
		collectPlayerTutorial.SetActive(true);
	}

	private void Update()
	{
		CheckTutorialsInFirstSegment();
		CheckTutorialsInSecondSegment();
		CheckTutorialsInThirdSegment();
		TutorialsFinished();
	}

	private void CheckTutorialsInFirstSegment()
	{
		if (playerMovement.currentMaxJumps >= 2 && !firstSegmentPassed && gameSession.currentCheckpointNumber == 0)
		{
			collectPlayerTutorial.SetActive(false);
			doubleJumpTutorial.SetActive(true);
		}
		else if (playerMovement.currentMaxJumps < 2 && !firstSegmentPassed && gameSession.currentCheckpointNumber == 0)
		{
			collectPlayerTutorial.SetActive(true);
			doubleJumpTutorial.SetActive(false);
		}
		else if (firstSegmentPassed || gameSession.currentCheckpointNumber > 0)
		{
			Destroy(collectPlayerTutorial);
			Destroy(doubleJumpTutorial);
		}
	}

	private void CheckTutorialsInSecondSegment()
	{
		if (!secondSegmentEnter && noPlayerTutorial != null)
		{
			noPlayerTutorial.SetActive(false);
		}
		else if (secondSegmentEnter && !secondSegmentExit)
		{
			noPlayerTutorial.SetActive(true);
		}
		else if (secondSegmentExit)
		{
			Destroy(noPlayerTutorial);
			if (collectMorePlayerTutorial != null)
			{
				collectMorePlayerTutorial.SetActive(true);
			}
		}
	}

	private void CheckTutorialsInThirdSegment()
	{
		if (secondSegmentExit && playerMovement.currentMaxJumps >= 3 && !tutorialEnd)
		{
			collectMorePlayerTutorial.SetActive(false);
			multiJumpTutorial.SetActive(true);
			hadThreePlayersOrMore = true;
		}
		else if (secondSegmentExit && playerMovement.currentMaxJumps < 3 && !tutorialEnd)
		{
			collectMorePlayerTutorial.SetActive(true);
			multiJumpTutorial.SetActive(false);
		}
	}

	private void TutorialsFinished()
	{
		if (hadThreePlayersOrMore && tutorialEnd)
		{
			Destroy(collectMorePlayerTutorial);
			Destroy(multiJumpTutorial);
		}
	}
}
