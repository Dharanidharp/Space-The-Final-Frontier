using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	// Player primary weapon bullet script

	Rigidbody2D bullet;
	public float bulletSpeed;
	int score = 0;
	RPlayerController playerController;

	void Start()
	{
		bullet = GetComponent<Rigidbody2D> ();
		playerController = FindObjectOfType<RPlayerController> ();
	}

	void Update()
	{
		MoveBullet ();
	}

	void MoveBullet()
	{
		// moves the bullet upwards from the spawn point
		bullet.AddForce (Vector3.up * bulletSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Spawner"))
		{
			// destroy the bullet when reached top
			Destroy (gameObject);
		}

		if(other.gameObject.CompareTag("Asteroid"))
		{
			// destroy the asteroid when hit
			Destroy (other.gameObject);
			// destroy this bullet object when hit with asteroid
			Destroy (gameObject);
			// add to player score
			score += 10;
			playerController.setScore (score);
			// add to total number of asteroids hit 
			//playerController.aHit += 1;
		}

		if(other.gameObject.CompareTag("Enemy1"))
		{
			// destroy this bullet object when hit with enemy 1
			Destroy (gameObject);
		}

		if(other.gameObject.CompareTag("Enemy2"))
		{
			// destroy this bullet object when hit with enemy 2
			Destroy (gameObject);
		}

		if(other.gameObject.CompareTag("Enemy3"))
		{
			// destroy this bullet object when hit with enemy 3
			Destroy (gameObject);
		}

		if(other.gameObject.CompareTag("Missile"))
		{
			// destroy this bullet object when hit with enemy 2's missile
			Destroy (gameObject);
			// destroy the missile when hit
			Destroy (other.gameObject);
			// add to player score
			playerController.setScore (score);
		}
	}

}
