using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraManager : MonoBehaviour {

	[SerializeField] GameObject[] virtualCameras;
	PlayerMovement player;

	private void Start()
	{
		player = FindObjectOfType<PlayerMovement>();
	}

	public void ChooseCamera()
	{
		if (player.poiCameraActive)
		{
			virtualCameras[10].SetActive(true);
			for (var i = 0; i <= (virtualCameras.Length - 2); i++)
			{
				virtualCameras[i].SetActive(false);
			}
		}
		else
		{
			for (var i = 0; i <= (virtualCameras.Length - 1); i++)
			{
				if (i == (player.maxJumps - 1))
				{
					virtualCameras[i].SetActive(true);
				}
				else
				{
					virtualCameras[i].SetActive(false);
				}
			}
		}
	}
}
