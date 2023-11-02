/*using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Assign the PowerUp prefabs in the Unity Inspector
    public float spawnInterval = 15f; // Time interval between spawns
    public float powerUpDuration = 8f; // Time the power-up lasts if not collected

    public bool[] powerup_index = new bool[5];
    public int numberofpowerupsspawned = 0;
    public GameObject HexagonPlayground;
    SpriteRenderer sr;
    Vector3 playGroundExtendMin;
    Vector3 playGroundExtendMax;
    private bool canSpawn = false;

    private void Start()
    {
        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        playGroundExtendMin = sr.bounds.min;
        playGroundExtendMax = sr.bounds.max;

        // Start spawning power-ups at regular intervals
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        if (canSpawn == false && Input.GetKeyDown(KeyCode.Space))
        {
            canSpawn = true; // Enable spawning when spacebar is pressed
        }

        while (true) // Continue spawning power-ups indefinitely
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the spawn interval

            // Randomly choose one of the power-up prefabs to spawn
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUpPrefabToSpawn = powerUpPrefabs[randomIndex];

            // Randomly determine the spawn position within a defined area
            Vector3 randomSpawn = new Vector3(Random.Range(-5f, 5f), Random.Range(-5, 5f), 0);

            if(randomSpawn.x > playGroundExtendMin.x && randomSpawn.x < playGroundExtendMax.x && randomSpawn.y > playGroundExtendMin.y && randomSpawn.y < playGroundExtendMax.y && powerup_index[randomIndex] == false)
            {
                GameObject newCollectible = Instantiate(powerUpPrefabs[randomIndex], randomSpawn, Quaternion.identity);
                powerup_index[randomIndex] = true;
                numberofpowerupsspawned++;
                StartCoroutine(DestroyPowerUp(newCollectible, powerUpDuration, randomIndex));
            }

            // // Instantiate the chosen power-up at the spawn position
            // GameObject powerUp = Instantiate(powerUpPrefabToSpawn, spawnPosition, Quaternion.identity);

            // Destroy the power-up after a specified duration
            // StartCoroutine(DestroyPowerUp(powerUp, powerUpDuration, randomIndex));
        }
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
}
*/


using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Assign the PowerUp prefabs in the Unity Inspector
    public float spawnInterval = 15f; // Time interval between spawns
    public float powerUpDuration = 8f; // Time the power-up lasts if not collected

    public bool[] powerup_index = new bool[5];
    public int numberofpowerupsspawned = 0;
    public GameObject HexagonPlayground;
    SpriteRenderer sr;
    Vector3 playGroundExtendMin;
    Vector3 playGroundExtendMax;
    private bool canSpawn = false;

    private void Start()
    {
        sr = HexagonPlayground.GetComponent <SpriteRenderer>();
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
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUpPrefabToSpawn = powerUpPrefabs[randomIndex];

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
