using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileScript : MonoBehaviour {

    // Instance variables
	[SerializeField] private Rigidbody2D missileRb;
	[SerializeField] private Transform target;
	[SerializeField] private float missileSpeed;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();	
	}

	void FixedUpdate()
	{
		MoveMissile ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.gameObject.tag;

		if(tag == "Player" || tag == "KillPlane" || tag == "empCollider")
		{
			Destroy (gameObject);
		}
	}

	public void MoveMissile()
	{
		if (target != null) 
		{
			transform.position = Vector3.MoveTowards (transform.position, target.position, missileSpeed * Time.deltaTime);
		} else 
		{
			return;
		}
	}
}
