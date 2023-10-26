using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpManager : MonoBehaviour
{   
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
        
    }

    // Update is called once per frame
    void Update()
    {
        // print number of firewall powerup count
        Debug.Log(powerupsCount[PowerUpType.FireWalls]);
    }

    void addPowerUp(PowerUpType type)
    {
        if (powerupsCount[type] <  1)
        {
            powerupsCount[type] = 1;
        }
    }
    void removePowerUp(PowerUpType type) 
    {
        if (powerupsCount[type] > 0)
        {
            powerupsCount[type] = 0;
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
}
