using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public float speed;
    public Vector3 direction; 
    bool running = false;
    Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
       direction = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            StartCoroutine(changeDirection());
        }
        dest = transform.position + direction * 0;
        transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime);
    }

    IEnumerator changeDirection()
    {
        running = true;
        yield return new WaitForSeconds(0.5f);
        direction.x = Random.Range(-5, 5);
        direction.y = Random.Range(-5, 5);
        running = false;
    }

}
