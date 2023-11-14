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
    [SerializeField] private ScoreManager scoreManager;


    public PlayerInputController OpponentPlayerController;

    public int scoreOnPowerUp = 2;

    [HideInInspector] public int numOfFireWallHitByPlayer = 0;
    [HideInInspector] public int numOfFreezeHitByPlayer = 0;

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






    public bool fireWallActive = false;
    public bool moveWallsInside = false;
    public bool moveWallsOutside = false;

    // enum for powerup types
    public enum PowerUpType
    {
        FireWalls, 
        Freeze,
        Missiles
    }
    // create a hashmap to store the powerups of size 3 element with value 0
    public Dictionary<PowerUpType, int> powerupsCount = new Dictionary<PowerUpType, int>()
    {
        {PowerUpType.FireWalls, 0},
        {PowerUpType.Freeze, 0},
        {PowerUpType.Missiles, 0}
    };
   
    // Start is called before the first frame update
    void Start()
    {   
        scoreManager = GetComponent<ScoreManager>();
        playerNumber = scoreManager.GetPlayerNumber();
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
        if (powerupsCount[PowerUpType.Freeze] > 0)
        {
            UseFreeze();
            removePowerUp(PowerUpType.Freeze);
            numOfFreezeHitByPlayer++;
        }
        else if (powerupsCount[PowerUpType.Missiles] > 0)
        {
            UseMissiles();
            removePowerUp(PowerUpType.Missiles);
        }
        else if (powerupsCount[PowerUpType.FireWalls] > 0)
        {
            UseFireWalls();
            removePowerUp(PowerUpType.FireWalls);
            numOfFireWallHitByPlayer++;
        }
    }


    private void UseFireWalls()
    {
        fireWallActive = true;
        moveWallsInside = true;
    }

    public void addPowerUp(PowerUpType type)
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
            scoreManager.IncrementScore(scoreOnPowerUp);
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

   /* private void OnTriggerEnter2D(Collider2D collision)
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
    }*/

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

    public GameObject Missiles;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject HexagonPlayground;
    Vector3 playGroundExtendMin;
    Vector3 playGroundExtendMax;
    SpriteRenderer sr;
    private void UseMissiles() {

        sr = HexagonPlayground.GetComponent<SpriteRenderer>();
        Debug.Log("Use Missiles");
        Vector3 randomSpawn = new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), UnityEngine.Random.Range(-2.5f, 2.5f), 0);
        if(this.gameObject.CompareTag("Player1")) {
            GameObject missile = Instantiate(Missiles, randomSpawn, Quaternion.identity);
            missile.GetComponent<HomingMissile>().target = Player2.transform;
        } else {
            GameObject missile = Instantiate(Missiles, randomSpawn, Quaternion.identity);
            missile.GetComponent<HomingMissile>().target = Player1.transform;
        }
    }


}