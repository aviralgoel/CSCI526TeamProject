using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;

    // Start is called before the first frame update
    void Start()
    {
        playerOne.SetActive(false);
        playerTwo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   

        if(playerOne.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerOne.SetActive(true);
            }
        }
        else if(playerTwo.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                playerTwo.SetActive(true);
            }
        }

        // Manage Player Inputs
        if(playerOne.activeInHierarchy)
        {
            PlayerOneInputController();
        }
        if(playerTwo.activeInHierarchy)
        {
            PlayerTwoInputController();
        }
    }

    private void PlayerTwoInputController()
    {
        return;
    }

    private void PlayerOneInputController()
    {
        return;
    }
}
