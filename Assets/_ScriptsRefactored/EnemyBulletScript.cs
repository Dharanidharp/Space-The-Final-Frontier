using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {

    // Instance variables
	[SerializeField] private Rigidbody2D bulletRb;
	[SerializeField] private float bulletSpeed;


	void FixedUpdate()
	{
		MoveBullet ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        // Store the tag name of the colliding game object
		string tag = other.gameObject.tag;

		if (tag == "Player" || tag == "KillPlane" || tag == "empCollider") 
		{
			Destroy (gameObject);
		}
	}

    // Add downward force on the rigidbody of the bullet
	void MoveBullet()
	{
		bulletRb.AddForce (Vector2.down * bulletSpeed * Time.deltaTime);
	}
}
