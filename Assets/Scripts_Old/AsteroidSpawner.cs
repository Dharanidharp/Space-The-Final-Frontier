using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

	public Transform[] spawnPoints;

	public GameObject asteroidObject;

	private float instantiateTimer = 0.8f;

	void Update()
	{
		CreatePrefab ();
	}

	void CreatePrefab()
	{
		instantiateTimer -= Time.deltaTime;

		if(instantiateTimer <= 0)
		{
			Instantiate (asteroidObject, spawnPoints [Random.Range (0, 3)].transform.position, Quaternion.identity);
			instantiateTimer = 0.8f;
		}
	}
}
