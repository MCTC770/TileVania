using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour {

	[SerializeField] Image firstStarUI;
	GameObject firstStar;
	[SerializeField] Image secondStarUI;
	GameObject secondStar;
	[SerializeField] Image thirdStarUI;
	GameObject thirdStar;
	[SerializeField] Image fullStar;

	private void Update()
	{
		firstStar = GameObject.Find("Star1");
		secondStar = GameObject.Find("Star2");
		thirdStar = GameObject.Find("Star3");

		print(firstStar + " " + secondStar + " " + thirdStar);
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
	}
}
