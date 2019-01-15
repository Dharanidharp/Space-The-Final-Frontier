using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour {

	[Header("Enemy B")]
	[SerializeField] private int health = 100;
	[SerializeField] private float speed;
	[SerializeField] private Rigidbody2D enemyBRb;
	[SerializeField] private GameObject missile;
	[SerializeField] private Transform[] missileSpawnPoints;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private AudioClip missileSound;
	[SerializeField] private float timer = 0.5f;

	private RPlayerController rPlayerController;
	private GameplayManager gpManager;

	// Use this for initialization
	void Start () 
	{
		enemyBRb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
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
			gpManager.ShootBullets (missile, missileSpawnPoints, missileSound);
			timer = 0.5f;
		}

        Die();
	}

	public void Move()
	{
		enemyBRb.AddForce (Vector2.down * speed * Time.deltaTime);
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
			health -= 15;
			StartCoroutine (gpManager.FlashSprite (spriteRenderer, 0.1f));
		}
	}

	public void Die()
	{
		if(health <= 0)
		{
			Destroy (gameObject);
			rPlayerController.setScore (100);
			rPlayerController.setShipsDestroyed (1);
		}
	}
}
