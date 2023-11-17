/*using UnityEngine;

public class Spawnercode : MonoBehaviour
{
    public float radiusToSpawnWithin = 3.10f;
    public bool shouldCollectibleMove = false;
    public GameObject[] myobj;  // Array of prefabs to spawn
    public float spawnInterval = 0.5f;  // Time interval between spawns
    public float destructionTime = 15f;  // Time after which the collectible will be destroyed
    public GameObject HexagonPlayground;
    private SpriteRenderer sr; // Change the variable type to SpriteRenderer
    private float timer = 0.0f;
    private Vector3 playGroundExtendMin;
    private Vector3 playGroundExtendMax;
    public int numOfCollectiblesSpawned = 0;
    private bool canSpawn = false; // Control whether objects can spawn or not


    [Range(0.0f, 1.0f)]
    public float goodObjectPercentage = 0.75f;  // Percentage of "good" object spawns

    private void Start()
    {
        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        playGroundExtendMin = sr.bounds.min;
        playGroundExtendMax = sr.bounds.max;
    }

    void Update()
    {
        if (canSpawn == false && Input.GetKeyDown(KeyCode.Space))
        {
            canSpawn = true; // Enable spawning when spacebar is pressed
        }

        if (canSpawn)
        {
            // Update the timer
            timer += Time.deltaTime;

            // Check if it's time to spawn a new object
            if (timer >= spawnInterval)
            {
                // Reset the timer
                timer = 0.0f;

                // Generate a random position
                Vector3 randomSpawn = Random.insideUnitCircle * radiusToSpawnWithin;

                // Determine whether to spawn a "good" or "bad" object based on the percentage
                int randomIndex = (Random.value < goodObjectPercentage) ? 0 : 1;

                if (randomSpawn.x > playGroundExtendMin.x && randomSpawn.x < playGroundExtendMax.x &&
                    randomSpawn.y > playGroundExtendMin.y && randomSpawn.y < playGroundExtendMax.y)
                {
                    GameObject newCollectible = Instantiate(myobj[randomIndex], randomSpawn, Quaternion.identity);
                    if (shouldCollectibleMove)
                    {
                        newCollectible.GetComponent<Collectibles>().shouldMove = true;
                    }
                    numOfCollectiblesSpawned++;
                }
            }
        }
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }
}
*/

using UnityEngine;

public class Spawnercode : MonoBehaviour
{
    public float radiusToSpawnWithin = 3.10f;
    public bool shouldCollectibleMove = false;
    public GameObject[] myobj;  // Array of prefabs to spawn
    public float spawnInterval = 0.5f;  // Time interval between spawns
    public float destructionTime = 15f;  // Time after which the collectible will be destroyed
    public GameObject HexagonPlayground;
    private SpriteRenderer sr; // Change the variable type to SpriteRenderer
    private float timer = 0.0f;
    private Vector3 playGroundExtendMin;
    private Vector3 playGroundExtendMax;
    public int numOfCollectiblesSpawned = 0;
    private bool canSpawn = false; // Control whether objects can spawn or not
    private LayerMask layerMask;
    public GameManager gameManager;
    


    [Range(0.0f, 1.0f)]
    public float goodObjectPercentage = 0.75f;  // Percentage of "good" object spawns

    private void Start()
    {
        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        playGroundExtendMin = sr.bounds.min;
        playGroundExtendMax = sr.bounds.max;
        layerMask = ~LayerMask.GetMask("Walls"); 
        //gameManager = GetComponent<GameManager>(); 
    }

    void Update()
    {
        if (canSpawn == false && gameManager.isGameStarted)
        {
            canSpawn = true; // Enable spawning when spacebar is pressed
        }

        if (canSpawn)
        {
            // Update the timer
            timer += Time.deltaTime;

            // Check if it's time to spawn a new object
            if (timer >= spawnInterval)
            {
                // Reset the timer
                timer = 0.0f;

                // Generate a random position
                Vector3 randomSpawn = Random.insideUnitCircle * radiusToSpawnWithin;

                // Check if the new position is too close to existing collectibles
                Collider2D[] colliders = Physics2D.OverlapCircleAll(randomSpawn, 0.4f, layerMask);


                bool canSpawnHere = false;
                if (colliders.Length == 0)
                {
                    canSpawnHere = true;
                }

        

                if (canSpawnHere &&
                    randomSpawn.x > playGroundExtendMin.x && randomSpawn.x < playGroundExtendMax.x &&
                    randomSpawn.y > playGroundExtendMin.y && randomSpawn.y < playGroundExtendMax.y)
                {
                    // Determine whether to spawn a "good" or "bad" object based on the percentage
                    int randomIndex = (Random.value < goodObjectPercentage) ? 0 : 1;

                    GameObject newCollectible = Instantiate(myobj[randomIndex], randomSpawn, Quaternion.identity);
                    if (shouldCollectibleMove)
                    {
                        newCollectible.GetComponent<Collectibles>().shouldMove = true;
                    }
                    numOfCollectiblesSpawned++;
                }
            }
        }
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }
}
