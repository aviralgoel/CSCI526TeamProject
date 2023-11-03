using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public GameObject player1;
    public GameObject player2;
    public ScoreManager player1ScoreManager;
    public ScoreManager player2ScoreManager;
    [SerializeField] private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(gameManager.isGameStarted) {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
	}

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player1"))
        {
            player1ScoreManager.IncrementScore(-10);
            Debug.Log("Hit P1");
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Player2"))
        {
            player2ScoreManager.IncrementScore(-10);
            Debug.Log("Hit P2");
            Destroy(gameObject);
        }
        // Destroy(gameObject);
    }
}
