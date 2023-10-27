using JetBrains.Annotations;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public int playerNumber;
    Rigidbody2D rb;
    Vector3 direction;
    Quaternion targetRotation;
    public bool isMovementAllowed;
    public float movementSpeed;
    public float turnSpeed;
    public float angleToTurn = 10f;
    private ScoreManager scoreManager;
    [Range(1f, 5f)]
    public float speedMultiplier;
    [Range(0f, 2f)]
    public float turnSpeedMultiplier;
    
    KeyCode controllingKey;
    private float defaultTurnSpeedMultiplierValue = 1f;

    //public GameObject blackHole;

    private void Awake()
    {
        isMovementAllowed = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player1")
        {
            playerNumber = 1;
        }
        else if (gameObject.tag == "Player2")
        {
            playerNumber = 2;
        }
        rb = GetComponent<Rigidbody2D>();
        controllingKey = playerNumber == 1 ? KeyCode.L : KeyCode.A;
        rb.velocity = Vector3.zero;
        speedMultiplier = 1f;
        turnSpeedMultiplier = 1f;
        turnSpeed = 1f;
        scoreManager = GetComponent<ScoreManager>();
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {   
        if(isMovementAllowed)
        {
            InputController();
        }
        

    }
    public void SetIsMovementAllowed(bool isAllowed)
    {
        isMovementAllowed = isAllowed;
        //Debug.Log("Player movement active");
    }
    public void FreezeThisPlayer()
    {
        isMovementAllowed = false;
        rb.velocity = Vector3.zero;
        if(playerNumber == 1)
        {
            UIManager.instance.SetPlayer1PowerUpText("You got frozen!");

        }
        else
        {
            UIManager.instance.SetPlayer2PowerUpText("You got frozen!");
        }
        //Debug.Log("Player movement freezed");
    }
    public void UnFreezeThisPlayer()
    {
        isMovementAllowed = true;
        if (playerNumber == 1)
        {
            UIManager.instance.SetPlayer1PowerUpText("");

        }
        else
        {
            UIManager.instance.SetPlayer2PowerUpText("");
        }
        Debug.Log("Player movement unfreezed");
    }
    private void Update()
    {
        speedMultiplier = (scoreManager.GetTimeActive() < 10f) ? 1f : 1 + scoreManager.GetTimeActive() / 75f;
        angleToTurn = (scoreManager.GetTimeActive() < 10f) ? 10f : 10f + scoreManager.GetTimeActive() / 20f;
        //defaultTurnSpeedMultiplierValue = (scoreManager.GetTimeActive() < 10f) ? 1f : 1 + scoreManager.GetTimeActive() / 10f;
    }


    private void InputController()
    {   
        // turning and moving the player always to its right
        // using forces (and not trasnform.Translate())
      
        rb.velocity = transform.up * movementSpeed * Time.deltaTime * speedMultiplier;
        if (Input.GetKey(controllingKey))
        {
           
            direction = Quaternion.Euler(3, 5, angleToTurn * turnSpeed) * transform.up * Time.deltaTime;
            turnSpeed += turnSpeedMultiplier * Time.deltaTime;
        }
        else
        {
            // direction = transform.position - blackHole.transform.position;
            turnSpeed = 1f;
            direction = Quaternion.Euler(3, 5, -angleToTurn*turnSpeed) * transform.up * Time.deltaTime;
        }
        targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        
        if(Input.GetKeyUp(controllingKey))
        {
            turnSpeedMultiplier = defaultTurnSpeedMultiplierValue;
        }
    }
   
}