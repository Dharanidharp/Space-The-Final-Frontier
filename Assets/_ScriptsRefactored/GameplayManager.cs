using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {

    // Instance variables
	[SerializeField] private Color[] colors = new Color[2];
	[SerializeField] private Transform spawner;
	[SerializeField] private GameObject[] enemies;
	[SerializeField] private GameObject asteroids;

	[SerializeField] private float enemyShipTimer = 2.0f;
	[SerializeField] private float asteroidTimer;

	[SerializeField] private Text scoreText;
	[SerializeField] private Text shipsDestroyedText;
	[SerializeField] private Text asteroidsDestroyedText;
	[SerializeField] private Image healthBar;
	[SerializeField] private Image shieldBar;
	[SerializeField] private Image empBar;
    [SerializeField] private Text shieldPercentageInfo;

    [SerializeField] private LevelManager levelManager;
	[SerializeField] private RPlayerController rPlayerController;

    // On Start
	public void Start()
	{
		rPlayerController = FindObjectOfType<RPlayerController> ();
		levelManager = FindObjectOfType<LevelManager> ();
        asteroidTimer = Random.Range(2.0f, 4.0f);

		// Get UI Text components
		scoreText = GameObject.FindGameObjectWithTag ("score").GetComponent<Text> ();
        shipsDestroyedText = GameObject.FindGameObjectWithTag ("shipsDestroyedNumber").GetComponent<Text> ();
        asteroidsDestroyedText = GameObject.FindGameObjectWithTag ("asteroidsDestroyedNumber").GetComponent<Text> ();
        shieldPercentageInfo = GameObject.FindGameObjectWithTag("Shield_Info").GetComponent<Text>();

        // Get UI Image components
        healthBar = GameObject.FindGameObjectWithTag ("healthBar").GetComponent<Image> ();
        shieldBar = GameObject.FindGameObjectWithTag ("shieldBar").GetComponent<Image> ();
        empBar = GameObject.FindGameObjectWithTag ("empBar").GetComponent<Image> ();
	}

    // Updates once every frame
	public void Update()
	{
		if(!rPlayerController.getIsPlayerAlive())
		{
			
		}

		UpdateUI ();
	}

    // Update once every physics update
	public void FixedUpdate()
	{
		SpawnEnemy (enemies, asteroids);
	}

    // Flash the sprites when damaged
	public IEnumerator FlashSprite(SpriteRenderer spriteRenderer, float delay)
	{
		spriteRenderer.material.color = colors [0];
		yield return new WaitForSeconds (delay);
		spriteRenderer.material.color = colors [1];
	}

    // Play sounds
	public void PlaySound(AudioClip audio, float volMultiplier)
	{
		AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, Mathf.Clamp(volMultiplier, 0.05f, 1f));
	}

    // Weapons
	public void ShootBullets(GameObject bulletObject, Transform[] bulletPoints, AudioClip bulletShotSound)
	{
		GameObject newObject = bulletObject;
		Transform[] newTransforms = bulletPoints;
		AudioClip newBulletSound = bulletShotSound;

        // Loop through all the bullet points and instantiate bullets at each one of them
		foreach(Transform i in newTransforms)
		{
			Instantiate (newObject, i.transform.position, Quaternion.identity);
			PlaySound (newBulletSound, 0.2f);
		}
	}

    // Spawn enemy ships and asteroids
	public void SpawnEnemy(GameObject[] enemyShip, GameObject asteroid)
	{
		GameObject[] newEnemyShip = enemyShip;
		GameObject newAsteroid = asteroid;

        // Spawn enemies between -1 and +1 values of the spawner transform scale
		Vector3 spawnVector = new Vector3 (Random.Range (-spawner.localScale.x, (spawner.localScale.x * 2)), spawner.position.y, 0);

		enemyShipTimer -= Time.deltaTime;
		asteroidTimer -= Time.deltaTime;

        // Instantiate enemies when timer reaches 0
		if(enemyShipTimer <= 0)
		{
			Instantiate (newEnemyShip [Random.Range (0, 3)], spawnVector, Quaternion.identity);
			enemyShipTimer = 2.0f;
		}

        // Instantiate asteroids when timer reaches 0
		else if(asteroidTimer <= 0)
		{
            // Set the size of the asteroids to a random value
            float asteroidSize = Random.Range(1.0f, 3.5f);
            newAsteroid.transform.localScale = new Vector3(asteroidSize, asteroidSize, 0);

            Instantiate (newAsteroid, spawnVector, Quaternion.identity);
			asteroidTimer = Random.Range (2.0f, 4.0f);
		}
	}

    // Updates the UI
	public void UpdateUI()
	{
		healthBar.fillAmount = rPlayerController.getPlayerHealth () / 1000f;
		shieldBar.fillAmount = rPlayerController.getShield () / 100f;
		empBar.fillAmount = rPlayerController.getEMP () / 100f;

		scoreText.text = rPlayerController.getScore ().ToString ();
		asteroidsDestroyedText.text = rPlayerController.getAsteroidsDestroyed ().ToString ();
		shipsDestroyedText.text = rPlayerController.getEnemiesDestroyed ().ToString ();

        if (!rPlayerController.getIsShieldBarFull())
        {
            shieldPercentageInfo.text = "Shield at " + Mathf.Round(rPlayerController.getShield()).ToString();
        }
        else
        {
            shieldPercentageInfo.text = "Shields ready !";
        }
	}
}
