/*using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BladeCollisionChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("good") || collision.collider.CompareTag("bad"))
        {
            if (gameObject.CompareTag("Player1Blade"))
            {
                Debug.Log("Player 1 blade collided with " + collision.collider.tag);
            }
            else if (gameObject.CompareTag("Player2Blade"))
            {
                Debug.Log("Player 2 blade collided with " + collision.collider.tag);
            }
        }
    }
}







*/

using UnityEngine;

public class BladeCollisionChecker : MonoBehaviour
{
    private ScoreManager scoreManager;

    private void Start()
    {
        // Find the ScoreManager script in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Good") || collider.CompareTag("Bad"))
        {
            if (gameObject.CompareTag("Player1Blade"))
            {
                Debug.Log("Player 1 blade collided with " + collider.tag);
                if (scoreManager != null)
                {
                    scoreManager.IncrementScore();
                }

            }
            else if (gameObject.CompareTag("Player2Blade"))
            {
                Debug.Log("Player 2 blade collided with " + collider.tag);
                if (scoreManager != null)
                {
                    scoreManager.DecrementScore();
                }
            }
        }
    }
    
    
}
