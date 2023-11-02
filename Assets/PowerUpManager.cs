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


    public PlayerInputController OpponentPlayerController;

    [HeaderAttribute("Fire Wall Mechanic")]
    public float fireWallMovementSpeed = 0.2f;
    public float fireWallDuration = 7f;
    public Transform[] walls;
    public Transform[] wallSources;
    public Transform[] wallDestinations;

    [HeaderAttribute("Freeze Mechanic")]
    [SerializeField]private bool isFrozen = false;
    public float freezeTime = 10f; // Time in seconds for freezing effect
    public float freezeMovementSpeed = 0.2f;
    public ParticleSystem freezeEffect;



    public enum Walls
    {
        Top  = 0,
        TopRight = 1,
        BottomRight = 2,
        Bottom = 3,
        BottomLeft = 4,
        TopLeft = 5

    }


    /*  public Transform wallBottom;
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
      public Transform wallRightBottomSource;*/



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
        // PowerUpControllingKey = (playerNumber == 2) ? KeyCode.Q : KeyCode.P;
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
        //wallBottom.position = Vector3.MoveTowards(wallBottom.position, wallBottomDestination.position, Time.deltaTime * fireWallMovementSpeed);
        //wallLeftBottom.position = Vector3.MoveTowards(wallLeftBottom.position, wallLeftBottomDestination.position, Time.deltaTime*fireWallMovementSpeed);
        //wallLeftTop.position = Vector3.MoveTowards(wallLeftTop.position, wallLeftTopDestination.position, Time.deltaTime*fireWallMovementSpeed);
        //wallRightBottom.position = Vector3.MoveTowards(wallRightBottom.position, wallRightBottomDestination.position, Time.deltaTime*fireWallMovementSpeed);
        //wallRightTop.position = Vector3.MoveTowards(wallRightTop.position, wallRightTopDestination.position, Time.deltaTime*fireWallMovementSpeed);
        //wallTop.position = Vector3.MoveTowards(wallTop.position, wallTopDestination.position, Time.deltaTime*fireWallMovementSpeed);

        // change color of all the walls to red
        //wallBottom.transform.GetComponent<SpriteRenderer>().color = Color.red;
        //wallLeftBottom.transform.GetComponent<SpriteRenderer>().color = Color.red;
        //wallTop.transform.GetComponent<SpriteRenderer>().color = Color.red;
        //wallLeftTop.transform.GetComponent<SpriteRenderer>().color = Color.red;
        //wallRightBottom.transform.GetComponent<SpriteRenderer>().color = Color.red;
        //wallRightTop.transform.GetComponent<SpriteRenderer>().color = Color.red;

        for(int i = 0; i < 6; i++)
        {
            walls[i].position = Vector3.MoveTowards(walls[i].position, wallDestinations[i].position, Time.deltaTime * fireWallMovementSpeed);
            walls[i].transform.GetComponent<SpriteRenderer>().color = Color.red;
        }

        UIManager.instance.SetPlayer1PowerUpText("Avoid Red Walls!");
        UIManager.instance.SetPlayer2PowerUpText("Avoid Red Walls!");
        if (Mathf.Approximately(Vector3.Distance(walls[(int)Walls.Bottom].position, wallDestinations[(int)Walls.Bottom].position),0) &&
            Mathf.Approximately(Vector3.Distance(walls[(int)Walls.Top].position, wallDestinations[(int)Walls.Top].position),0))
        {
            StartCoroutine(Pause());


        }
    }
    private void MoveWallsOutside()
    {   
        // move
        //wallBottom.position = Vector3.MoveTowards(wallBottom.position, wallBottomSource.position, Time.deltaTime * fireWallMovementSpeed );
        //wallLeftBottom.position = Vector3.MoveTowards(wallLeftBottom.position, wallLeftBottomSource.position, Time.deltaTime*fireWallMovementSpeed);
        //wallLeftTop.position = Vector3.MoveTowards(wallLeftTop.position, wallLeftTopSource.position, Time.deltaTime*fireWallMovementSpeed);
        //wallRightBottom.position = Vector3.MoveTowards(wallRightBottom.position, wallRightBottomSource.position, Time.deltaTime * fireWallMovementSpeed);
        //wallRightTop.position = Vector3.MoveTowards(wallRightTop.position, wallRightTopSource.position, Time.deltaTime * fireWallMovementSpeed);
        //wallTop.position = Vector3.MoveTowards(wallTop.position, wallTopSource.position , Time.deltaTime * fireWallMovementSpeed);


        // change color of all the walls to white
        //wallBottom.transform.GetComponent<SpriteRenderer>().color = Color.white;
        //wallLeftBottom.transform.GetComponent<SpriteRenderer>().color = Color.white;
        //wallTop.transform.GetComponent<SpriteRenderer>().color = Color.white;
        //wallLeftTop.transform.GetComponent<SpriteRenderer>().color = Color.white;
        //wallRightBottom.transform.GetComponent<SpriteRenderer>().color = Color.white;
        //wallRightTop.transform.GetComponent<SpriteRenderer>().color = Color.white;

        for(int i = 0; i < 6; i++)
        {
            walls[i].transform.GetComponent<SpriteRenderer>().color = Color.white;
            walls[i].position = Vector3.MoveTowards(walls[i].position, wallSources[i].position, Time.deltaTime * fireWallMovementSpeed);
        }
        if (Mathf.Approximately(Vector3.Distance(walls[(int)Walls.Bottom].position, wallSources[(int)Walls.Bottom].position), 0) &&
            Mathf.Approximately(Vector3.Distance(walls[(int)Walls.Top].position, wallSources[(int)Walls.Top].position), 0) &&
            Mathf.Approximately(Vector3.Distance(walls[(int)Walls.TopLeft].position, wallSources[(int)Walls.TopLeft].position), 0))
        {
            moveWallsOutside = false;
            fireWallActive = false;
        }

        UIManager.instance.SetPlayer1PowerUpText("");
        UIManager.instance.SetPlayer2PowerUpText("");

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


    private void UseFireWalls()
    {
        fireWallActive = true;
        moveWallsInside = true;
    }

    void addPowerUp(PowerUpType type)
    {
        if (powerupsCount[type] <  1)
        {
            powerupsCount[type] = 1;
            totalPowerUpCount = 1;
            if (playerNumber == 1)
            {
                UIManager.instance.SetPlayer1PowerUpText("You picked up " + type.ToString());
            }
            else if (playerNumber == 2)
            {
                UIManager.instance.SetPlayer2PowerUpText("You picked up " + type.ToString());
            }
        }
        
    }
    void removePowerUp(PowerUpType type) 
    {
        if (powerupsCount[type] > 0)
        {
            powerupsCount[type] = 0;
            totalPowerUpCount = 0;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireWalls"))
        {
            addPowerUp(PowerUpType.FireWalls);

            //Debug.Log("we will now search for sound!");
            FindObjectOfType<SoundManager>().Play("PowerUp");

        }
        else if (collision.gameObject.CompareTag("Freeze"))
        {
            addPowerUp(PowerUpType.Freeeze);

            FindObjectOfType<SoundManager>().Play("PowerUp");
            
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            addPowerUp(PowerUpType.Shield);
        }
    }

    // co routine to pause 5 second
    IEnumerator Pause()
    {
        
        yield return new WaitForSeconds(fireWallDuration);
        moveWallsInside = false;
        moveWallsOutside = true;

    }
	private void UseFreeze()
    {
        StartCoroutine(FreezeAndUnfreeze());
    }

    private IEnumerator FreezeAndUnfreeze()
    {
        isFrozen = true;
        OpponentPlayerController.FreezeThisPlayer();
        freezeEffect.Play(true);
        yield return new WaitForSeconds(freezeTime);
        OpponentPlayerController.UnFreezeThisPlayer();
        isFrozen = false;
    }


}