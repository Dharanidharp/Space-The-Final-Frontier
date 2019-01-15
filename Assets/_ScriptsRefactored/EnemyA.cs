using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour {

	[Header("Enemy A")]
	[SerializeField] private int health = 200;
	[SerializeField] private float speed;
	[SerializeField] private Rigidbody2D enemyARb;
	[SerializeField] private GameObject enemyBullet;
	[SerializeField] private Transform[] enemyABulletSpawnPoints;
	[SerializeField] private Transform enemyATarget;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private AudioClip bulletSound;
	[SerializeField] private float timer = 1.0f;

	private RPlayerController rPLayerController;
	private GameplayManager gpManager;

	// Use this for initialization
	void Start () 
	{
		enemyARb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		enemyATarget = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		rPLayerController = FindObjectOfType<RPlayerController> ();
		gpManager = FindObjectOfType<GameplayManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move ();

		timer -= Time.deltaTime;

		if(timer <= 0)
		{
			gpManager.ShootBullets (enemyBullet, enemyABulletSpawnPoints, bulletSound);
			timer = 1.0f;
		}

        Die();
	}

	public void Move()
	{
		if (enemyATarget != null) 
		{
			transform.position = Vector2.MoveTowards (transform.position, enemyATarget.position, speed * Time.deltaTime);
		} else
		{
			return;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.gameObject.tag;

        if (tag == "Player")
        {
            health = 0;
        }

        else if (tag == "empCollider")
        {
            health = 0;
        }

        else if (tag == "KillPlane")
        {
            Destroy(gameObject);
        }

        else if (tag == "Bullet")
        {
            health -= 25;
            StartCoroutine(gpManager.FlashSprite(spriteRenderer, 0.1f));
        }
	}

	public void Die()
	{
		if(health <= 0)
		{
			Destroy (gameObject);
			rPLayerController.setScore (50);
			rPLayerController.setShipsDestroyed (1);
		}
	}
}
