// using System.Collections;
// using UnityEngine;

// public class Collectibles : MonoBehaviour
// {

//     // Updated collision method with debug log messages for "Player1" and "Player2"
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player1Blade") || collision.gameObject.CompareTag("Player2Blade"))
//         {         
//             // Destroy this collectible
//             Destroy(this.gameObject);
//         }
//     }
// }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System.Collections;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public float selfDestructionTime = 10f;
    Vector2 randomDirection;
    public float moveSpeed = 8.3f;
    public bool shouldMove = false;

    private void Start()
    {
        StartCoroutine(DestroyAfterDelay());
        randomDirection = Random.insideUnitCircle;
    }

    private void Update()
    {
        //transform.position += new Vector3(randomDirection.x, randomDirection.y, 0) * Time.deltaTime * moveSpeed;
        // move in a random direction
        if(shouldMove) transform.Translate(randomDirection * Time.deltaTime* 0.2f);
    }
    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(selfDestructionTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1Blade") || collision.gameObject.CompareTag("Player2Blade"))
        {         
            // Destroy this collectible immediately upon collision
            Destroy(this.gameObject);
        }
        // print tag of object collided with
        Debug.Log(collision.gameObject.tag);
    }
}

