using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBulletScript : MonoBehaviour {

	[Header("Bullet Rigidbody Stats")]
	[SerializeField] private Rigidbody2D bulletRb;
	[SerializeField] private float bulletSpeed;
	private RPlayerController rPlayerController;

	// Use this for initialization
	void Start () 
	{
		bulletRb = GetComponent<Rigidbody2D> ();
		rPlayerController = FindObjectOfType<RPlayerController> ();
	}

	void FixedUpdate () 
	{
		MoveBullet (bulletSpeed);
	}

	public void MoveBullet(float speed)
	{
		bulletRb.AddForce (Vector2.up * speed * Time.deltaTime);
	}

    // Collider detection
	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.gameObject.tag;

		if(tag == "Enemy1" || tag == "Enemy2" || tag == "Enemy3" || tag == "Spawner")
		{
			Destroy (gameObject);
		}

		else if(tag == "Asteroid")
		{
			rPlayerController.setScore (10);
			rPlayerController.setAsteroidsDestroyed (1);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}

		else if(tag == "Missile")
		{
			rPlayerController.setScore (5);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
