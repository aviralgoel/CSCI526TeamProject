using System.Collections;
using System.Threading;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Assign the PowerUp prefabs in the Unity Inspector
    public float spawnInterval = 5f; // Time interval between spawns
    public float powerUpDuration = 8f; // Time the power-up lasts if not collected
    public float radiusToSpawnWithin = 5f;

    public bool[] powerup_index = new bool[2]; // Two types of power-ups

    public int numberofpowerupsspawned = 0;
    public GameObject HexagonPlayground;
    SpriteRenderer sr;
    Vector3 playGroundExtendMin;
    Vector3 playGroundExtendMax;
    private bool canSpawn = false;
    private float timer = 0.0f;
    private LayerMask layerMask;

    [Range(0.0f, 1.0f)]
    public float freezePowerupPercentage = 0.5f; // Percentage of "freeze" power-up spawns
    public int numOfPowerup = 3;

    public bool shouldCollectibleMove = false;
    public GameManager gameManager;

    private void Start()
    {
        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        playGroundExtendMin = sr.bounds.min;
        playGroundExtendMax = sr.bounds.max;
        layerMask = ~LayerMask.GetMask("Walls");
    }

    
    /*private IEnumerator SpawnPowerUps()
    {
        while (true) // Continue spawning power-ups indefinitely
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the spawn interval

            // Randomly choose one of the power-up prefabs to spawn
            int randomIndex = ChooseRandomPowerupIndex();
            
            // Randomly determine the spawn position within a defined area
            Vector3 randomSpawn = new Vector3(Random.Range(-5f, 5f), Random.Range(-5, 5f), 0);

            // Check if the new position is too close to existing power-ups or collectibles
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomSpawn, 0.3f, layerMask);

            bool canSpawnHere = true;
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Player1") || collider.gameObject.CompareTag("Player2") || collider.gameObject.CompareTag("Blackhole"))
                {
                    canSpawnHere = false; 
                }
                else
                {
                    canSpawnHere = true;
                    
                }
            }

            if (canSpawnHere &&
                randomSpawn.x > playGroundExtendMin.x && randomSpawn.x < playGroundExtendMax.x &&
                randomSpawn.y > playGroundExtendMin.y && randomSpawn.y < playGroundExtendMax.y && powerup_index[randomIndex] == false)
            {
                GameObject newCollectible = Instantiate(powerUpPrefabs[randomIndex], randomSpawn, Quaternion.identity);
                if(shouldCollectibleMove) newCollectible.GetComponent<Collectibles>().shouldMove = true;
                powerup_index[randomIndex] = true;
                numberofpowerupsspawned++;
                StartCoroutine(DestroyPowerUp(newCollectible, powerUpDuration, randomIndex));
            }
        }
    }*/

    private int ChooseRandomPowerupIndex()
    {
        float randomValue = Random.value * 100;
        return Mathf.RoundToInt(randomValue) % 3;
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

    private void Update()
    {
        if (gameManager.isGameStarted && !gameManager.isGameOver)
        {
            canSpawn = true;
        }
        if(canSpawn)
        {   
            timer += Time.deltaTime;
            if (timer > spawnInterval)
            {
                timer = 0.0f;
                // Generate a random position
                int randomIndex = ChooseRandomPowerupIndex();
                Vector3 randomSpawn = Random.insideUnitCircle * radiusToSpawnWithin;
                // Check if the new position is too close to existing power-ups or collectibles
                Collider2D[] colliders = Physics2D.OverlapCircleAll(randomSpawn, 0.3f, layerMask);

                bool canSpawnHere = false;
                if(colliders.Length == 0)
                {
                    canSpawnHere = true;
                }
                if (canSpawnHere &&
                randomSpawn.x > playGroundExtendMin.x && randomSpawn.x < playGroundExtendMax.x &&
                randomSpawn.y > playGroundExtendMin.y && randomSpawn.y < playGroundExtendMax.y && powerup_index[randomIndex] == false)
                {
                    GameObject newCollectible = Instantiate(powerUpPrefabs[randomIndex], randomSpawn, Quaternion.identity);
                    if (shouldCollectibleMove) newCollectible.GetComponent<Collectibles>().shouldMove = true;
                    powerup_index[randomIndex] = true;
                    numberofpowerupsspawned++;
                    StartCoroutine(DestroyPowerUp(newCollectible, powerUpDuration, randomIndex));
                }


            }
        }
       
    }
}
