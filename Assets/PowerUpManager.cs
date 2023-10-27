/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
*/

/*public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private KeyCode PowerUpControllingKey;
    [SerializeField] private int totalPowerUpCount = 0;
    public GameObject wallBottom;
    // enum for powerup types
    public enum PowerUpType
    {
        FireWalls, 
        Freeeze,
        Shield
    }
    // create a hashmap to store the powerups of size 3 element with value 0
    public Dictionary<PowerUpType, int> powerupsCount = new Dictionary<PowerUpType, int>()
    {
        {PowerUpType.FireWalls, 0},
        {PowerUpType.Freeeze, 0},
        {PowerUpType.Shield, 0}
    };
   
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<ScoreManager>().GetPlayerNumber();
        PowerUpControllingKey = (playerNumber == 2) ? KeyCode.Q : KeyCode.P;
    }

    // Update is called once per frame
    void Update()
    {
        if(totalPowerUpCount > 0)
        {
            UsePowerUp();
        }
    }

    private void UsePowerUp()
    {
        if (powerupsCount[PowerUpType.Freeeze] > 0)
        {
            UseFreeze();
            removePowerUp(PowerUpType.Freeeze);
        }
        else if (powerupsCount[PowerUpType.Shield] > 0)
        {
            //UseShield();
            removePowerUp(PowerUpType.Shield);
        }
        else if (powerupsCount[PowerUpType.FireWalls] > 0)
        {
            UseFireWalls();
            removePowerUp(PowerUpType.FireWalls);
        }
    }

    private void UseFreeze()
    {
        // Niranjanaa implements

    }

    private void UseFireWalls()
    {
        Debug.Log("Using Firewalls!");
    }

    void addPowerUp(PowerUpType type)
    {
        if (powerupsCount[type] <  1)
        {
            powerupsCount[type] = 1;
            totalPowerUpCount = 1;
            if (playerNumber == 1)
            {
                UIManager.instance.SetPlayer1PowerUpText(type.ToString());
            }
            else if (playerNumber == 2)
            {
                UIManager.instance.SetPlayer2PowerUpText(type.ToString());
            }
        }
        
    }
    void removePowerUp(PowerUpType type) 
    {
        if (powerupsCount[type] > 0)
        {
            powerupsCount[type] = 0;
            totalPowerUpCount = 0;
            *//*if (playerNumber == 1)
            {
                UIManager.instance.SetPlayer1PowerUpText("No Powerup Collected Yet...");
            }
            else if (playerNumber == 2)
            {
                UIManager.instance.SetPlayer2PowerUpText("No Powerup Collected Yet...");
            }*//*
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireWalls"))
        {
            addPowerUp(PowerUpType.FireWalls);
        }
        else if (collision.gameObject.CompareTag("Freeze"))
        {
            addPowerUp(PowerUpType.Freeeze);
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            addPowerUp(PowerUpType.Shield);
        }
    }
}*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // Enum for power-up types
    public enum PowerUpType
    {
        FireWalls,
        Freeze,
        Shield
    }
    public Dictionary<PowerUpType, int> powerupsCount = new Dictionary<PowerUpType, int>()
    {
        {PowerUpType.FireWalls, 0},
        {PowerUpType.Freeze, 0},
        {PowerUpType.Shield, 0}
    };

    private bool isFrozen = false;
    private float freezeTime = 10f; // Time in seconds for freezing effect

    [SerializeField]  private PlayerInputController playerController; // Reference to PlayerInputController
    [SerializeField]  private int totalPowerUpCount;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerInputController>(); // Get the reference to PlayerInputController

        // ... (other initialization)
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isMovementAllowed && totalPowerUpCount > 0)
        {
            UsePowerUp();
        }
    }

    private void UsePowerUp()
    {
        if (powerupsCount[PowerUpType.Freeze] > 0)
        {
            UseFreeze();
            removePowerUp(PowerUpType.Freeze);
        }
        else if (powerupsCount[PowerUpType.Shield] > 0)
        {
            // UseShield();
            removePowerUp(PowerUpType.Shield);
        }
        else if (powerupsCount[PowerUpType.FireWalls] > 0)
        {
           // UseFireWalls();
            removePowerUp(PowerUpType.FireWalls);
        }
    }

    private void UseFreeze()
    {
        StartCoroutine(FreezeAndUnfreeze());
    }

    private IEnumerator FreezeAndUnfreeze()
    {
        isFrozen = true;

        playerController.isMovementAllowed = false; // Freeze the player's movement

        yield return new WaitForSeconds(freezeTime);

        isFrozen = false;

        playerController.isMovementAllowed = true; // Unfreeze the player's movement
    }

    void addPowerUp(PowerUpType type)
    {
        if (powerupsCount[type] < 1)
        {
            powerupsCount[type] = 1;
            totalPowerUpCount = 1;
            //UIManager.instance.SetPlayerPowerUpText(playerController.playerNumber, type.ToString());
        }
    }

    void removePowerUp(PowerUpType type)
    {
        if (powerupsCount[type] > 0)
        {
            powerupsCount[type] = 0;
            totalPowerUpCount = 0;
            //UIManager.instance.SetPlayerPowerUpText(playerController.playerNumber, "No Powerup Collected Yet...");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireWalls"))
        {
            addPowerUp(PowerUpType.FireWalls);
        }
        else if (collision.gameObject.CompareTag("Freeze"))
        {
            addPowerUp(PowerUpType.Freeze);
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            addPowerUp(PowerUpType.Shield);
        }
    }
}
