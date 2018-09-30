using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour {

	public int starsCollected = 0;
	public int starsTotal;
	[SerializeField] Text starText;
	[SerializeField] GameSession currentGameSession;

	void Start ()
	{
		starsTotal = FindObjectsOfType<StarPickup>().Length;
		currentGameSession = FindObjectOfType<GameSession>();
	}

	private void Update()
	{
		starsCollected = currentGameSession.starsCollected;
		starText.text = starsCollected.ToString() + "/" + starsTotal.ToString();
	}
}
