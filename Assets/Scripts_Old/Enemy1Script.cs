using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1Script : MonoBehaviour {

	// Enemy 1 behavior - standard health, fire rate and movement speed

	public Rigidbody2D enemy1Rb;
	public Transform[] enemyBulletSpawnPoints;
	public Transform target;
	public GameObject enemyBulletObject;
	public SpriteRenderer spriteRenderer;
	public AudioClip enemyBulletSound;

	PlayerController playerController;

	public Color[] colors = new Color[2];

	public float enemy1Speed;
	public int enemy1Health = 200;
	public float step;

	private float instantiateTimer = 1f;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		playerController = FindObjectOfType<PlayerController> ();
	}

	void Update()
	{
		MoveEnemy1 ();
		CreatePrefab ();
		KillEnemy ();
	}

	void MoveEnemy1()
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
			enemy1Health -= 25;
			StartCoroutine("FlashEnemy1");
		}

		if(other.gameObject.CompareTag("KillPlane"))
		{
			Destroy (gameObject);
		}
	}

	void PlaySound(AudioClip clip, float volMultiplier)
	{
		AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(volMultiplier, 0.05f, 1f));
	}

	void CreatePrefab()
	{
		instantiateTimer -= Time.deltaTime;

		if(instantiateTimer <= 0)
		{
			Instantiate (enemyBulletObject, enemyBulletSpawnPoints[0].transform.position, Quaternion.identity);
			Instantiate (enemyBulletObject, enemyBulletSpawnPoints[1].transform.position, Quaternion.identity);
			instantiateTimer = 1f;
			PlaySound (enemyBulletSound, 4f);

		}
	}

	void KillEnemy()
	{
		if(enemy1Health <= 0)
		{
			Destroy (gameObject);
			//playerController.score += 50;
			//playerController.eHit += 1;
		}
	}

	IEnumerator FlashEnemy1()
	{
		spriteRenderer.material.color = colors [0];
		yield return new WaitForSeconds (0.1f);
		spriteRenderer.material.color = colors [1];
	}
}
