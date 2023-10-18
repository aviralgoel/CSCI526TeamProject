// using System.Collections;
// using UnityEngine;

// public class Collectibles : MonoBehaviour
// {
//     public float speed;
//     public Vector3 direction; 
//     bool running = false;

//     public float minDeactivationTime = 3f;
//     public float maxDeactivationTime = 8f;
//     Vector3 dest;
    
//     void Start()
//     {
//         direction = Vector3.zero;
//     }

//     private void OnEnable()
//     {
//         StartCoroutine(DelayedDeactivate());
//     }

//     void Update()
//     {
//         if (!running)
//         {
//             StartCoroutine(changeDirection());
//         }
//         dest = transform.position + direction * 0;
//         transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime);
//     }

//     IEnumerator changeDirection()
//     {
//         running = true;
//         yield return new WaitForSeconds(0.5f);
//         direction.x = Random.Range(-5, 5);
//         direction.y = Random.Range(-5, 5);
//         running = false;
//     }

//     IEnumerator DelayedDeactivate()
//     {
//         float randomTime = Random.Range(minDeactivationTime, maxDeactivationTime);
//         yield return new WaitForSeconds(randomTime);
//         this.gameObject.SetActive(false);
//     }

//     // Modified collision method for both "Player1" and "Player2"
//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
//         {
//             // Destroy this collectible
//             Destroy(this.gameObject);

//             // Add your +1 score animation or logic here for the respective player
//             if(collision.gameObject.CompareTag("Player1"))
//             {
//                 // Logic or animation for Player1 scoring
//             }
//             else
//             {
//                 // Logic or animation for Player2 scoring
//             }
//         }
//     }
// }

using System.Collections;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    //public float speed;
    //public Vector3 direction; 
    //bool running = false;

    //public float minDeactivationTime = 3f;
    //public float maxDeactivationTime = 15f;
    //Vector3 dest;
    
    void Start()
    {
        //direction = Vector3.zero;
    }

  /*  private void OnEnable()
    {
        //StartCoroutine(DelayedDeactivate());
    }*/

    void Update()
    {
       /* if (!running)
        {
            StartCoroutine(changeDirection());
        }
        dest = transform.position + direction * 0;
        transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime);*/
    }

/*    IEnumerator changeDirection()
    {
        running = true;
        yield return new WaitForSeconds(0.5f);
        direction.x = Random.Range(-2.5f, 3.75f);
        direction.y = Random.Range(-2.1f, 3.5f);
        running = false;
    }*/
/*
    IEnumerator DelayedDeactivate()
    {
        float randomTime = Random.Range(minDeactivationTime, maxDeactivationTime);
        yield return new WaitForSeconds(randomTime);
        this.gameObject.SetActive(false);
    }*/

    // Updated collision method with debug log messages for "Player1" and "Player2"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            // Destroy this collectible
            Destroy(this.gameObject);

            if(collision.gameObject.CompareTag("Player1"))
            {
                Debug.Log("Player1 collected a collectible!"); // Debug message for Player1
                // Logic or animation for Player1 scoring
            }
            else if(collision.gameObject.CompareTag("Player2"))
            {
                Debug.Log("Player2 collected a collectible!"); // Debug message for Player2
                // Logic or animation for Player2 scoring
            }
        }
    }
}
