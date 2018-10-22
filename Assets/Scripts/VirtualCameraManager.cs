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
		if (player.poi1CameraActive)
		{
			for (var i = 0; i <= (virtualCameras.Length - 1); i++)
			{
				virtualCameras[i].SetActive(false);
			}
			virtualCameras[10].SetActive(true);
		}
		else if (player.poi2CameraActive)
		{
			for (var i = 0; i <= (virtualCameras.Length - 1); i++)
			{
				virtualCameras[i].SetActive(false);
			}
			virtualCameras[11].SetActive(true);
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
