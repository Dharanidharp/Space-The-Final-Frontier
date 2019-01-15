using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	Rigidbody2D playerRb;

	public GameObject bullet;
	public GameObject laser;
	public SpriteRenderer spriteRenderer;
	public Color[] colors = new Color[2];

	public Transform[] bulletSpawnPoints;
	public Transform laserPoint;

	public AudioClip bulletShotSound;
	public AudioClip laserShotSound;

    public Image healthImage;

	Text hText;
	Text sText;
	Text aText;
	Text eText;

	LevelManager levelManager;

	public float speed;
	public float health = 1000;
    //public float startHealth;
	public int score = 0;
	public int aHit = 0;
	public int eHit = 0;

	bool isAlive = true;

	void Start()
	{
		playerRb = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();

		spriteRenderer = GetComponent<SpriteRenderer> ();
		levelManager = FindObjectOfType<LevelManager> ();

		hText = GameObject.FindGameObjectWithTag ("Hnum").GetComponent<Text> ();
		sText = GameObject.FindGameObjectWithTag ("Snum").GetComponent<Text> ();
		aText = GameObject.FindGameObjectWithTag ("Anum").GetComponent<Text> ();
		eText = GameObject.FindGameObjectWithTag ("Enum").GetComponent<Text> ();

        //health = startHealth;

	}

	void Update()
	{
		MoveRight ();
		MoveLeft ();
		MoveUp ();
		MoveDown ();
		UpdateHUD ();
		ShootBullets ();
		ShootLaser ();
		ShootMissiles ();
		Die ();
	}

	void UpdateHUD()
	{
		hText.text = health.ToString ();
		sText.text = score.ToString ();
		aText.text = aHit.ToString ();
		eText.text = eHit.ToString ();

        healthImage.fillAmount = health / 1000f;
	}

	void MoveRight()
	{
		if(Input.GetKey(KeyCode.D))
		{
			playerRb.AddRelativeForce ((Vector2.right * speed) * Time.deltaTime, ForceMode2D.Impulse);
		}
		playerRb.AddRelativeForce ((Vector2.right * speed) * Time.deltaTime, ForceMode2D.Impulse);
	}

	void MoveLeft()
	{
		if(Input.GetKey(KeyCode.A))
		{
			playerRb.AddRelativeForce ((Vector2.left * speed) * Time.deltaTime, ForceMode2D.Impulse);
		}
		playerRb.AddRelativeForce ((Vector2.left * speed) * Time.deltaTime, ForceMode2D.Impulse);
	}

	void MoveDown()
	{
		if(Input.GetKey(KeyCode.S))
		{
			playerRb.AddRelativeForce ((Vector2.down * speed) * Time.deltaTime, ForceMode2D.Impulse);
		}
		playerRb.AddRelativeForce ((Vector2.down * speed) * Time.deltaTime, ForceMode2D.Impulse);
	}

	void MoveUp()
	{
		if(Input.GetKey(KeyCode.W))
		{
			playerRb.AddRelativeForce ((Vector2.up * speed) * Time.deltaTime, ForceMode2D.Impulse);
		}
		playerRb.AddRelativeForce ((Vector2.up * speed) * Time.deltaTime, ForceMode2D.Impulse);
	}

	void ShootBullets()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Instantiate (bullet, bulletSpawnPoints[0].transform.position, Quaternion.identity);
			Instantiate (bullet, bulletSpawnPoints[1].transform.position, Quaternion.identity);
			PlaySound (bulletShotSound, 2.0f);
		}
	}

	void ShootLaser()
	{
		if(Input.GetMouseButtonDown(1))
		{
			Instantiate (laser, laserPoint.transform.position, Quaternion.identity);
			PlaySound (laserShotSound, 5.0f);
		}
	}

	void ShootMissiles()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Enemy2") || other.gameObject.CompareTag("Enemy3"))
		{
			health -= 20;
			StartCoroutine ("FlashPlayer");
		}

		if(other.gameObject.CompareTag("Missile"))
		{
			health -= 60;
			StartCoroutine ("FlashPlayer");
		}

		if(other.gameObject.CompareTag("Asteroid"))
		{
			health -= 5;
			StartCoroutine ("FlashPlayer");
		}

		if(other.gameObject.CompareTag("EnemyBullet"))
		{
			health -= 10;
			StartCoroutine ("FlashPlayer");
		}
	}

	void Die()
	{
		if(health <= 0)
		{
			isAlive = false;
			Destroy (gameObject);
			levelManager.LoadLevel ("GameOver");
		}
	}

	IEnumerator FlashPlayer()
	{
		spriteRenderer.material.color = colors [0];
		yield return new WaitForSeconds (0.1f);
		spriteRenderer.material.color = colors [1];
	}

	void PlaySound(AudioClip clip, float volMultiplier)
	{
		AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(volMultiplier, 0.05f, 1f));
	}
}
