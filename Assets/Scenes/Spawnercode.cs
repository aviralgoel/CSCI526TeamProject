using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnercode : MonoBehaviour
{
    public GameObject[] myobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomIndex = Random.Range(0, myobj.Length);
            Vector3 randomSpawn = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), -1);

            Instantiate(myobj[randomIndex], randomSpawn, Quaternion.identity);
        }
    }
}
