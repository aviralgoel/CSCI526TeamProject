using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    // public GameObject player;
    public Transform target;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
	}

    public void setTarget(Transform input) {
        this.target = input;
    }

/*    void OnTriggerEnter2D(Collider2D collision)
     {
         if(collision.gameObject.CompareTag("Player1"))
         {
             player1ScoreManager.IncrementScore(-10);
             Debug.Log("Hit P1");
             Destroy(this.gameObject);
         }
         if(collision.gameObject.CompareTag("Player2"))
         {
             player2ScoreManager.IncrementScore(-10);
             Debug.Log("Hit P2");
             Destroy(this.gameObject);
         }
         // Destroy(gameObject);
     }*/

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    //     if(col.gameObject.CompareTag("Player1") || col.gameObject.CompareTag("Player2") || col.gameObject.CompareTag("Player1Boundary") || col.gameObject.CompareTag("Player2Boundary"))
    //     {
    //         Destroy(gameObject);
    //     }
    //     //spriteMove = -0.1f;
    // }
}

