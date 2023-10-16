// using UnityEngine;

// public class Spawnercode : MonoBehaviour
// {
//     public GameObject[] myobj;  // Array of prefabs to spawn
//     public float spawnInterval = 2.0f;  // Time interval between spawns
//     public float xRange = 5.0f;  // Random X range
//     public float yRange = 5.0f;  // Random Y range

//     private float timer = 0.0f;

//     void Update()
//     {
//         // Update the timer
//         timer += Time.deltaTime;

//         // Check if it's time to spawn a new object
//         if (timer >= spawnInterval)
//         {
//             // Reset the timer
//             timer = 0.0f;

//             // Generate a random position
//             Vector3 randomSpawn = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), -1);

//             // Choose a random prefab from the array
//             int randomIndex = Random.Range(0, myobj.Length);

//             // Instantiate the chosen prefab at the random position
//             Instantiate(myobj[randomIndex], randomSpawn, Quaternion.identity);
//         }
//     }
// }

using UnityEngine;

public class Spawnercode : MonoBehaviour
{
    public GameObject[] myobj;  // Array of prefabs to spawn
    public float spawnInterval = 2.0f;  // Time interval between spawns
    public float destructionTime = 5f;  // Time after which the collectible will be destroyed
    public float xRange = 5.0f;  // Random X range
    public float yRange = 5.0f;  // Random Y range

    private float timer = 0.0f;

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
            Vector3 randomSpawn = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), -1);

            // Choose a random prefab from the array
            int randomIndex = Random.Range(0, myobj.Length);

            // Instantiate the chosen prefab at the random position
            GameObject newCollectible = Instantiate(myobj[randomIndex], randomSpawn, Quaternion.identity);

            // Destroy the collectible after the specified destruction time
            Destroy(newCollectible, destructionTime);
        }
    }
}
