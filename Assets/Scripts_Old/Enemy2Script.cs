using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : MonoBehaviour {

	public Rigidbody2D enemy2Rb;
	public Transform enemyMissileSpawnPoint;
	public GameObject enemyMissileObject;
	public SpriteRenderer spriteRenderer;
	public AudioClip missileShotSound;

	PlayerController playerController;

	public Color[] colors = new Color[2];

	public float enemy2Speed;
	public int enemy2Health = 100;
	public float step;

	private float instantiateTimer = 0.5f;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		playerController = FindObjectOfType<PlayerController> ();
	}

	void Update()
	{
		MoveEnemy2 ();
		CreatePrefab ();
		KillEnemy ();
	}

	void MoveEnemy2()
	{
		enemy2Rb.AddForce (Vector2.down * enemy2Speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Destroy (gameObject);
		}

		if(other.gameObject.CompareTag("Bullet"))
		{
			enemy2Health -= 15;
			StartCoroutine("FlashEnemy2");
		}

		if(other.gameObject.CompareTag("KillPlane"))
		{
			Destroy (gameObject);
		}
	}

	void CreatePrefab()
	{
		instantiateTimer -= Time.deltaTime;

		if(instantiateTimer <= 0)
		{
			Instantiate (enemyMissileObject, enemyMissileSpawnPoint.transform.position, Quaternion.identity);
			PlaySound (missileShotSound, 4.0f);
			instantiateTimer = 0.5f;

		}
	}

	void PlaySound(AudioClip clip, float volMultiplier)
	{
		AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(volMultiplier, 0.05f, 1f));
	}

	void KillEnemy()
	{
		if(enemy2Health <= 0)
		{
			Destroy (gameObject);
			//playerController.score += 100;
			//playerController.eHit += 1;
		}
	}

	IEnumerator FlashEnemy2()
	{
		spriteRenderer.material.color = colors [0];
		yield return new WaitForSeconds (0.1f);
		spriteRenderer.material.color = colors [1];
	}
}
