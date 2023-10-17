using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CollisionController : MonoBehaviour
{
    // detect collision with other game bodies 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("Player1Blade") && collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player 1 hit Player 2");
        }
        else if (this.gameObject.CompareTag("Player2Blade") && collision.gameObject.CompareTag("Player1"))
        {
            Debug.Log("Player 2 hit Player 1");
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
