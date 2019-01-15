using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPlayerController : MonoBehaviour {

	[Header("Player Stats")]
	[SerializeField] private float health = 0;
	[SerializeField] private int score = 0;
	[SerializeField] private int xp = 0;
	[SerializeField] private int shipLevel = 1;
	[SerializeField] private string shipRank = "Rookie";
	[SerializeField] private int enemiesDestroyed = 0;
	[SerializeField] private int asteroidsDestroyed = 0;
	[SerializeField] private bool isAlive = true;

	[Header("Player Shield Stats")]
	[SerializeField] private float shield = 0;
	[SerializeField] private bool isShieldActive = false;
	[SerializeField] private float shieldDownRate = 0;
	[SerializeField] private float shieldUpRate = 0;
	[SerializeField] private bool isShieldBarFull = true;

	[Header("Player EMP Stats")]
	[SerializeField] private float emp = 0;
	[SerializeField] private bool isEMPActive = false;
	[SerializeField] private float empUpRate = 0;
	[SerializeField] private float empDownRate = 0;
	[SerializeField] private bool isEmpBarFull = true;

	[Header("Player Rigidbody Stats")]
	[SerializeField] private float speed;
	[SerializeField] private Rigidbody2D playerRb;
	private Vector2 velocity = Vector2.zero;
	private float hAxis;
	private float vAxis;

	private RBulletScript rBulletScript;
	private GameplayManager gameManager;

	[Header("Player Weapons")]
	[SerializeField] private ParticleSystem shieldPS;
	[SerializeField] private ParticleSystem empPS;
	[SerializeField] private GameObject bullet;
	[SerializeField] private Transform[] bulletSpawnPoints;
	[SerializeField] private Image playerHealthImage;
	[SerializeField] private float timer = 0.2f;

	[Header("Player Audio")]
	[SerializeField] private AudioClip bulletShotSound;
	[SerializeField] private AudioClip laserShotSound;

	private SpriteRenderer playerSpriteRenderer;
    private CircleCollider2D empCollider;

	// Use this for initialization
	void Start () 
	{
		health = 1000.0f;
		shield = 100.0f;
		shieldDownRate = 10.0f;
		shieldUpRate = 20.0f;
		emp = 100.0f;
		empDownRate = 25.0f;
		empUpRate = 10.0f;

		playerRb = GetComponent<Rigidbody2D> ();
		shieldPS = GameObject.FindGameObjectWithTag ("shieldPS").GetComponent<ParticleSystem> ();
		empPS = GameObject.FindGameObjectWithTag ("EMP").GetComponent<ParticleSystem> ();
		playerSpriteRenderer = GetComponent<SpriteRenderer> ();
        empCollider = GameObject.FindGameObjectWithTag("empCollider").GetComponent<CircleCollider2D>();
		gameManager = FindObjectOfType<GameplayManager> ();
	}

	// Update is called once per frame
	void Update()
	{
		hAxis = Input.GetAxisRaw ("Horizontal");
		vAxis = Input.GetAxisRaw ("Vertical");

		PlayerMovement ();

		timer -= Time.deltaTime;

		if(timer <= 0)
		{
			gameManager.ShootBullets (bullet, bulletSpawnPoints, bulletShotSound);
			timer = 0.2f;
		}

		Die ();

		PowerUpShield ();
		ShootEMP ();
	}

	public void PlayerMovement()
	{
		Vector2 hMovement = transform.right * hAxis;
		Vector2 vMovement = transform.up * vAxis;

		Vector2 velocity = (hMovement + vMovement).normalized * speed;

		if(velocity != Vector2.zero)
		{
			playerRb.MovePosition (playerRb.position + velocity * Time.fixedDeltaTime);
		}
	}

	public void PowerUpShield()
	{
		if(Input.GetKeyDown(KeyCode.LeftShift) && !isShieldActive && isShieldBarFull)
		{
			shieldPS.Play ();
			isShieldActive = true;
		}

		if (isShieldActive)
		{
			shield -= shieldDownRate * Time.deltaTime;
            isShieldBarFull = false;
		}

		if(shield <= 0)
		{
			shieldPS.Stop ();
			isShieldActive = false;
		}

		if (!isShieldActive && shield < 100)
		{
			shield += shieldUpRate * Time.deltaTime;
		}

		if(shield >= 100)
		{
			isShieldBarFull = true;
		}
	}

	public void ShootEMP()
	{
		if(Input.GetKeyDown(KeyCode.E) && !isEMPActive && isEmpBarFull)
		{
			empPS.Play ();
			isEMPActive = true;
		}

		if(isEMPActive)
		{
			emp -= empDownRate * Time.deltaTime;

            if (empCollider.radius < 7)
            {
                empCollider.radius += 1 * Time.deltaTime;
            }
		}

		if(emp <= 0)
		{
			empPS.Stop ();
			isEMPActive = false;
			isEmpBarFull = false;
            empCollider.radius = 0.1f;
		}

		if(!isEMPActive && emp <= 100)
		{
			emp += empUpRate * Time.deltaTime;
		}

		if(emp >= 100)
		{
			isEmpBarFull = true;
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		string tag = other.gameObject.tag;

		if(tag == "Enemy1" || tag == "Enemy2" || tag == "Enemy3")
		{
			if(!isShieldActive)
			{
				health -= 20;
				StartCoroutine (gameManager.FlashSprite (playerSpriteRenderer, 0.1f));
			}
		}

		else if(tag == "Missile")
		{
			if(!isShieldActive)
			{
				health -= 60;
				StartCoroutine (gameManager.FlashSprite (playerSpriteRenderer, 0.1f));
			}
		}

		else if(tag == "Asteroid")
		{
			if(!isShieldActive)
			{
				health -= 5;
				StartCoroutine (gameManager.FlashSprite (playerSpriteRenderer, 0.1f));
			}
		}

		else if(tag == "EnemyBullet")
		{
			if(!isShieldActive)
			{
				health -= 10;
				StartCoroutine (gameManager.FlashSprite (playerSpriteRenderer, 0.1f));
			}
		}
	}

	public void Die()
	{
		if(health <= 0)
		{
			isAlive = false;
			Destroy (gameObject);
		}
	}

	public void setScore(int newScore)
	{
		score += newScore;
	}

	public void setShipsDestroyed(int enemies)
	{
		enemiesDestroyed += enemies;
	}

	public void setAsteroidsDestroyed(int asteroids)
	{
		asteroidsDestroyed += asteroids;
	}

	public bool getIsPlayerAlive()
	{
		return isAlive;
	}

	public float getPlayerHealth()
	{
		return health;
	}

	public int getScore()
	{
		return score;
	}

	public int getEnemiesDestroyed()
	{
		return enemiesDestroyed;
	}

	public int getAsteroidsDestroyed()
	{
		return asteroidsDestroyed;
	}

	public bool getIsShieldActive()
	{
		return isShieldActive;
	}

    public bool getIsShieldBarFull()
    {
        return isShieldBarFull;
    }

	public bool getIsEMPActive()
	{
		return isEMPActive;
	}

	public float getShield()
	{
		return shield;
	}

	public float getEMP()
	{
		return emp;
	}
}
