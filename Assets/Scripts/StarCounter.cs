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
		if (firstStar != null && firstStar.transform.position != GameObject.Find("Star 1 Position").transform.position)
		{
			firstStar.transform.position = GameObject.Find("Star 1 Position").transform.position;
		}

		secondStar = GameObject.Find("Star2");
		if (secondStar != null && secondStar.transform.position != GameObject.Find("Star 2 Position").transform.position)
		{
			secondStar.transform.position = GameObject.Find("Star 2 Position").transform.position;
		}

		thirdStar = GameObject.Find("Star3");
		if (thirdStar != null && thirdStar.transform.position != GameObject.Find("Star 3 Position").transform.position)
		{
			thirdStar.transform.position = GameObject.Find("Star 3 Position").transform.position;
		}

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
