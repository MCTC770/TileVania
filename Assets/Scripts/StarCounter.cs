using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour {

	public int starsCollected = 0;
	public int starsTotal = 0;
	[SerializeField] Text starText;
	[SerializeField] Image firstStarUI;
	GameObject firstStar;
	[SerializeField] Image secondStarUI;
	GameObject secondStar;
	[SerializeField] Image thirdStarUI;
	GameObject thirdStar;
	[SerializeField] Image fullStar;
	GameSession currentGameSession;

	void Start ()
	{
		firstStar = GameObject.Find("Star 1");
		secondStar = GameObject.Find("Star 2");
		thirdStar = GameObject.Find("Star 3");
		starsTotal = FindObjectsOfType<StarPickup>().Length;
		currentGameSession = FindObjectOfType<GameSession>();
	}

	private void Update()
	{
		print(firstStar);
		if (firstStar == null)
		{
			firstStarUI.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		if (secondStar == null)
		{
			secondStarUI.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		if (thirdStar == null)
		{
			thirdStarUI.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		starsCollected = currentGameSession.starsCollected;
		starText.text = starsCollected.ToString() + "/" + "3";
	}
}
