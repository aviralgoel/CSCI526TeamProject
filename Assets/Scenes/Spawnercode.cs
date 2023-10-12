

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnercode : MonoBehaviour
{
    public GameObject[] myobj;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomIndex = Random.Range(0, myobj.Length);
            Vector3 randomSpawn = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), -1);

            // This line will instantiate a capsule and the capsule will start its own deactivation timer.
            Instantiate(myobj[randomIndex], randomSpawn, Quaternion.identity);
        }
    }
}
