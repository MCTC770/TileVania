using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsFinalResult : MonoBehaviour {

	GameSession gameSession;

	[SerializeField] Image star11;
	[SerializeField] Image star12;
	[SerializeField] Image star13;
	[SerializeField] Image star21;
	[SerializeField] Image star22;
	[SerializeField] Image star23;
	[SerializeField] Image star31;
	[SerializeField] Image star32;
	[SerializeField] Image star33;
	[SerializeField] Image fullStar;
	[SerializeField] Image emptyStar;
	[SerializeField] Text totalStarAmount;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		gameSession = FindObjectOfType<GameSession>();

		print("gs: " + gameSession + " " + gameSession.starsTracking[0][0]);

		if (gameSession.starsTracking[0][0] == 1)
		{
			star11.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star11.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (gameSession.starsTracking[0][1] == 1)
		{
			star12.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star12.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (gameSession.starsTracking[0][2] == 1)
		{
			star13.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star13.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (gameSession.starsTracking[1][0] == 1)
		{
			star21.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star21.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (gameSession.starsTracking[1][1] == 1)
		{
			star22.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star22.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (gameSession.starsTracking[1][2] == 1)
		{
			star23.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star23.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}


		if (gameSession.starsTracking[2][0] == 1)
		{
			star31.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star31.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (gameSession.starsTracking[2][1] == 1)
		{
			star32.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star32.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (gameSession.starsTracking[2][2] == 1)
		{
			star33.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			star33.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		totalStarAmount.text = gameSession.starsFromAllLevels.ToString();
	}
}
