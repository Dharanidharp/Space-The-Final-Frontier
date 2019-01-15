using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour {

	[SerializeField] private int health = 300;
	[SerializeField] private float speed;
	[SerializeField] private Rigidbody2D enemyCRb;
	[SerializeField] private GameObject enemyBullet;
	[SerializeField] private Transform[] bulletSpawnPoints;
	[SerializeField] private Transform enemyCTarget;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private AudioClip bulletSound;
	[SerializeField] private float timer = 0.8f;

	private RPlayerController rPlayerController;
	private GameplayManager gpManager;

	// Use this for initialization
	void Start () 
	{
		enemyCRb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		enemyCTarget = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		rPlayerController = FindObjectOfType<RPlayerController> ();
		gpManager = FindObjectOfType<GameplayManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move ();

		timer -= Time.deltaTime;

		if(timer <= 0)
		{
			gpManager.ShootBullets (enemyBullet, bulletSpawnPoints, bulletSound);
			timer = 0.8f;
		}

        Die();
	}

	public void Move()
	{
		if (enemyCTarget != null) 
		{
			transform.position = Vector2.MoveTowards (transform.position, enemyCTarget.position, speed * Time.deltaTime);
		} else 
		{
			return;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.gameObject.tag;

		if(tag == "Player")
		{
			health = 0;
		}

        else if (tag == "empCollider")
        {
            health = 0;
        }

        else if(tag == "KillPlane")
		{
			Destroy (gameObject);
		}

		else if(tag == "Bullet")
		{
			health -= 10;
			StartCoroutine (gpManager.FlashSprite (spriteRenderer, 0.1f));
		}
	}

	public void Die()
	{
		if(health <= 0)
		{
			Destroy (gameObject);
			rPlayerController.setScore (150);
			rPlayerController.setShipsDestroyed (1);
		}
	}
}
