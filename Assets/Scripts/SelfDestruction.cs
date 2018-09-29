using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour {

	AudioSource thisAudioSource;

	void Start () {
		thisAudioSource = GetComponent<AudioSource>();
		Destroy(gameObject, thisAudioSource.clip.length);
	}
}
