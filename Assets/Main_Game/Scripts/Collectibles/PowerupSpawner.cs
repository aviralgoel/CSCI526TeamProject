using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public float radiusToSpawnWithin = 3f;
    public GameObject[] powerUpPrefabs; // Assign the PowerUp prefabs in the Unity Inspector
    public float spawnInterval = 5f; // Time interval between spawns
    public float powerUpDuration = 8f; // Time the power-up lasts if not collected

    public bool[] powerup_index = new bool[2]; // Two types of power-ups

    public int numberofpowerupsspawned = 0;
    public GameObject HexagonPlayground;
    public GameObject CenterOfPlayGround;
    SpriteRenderer sr;
    Vector3 playGroundExtendMin;
    Vector3 playGroundExtendMax;
    private GameManager gameManager;
    private bool canSpawn = false;

    [Range(0.0f, 1.0f)]
    public float freezePowerupPercentage = 0.5f; // Percentage of "freeze" power-up spawns

    private void Start()
    {
        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        playGroundExtendMin = sr.bounds.min;
        playGroundExtendMax = sr.bounds.max;

        // Start spawning power-ups at regular intervals, but only after the space bar is pressed
        StartCoroutine(SpawnPowerUpsgame());
    }

    private IEnumerator SpawnPowerUpsgame()
    {
        gameManager = FindObjectOfType<GameManager>();
        while (!canSpawn)
        {
            yield return null; // Wait until the space bar is pressed
        }

        if (canSpawn && gameManager != null && gameManager.isGameStarted && gameManager.isGameOver == false)// Once space bar is pressed, begin spawning power-ups at regular intervals
        {
            StartCoroutine(SpawnPowerUps());
        }
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true) // Continue spawning power-ups indefinitely
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the spawn interval

            // Randomly choose one of the power-up prefabs to spawn
            int randomIndex = ChooseRandomPowerupIndex();


            // Randomly determine the spawn position within a defined area
            Vector3 randomSpawn = Random.insideUnitCircle * radiusToSpawnWithin;

            if (randomSpawn.x > playGroundExtendMin.x && randomSpawn.x < playGroundExtendMax.x && randomSpawn.y > playGroundExtendMin.y && randomSpawn.y < playGroundExtendMax.y && powerup_index[randomIndex] == false)
            {
                GameObject newCollectible = Instantiate(powerUpPrefabs[randomIndex], randomSpawn, Quaternion.identity);
                powerup_index[randomIndex] = true;
                numberofpowerupsspawned++;
                StartCoroutine(DestroyPowerUp(newCollectible, powerUpDuration, randomIndex));
            }
        }
    }

    private int ChooseRandomPowerupIndex()
    {
        float randomValue = Random.value;
        if (randomValue < freezePowerupPercentage)
        {
            return 0; // "freeze" power-up
        }
        else
        {
            return 1; // "firewall" power-up
        }
    }

    private IEnumerator DestroyPowerUp(GameObject powerUp, float delay, int randomIndex)
    {
        yield return new WaitForSeconds(delay);
        if (powerUp != null)
        {
            Destroy(powerUp);
        }
        powerup_index[randomIndex] = false;
    }


}


/*
using System.Collections;
using UnityEngine;
using System.Collections.Generic; // Add this for List

public class PowerupSpawner : MonoBehaviour
{
    public float radiusToSpawnWithin = 3f;
    public GameObject[] powerUpPrefabs; // Assign the PowerUp prefabs in the Unity Inspector
    public float spawnInterval = 5f; // Time interval between spawns
    public float powerUpDuration = 8f; // Time the power-up lasts if not collected

 
    public GameObject HexagonPlayground;
    public GameObject CenterOfPlayGround;
    SpriteRenderer sr;
    Vector3 playGroundExtendMin;
    Vector3 playGroundExtendMax;
    private GameManager gameManager;
    private bool canSpawn = false;

    [Range(0.0f, 1.0f)]
    public float freezePowerupPercentage = 0.5f; // Percentage of "freeze" power-up spawns

    private float timeSinceLastSpawn = 0f;
    private int lastSpawnedPowerupIndex = -1;

    private void Start()
    {
        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        playGroundExtendMin = sr.bounds.min;
        playGroundExtendMax = sr.bounds.max;

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!canSpawn || gameManager == null || !gameManager.isGameStarted || gameManager.isGameOver)
            return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnPowerUp();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnPowerUp()
    {
        int randomIndex = ChooseRandomPowerupIndex();

        // Randomly determine the spawn position within a defined area
        Vector3 randomSpawn = Random.insideUnitCircle * radiusToSpawnWithin;

        if (randomSpawn.x > playGroundExtendMin.x && randomSpawn.x < playGroundExtendMax.x && randomSpawn.y > playGroundExtendMin.y && randomSpawn.y < playGroundExtendMax.y)
        {
            GameObject newPowerUp = Instantiate(powerUpPrefabs[randomIndex], randomSpawn, Quaternion.identity);
            numberofpowerupsspawned++;
            StartCoroutine(DestroyPowerUp(newPowerUp, powerUpDuration, randomIndex));
        }
    }

    private int ChooseRandomPowerupIndex()
    {
        int randomIndex = Random.Range(0, maxPowerUpIndex + 1);

        return randomIndex;
    }

    private IEnumerator DestroyPowerUp(GameObject powerUp, float delay, int randomIndex)
    {
        yield return new WaitForSeconds(delay);

        if (powerUp != null)
        {
            Destroy(powerUp);
        }
    }
}
*/