using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Assign the PowerUp prefabs in the Unity Inspector
    public float spawnInterval = 5f; // Time interval between spawns
    public float powerUpDuration = 8f; // Time the power-up lasts if not collected

    public bool[] powerup_index = new bool[2]; // Two types of power-ups

    public int numberofpowerupsspawned = 0;
    public GameObject HexagonPlayground;
    SpriteRenderer sr;
    Vector3 playGroundExtendMin;
    Vector3 playGroundExtendMax;
    private bool canSpawn = false;

    [Range(0.0f, 1.0f)]
    public float freezePowerupPercentage = 0.5f; // Percentage of "freeze" power-up spawns
    public int numOfPowerup = 3;

    private void Start()
    {
        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        playGroundExtendMin = sr.bounds.min;
        playGroundExtendMax = sr.bounds.max;

        // Start spawning power-ups at regular intervals, but only after the space bar is pressed
        StartCoroutine(SpawnPowerUpsAfterSpacebar());
    }

    private IEnumerator SpawnPowerUpsAfterSpacebar()
    {
        while (!canSpawn)
        {
            yield return null; // Wait until the space bar is pressed
        }

        // Once space bar is pressed, begin spawning power-ups at regular intervals
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true) // Continue spawning power-ups indefinitely
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the spawn interval

            // Randomly choose one of the power-up prefabs to spawn
            int randomIndex = ChooseRandomPowerupIndex();
            Debug.Log(randomIndex);
            // Randomly determine the spawn position within a defined area
            Vector3 randomSpawn = new Vector3(Random.Range(-5f, 5f), Random.Range(-5, 5f), 0);

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
        float randomValue = Random.value * 100;
        return Mathf.RoundToInt(randomValue) % 3;
        // if (randomValue < freezePowerupPercentage)
        // {
        //     return 0; // "freeze" power-up
        // }
        // else
        // {
        //     return 1; // "firewall" power-up
        // }
    }

    private IEnumerator DestroyPowerUp(GameObject powerUp, float delay, int randomIndex)
    {
        yield return new WaitForSeconds(delay);
        if (powerUp != null)
        {
            powerup_index[randomIndex] = false;
            Destroy(powerUp);
        }
    }

    private void Update()
    {
        if (!canSpawn && Input.GetKeyDown(KeyCode.Space))
        {
            canSpawn = true; // Enable spawning when space bar is pressed
        }
    }
}
