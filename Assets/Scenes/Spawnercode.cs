using UnityEngine;

public class Spawnercode : MonoBehaviour
{
    public GameObject[] myobj;  // Array of prefabs to spawn
    public float spawnInterval = 1.0f;  // Time interval between spawns
    public float destructionTime = 15f;  // Time after which the collectible will be destroyed
    public GameObject HexagonPlayground;
    SpriteRenderer sr;
    private float timer = 0.0f;
    Vector3 playGroundExtendMin;
    Vector3 playGroundExtendMax;
    public int numOfCollectiblesSpawned = 0;

    private void Start()
    {
        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        playGroundExtendMin = sr.bounds.min;
        playGroundExtendMax = sr.bounds.max;
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new object
        if (timer >= spawnInterval)
        {
            // Reset the timer
            timer = 0.0f;

            // Generate a random position
            Vector3 randomSpawn = new Vector3(Random.Range(-5f, 5f), Random.Range(-5, 5f), 0);

            // Choose a random prefab from the array
            int randomIndex = Random.Range(0, myobj.Length);

            if(randomSpawn.x > playGroundExtendMin.x && randomSpawn.x < playGroundExtendMax.x && randomSpawn.y > playGroundExtendMin.y && randomSpawn.y < playGroundExtendMax.y)
            {
                GameObject newCollectible = Instantiate(myobj[randomIndex], randomSpawn, Quaternion.identity);
                numOfCollectiblesSpawned++;
            }

        }
    }
}
