using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour {

	[SerializeField] Image firstStarUI;
	public GameObject firstStar;
	[SerializeField] Image secondStarUI;
	public GameObject secondStar;
	[SerializeField] Image thirdStarUI;
	public GameObject thirdStar;
	[SerializeField] Image fullStar;
	[SerializeField] Image emptyStar;

	private void Update()
	{
		firstStar = GameObject.Find("Star1");
		secondStar = GameObject.Find("Star2");
		thirdStar = GameObject.Find("Star3");

		print("starsfound: " + firstStar + " " + secondStar + " " + thirdStar);
		print("firststar: " + (firstStar == null));

		if (firstStar == null)
		{
			firstStarUI.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			firstStarUI.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (secondStar == null)
		{
			secondStarUI.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			secondStarUI.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}

		if (thirdStar == null)
		{
			thirdStarUI.GetComponent<Image>().sprite = fullStar.GetComponent<Image>().sprite;
		}
		else
		{
			thirdStarUI.GetComponent<Image>().sprite = emptyStar.GetComponent<Image>().sprite;
		}
	}
}
