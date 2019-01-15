using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour 
{
	[SerializeField] private Rigidbody2D asteroidRb;
	[SerializeField] private float asteroidSpeed;
	[SerializeField] private float rotateSpeed;

	void FixedUpdate()
	{
		MoveAsteroid ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.gameObject.tag;

		if(tag == "Player" || tag == "empCollider")
		{
			Destroy (gameObject);
		}
	}

	public void MoveAsteroid()
	{
		asteroidRb.velocity = new Vector2 (0, -asteroidSpeed * Time.deltaTime);
		transform.Rotate (0, 0, rotateSpeed * Time.deltaTime);
	}
}
