using System.Collections;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private float selfDestructionTime = 10f;

    private void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(selfDestructionTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {         
            // Destroy this collectible immediately upon collision
            Destroy(this.gameObject);
        }
    }
}
