using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CollisionController : MonoBehaviour
{
    public GameObject enemy;
    public int score = 10;
    private Vector3 pos;
    void Start()
    {
        pos = enemy.transform.position;
    }
    // detect collision with other game bodies 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == enemy)
        {
            Debug.Log("Player 2 hit Player 1");
            enemy.transform.position = pos;
            score += 10;
        }
    }
    private void Update()
    {
        // if space key is pressed, print in debug the tag of this game object
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log(this.gameObject.tag);
        }
    }
    

}
