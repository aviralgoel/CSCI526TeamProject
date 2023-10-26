using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private KeyCode PowerUpControllingKey;
    [SerializeField] private int totalPowerUpCount = 0;

    public float fireWallMovementSpeed = 0.2f;

    public Transform wallBottom;
    public Transform wallTop;
    public Transform wallLeftTop;
    public Transform wallLeftBottom;
    public Transform wallRightTop;
    public Transform wallRightBottom;

    public Transform wallBottomDestination;
    public Transform wallTopDestination;
    public Transform wallLeftTopDestination;
    public Transform wallLeftBottomDestination;
    public Transform wallRightTopDestination;
    public Transform wallRightBottomDestination;

    public Transform wallBottomSource;
    public Transform wallTopSource;
    public Transform wallLeftTopSource;
    public Transform wallLeftBottomSource;
    public Transform wallRightTopSource;
    public Transform wallRightBottomSource;



    public bool fireWallActive = false;
    public bool moveWallsInside = false;
    public bool moveWallsOutside = false;
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

        /*wallBottomSource = wallBottom.position;
        wallTopSource = wallTop.position;
        wallLeftTopSource = wallLeftTop.position;
        wallLeftBottomSource = wallLeftBottom.position;
        wallRightTopSource = wallRightTop.position;
        wallRightBottomSource = wallRightBottom.position;*/

    }

    // Update is called once per frame
    void Update()
    {
        if(totalPowerUpCount > 0)
        {
            UsePowerUp();
        }
        if(fireWallActive == true && moveWallsInside == true)
        {
            MoveWallsInside();
        }
        if(fireWallActive == true && moveWallsOutside == true)
        {
            MoveWallsOutside();
        }
    }

    private void MoveWallsInside()
    {   
        // move
        wallBottom.position = Vector3.MoveTowards(wallBottom.position, wallBottomDestination.position, Time.deltaTime * fireWallMovementSpeed);
        wallLeftBottom.position = Vector3.MoveTowards(wallLeftBottom.position, wallLeftBottomDestination.position, Time.deltaTime*fireWallMovementSpeed);
        wallLeftTop.position = Vector3.MoveTowards(wallLeftTop.position, wallLeftTopDestination.position, Time.deltaTime*fireWallMovementSpeed);
        wallRightBottom.position = Vector3.MoveTowards(wallRightBottom.position, wallRightBottomDestination.position, Time.deltaTime*fireWallMovementSpeed);
        wallRightTop.position = Vector3.MoveTowards(wallRightTop.position, wallRightTopDestination.position, Time.deltaTime*fireWallMovementSpeed);
        wallTop.position = Vector3.MoveTowards(wallTop.position, wallTopDestination.position, Time.deltaTime*fireWallMovementSpeed);
        if (Mathf.Approximately(Vector3.Distance(wallBottom.position, wallBottomDestination.position),0))
        {
            StartCoroutine(Pause());
            moveWallsInside = false;

        }
        

        // shake
        //wallBottom.GetComponent<ShakeMe>().Shake();
        //wallLeftBottom.GetComponent<ShakeMe>().Shake();
        //wallLeftTop.GetComponent<ShakeMe>().Shake();
        //wallRightBottom.GetComponent<ShakeMe>().Shake();
        //wallRightTop.GetComponent<ShakeMe>().Shake();
        //wallTop.GetComponent<ShakeMe>().Shake();
    }
    private void MoveWallsOutside()
    {   
        // move
        wallBottom.position = Vector3.MoveTowards(wallBottom.position, wallBottomSource.position, Time.deltaTime * fireWallMovementSpeed );
        wallLeftBottom.position = Vector3.MoveTowards(wallLeftBottom.position, wallLeftBottomSource.position, Time.deltaTime*fireWallMovementSpeed);
        wallLeftTop.position = Vector3.MoveTowards(wallLeftTop.position, wallLeftTopSource.position, Time.deltaTime*fireWallMovementSpeed);
        wallRightBottom.position = Vector3.MoveTowards(wallRightBottom.position, wallRightBottomSource.position, Time.deltaTime * fireWallMovementSpeed);
        wallRightTop.position = Vector3.MoveTowards(wallRightTop.position, wallRightTopSource.position, Time.deltaTime * fireWallMovementSpeed);
        wallTop.position = Vector3.MoveTowards(wallTop.position, wallTopSource.position , Time.deltaTime * fireWallMovementSpeed);
        if (Mathf.Approximately(Vector3.Distance(wallBottom.position, wallBottomSource.position),0))
        {
           moveWallsOutside=false;
            fireWallActive = false;
        }


        // shake
        //wallBottom.GetComponent<ShakeMe>().Shake();
        //wallLeftBottom.GetComponent<ShakeMe>().Shake();
        //wallLeftTop.GetComponent<ShakeMe>().Shake();
        //wallRightBottom.GetComponent<ShakeMe>().Shake();
        //wallRightTop.GetComponent<ShakeMe>().Shake();
        //wallTop.GetComponent<ShakeMe>().Shake();
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
        fireWallActive = true;
        moveWallsInside = true;
        //StartCoroutine(WallMoveFunction(wallBottom.transform, wallBottomDestination));
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
            /*if (playerNumber == 1)
            {
                UIManager.instance.SetPlayer1PowerUpText("No Powerup Collected Yet...");
            }
            else if (playerNumber == 2)
            {
                UIManager.instance.SetPlayer2PowerUpText("No Powerup Collected Yet...");
            }*/
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
    // co routine to pause 5 second
    IEnumerator Pause()
    {
        
        yield return new WaitForSeconds(15);
        moveWallsOutside = true;

    }
}
