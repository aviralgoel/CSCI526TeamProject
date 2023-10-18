using System.Collections;
using UnityEngine;

public class Collectibles : MonoBehaviour
{

    // Updated collision method with debug log messages for "Player1" and "Player2"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1Blade") || collision.gameObject.CompareTag("Player2Blade"))
        {

           
            // Destroy this collectible
            Destroy(this.gameObject);


        }
    }
}
