using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Script : MonoBehaviour {

	public Rigidbody2D enemy3Rb;
	public Transform enemyBulletSpawnPoint;
	public Transform target;
	public GameObject enemyBulletObject;
	public SpriteRenderer spriteRenderer;
	public AudioClip enemyBulletSound;

	PlayerController playerController;

	public Color[] colors = new Color[2];

	public float enemy3Speed;
	public int enemy3Health = 250;
	public float step;

	private float instantiateTimer = 0.5f;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		playerController = FindObjectOfType<PlayerController> ();
	}

	void Update()
	{
		MoveEnemy3 ();
		CreatePrefab ();
		KillEnemy ();
	}

	void MoveEnemy3()
	{
		transform.position = Vector3.MoveTowards (transform.position, target.position, step * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Destroy (gameObject);
		}

		if(other.gameObject.CompareTag("Bullet"))
		{
			enemy3Health -= 10;
			StartCoroutine("FlashEnemy3");
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
			Instantiate (enemyBulletObject, enemyBulletSpawnPoint.transform.position, Quaternion.identity);
			instantiateTimer = 0.5f;
			PlaySound (enemyBulletSound, 4f);
		}
	}

	void PlaySound(AudioClip clip, float volMultiplier)
	{
		AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(volMultiplier, 0.05f, 1f));
	}

	void KillEnemy()
	{
		if(enemy3Health <= 0)
		{
			Destroy (gameObject);
			//playerController.score += 150;
			//playerController.eHit += 1;
		}
	}

	IEnumerator FlashEnemy3()
	{
		spriteRenderer.material.color = colors [0];
		yield return new WaitForSeconds (0.1f);
		spriteRenderer.material.color = colors [1];
	}
}
