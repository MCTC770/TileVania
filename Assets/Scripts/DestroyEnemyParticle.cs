using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyParticle : MonoBehaviour {

	float particleLifeTime;

	// Use this for initialization
	void Start () {
		particleLifeTime = GetComponent<ParticleSystem>().main.duration;
		StartCoroutine(SelfDestruct(particleLifeTime));
	}
	
	IEnumerator SelfDestruct(float particleLifeTime)
	{
		yield return new WaitForSeconds(particleLifeTime);
		Destroy(gameObject);
	}
}
