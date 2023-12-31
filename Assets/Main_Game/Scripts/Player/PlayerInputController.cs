using JetBrains.Annotations;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    
    public int playerNumber;    
    public Rigidbody2D rb;
    Vector3 direction;
    Quaternion targetRotation;
    public bool isMovementAllowed;
    public float movementSpeed;
    public float freezeMovementSpeed;
    public float normalMovementSpeed;
    public float turnSpeed;
    public float angleToTurn = 10f;
    private ScoreManager scoreManager;
    [Range(1f, 5f)]
    public float speedMultiplier;
    [Range(0f, 2f)]
    public float turnSpeedMultiplier;
    
    KeyCode controllingKey;
    private float defaultTurnSpeedMultiplierValue = 1f;

    // Guiding ArroW
    private bool showArrow = true;
    private float showArrowTimer = 10;
    public float maxScale = 0.09f;
    public float minScale = 0.03f;
    public float scaleSpeed = 0.01f;
    public GameObject turnArrow;

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
        controllingKey = playerNumber == 1 ? KeyCode.L : KeyCode.A  ;
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
        else
        {
            rb.velocity = Vector3.zero;
        }
        

    }
    public void SetIsMovementAllowed(bool isAllowed)
    {
        isMovementAllowed = isAllowed;
    }
    public void FreezeThisPlayer()
    {
        // isMovementAllowed = false;
        // rb.velocity = Vector3.zero;
        movementSpeed = freezeMovementSpeed;
        if(playerNumber == 1)
        {
            UIManager.instance.SetPlayer1PowerUpText("You got frozen!");
        }
        else
        {
            UIManager.instance.SetPlayer2PowerUpText("You got frozen!");
        }
    }
    public void UnFreezeThisPlayer()
    {
        //isMovementAllowed = true;
        //rb.velocity = Vector3.zero;
        movementSpeed = normalMovementSpeed;
        if (playerNumber == 1)
        {
            UIManager.instance.SetPlayer1PowerUpText("");

        }
        else
        {
            UIManager.instance.SetPlayer2PowerUpText("");
        }
    }
    private void Update()
    {   
        if(scoreManager != null)
        {

            //speedMultiplier = (scoreManager.GetTimeActive() < 10f) ? 1f : 1 + scoreManager.GetTimeActive() / 75f;
            //angleToTurn = (scoreManager.GetTimeActive() < 10f) ? 10f : 10f + scoreManager.GetTimeActive() / 20f;


            //speedMultiplier = (scoreManager.GetTimeActive() < 10f) ? 1f : 1 + scoreManager.GetTimeActive() / 75f;
            //angleToTurn = (scoreManager.GetTimeActive() < 10f) ? 10f : 10f + scoreManager.GetTimeActive() / 20f;


            //Debug.Log("Player" +  playerNumber + transform.position);
            //Debug.Log("Player 2 Position: X = " + playerObj2.transform.position.x + " --- Y = " + playerObj2.transform.position.y);
            // print the location of the gameobect THIS script is on
            

        }
        showArrowTimer += Time.deltaTime;
        if(showArrowTimer > 20.0f)
        {
            showArrow = false;
        }
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
            if (showArrow)
            {
                turnArrow.SetActive(true);
                turnArrow.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * Time.deltaTime;
                if (turnArrow.transform.localScale.x > maxScale)
                {
                    turnArrow.transform.localScale = new Vector3(maxScale, maxScale, maxScale);
                }
            }
            
        }
        else
        {
            // direction = transform.position - blackHole.transform.position;
            turnSpeed = 1f;
            direction = Quaternion.Euler(3, 5, -angleToTurn*turnSpeed) * transform.up * Time.deltaTime;
            if (showArrow || turnArrow.activeInHierarchy)
            {
                turnArrow.SetActive(false);
                turnArrow.transform.localScale = new Vector3(minScale, minScale, minScale);
            }
            
        }
        targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        
        if(Input.GetKeyUp(controllingKey))
        {
            turnSpeedMultiplier = defaultTurnSpeedMultiplierValue;
        }
    }

    public void BoostSpeed()
    {
        speedMultiplier = 2f;
        turnSpeedMultiplier = 2f;
        if (playerNumber == 1)
        {
            UIManager.instance.SetPlayer1PowerUpText("Speed Boost!");
        }
        else
        {
            UIManager.instance.SetPlayer2PowerUpText("Speed Boost!");
        }
    }
    public void ResetSpeed()
    {
        speedMultiplier = 1f;
        turnSpeedMultiplier = 1f;
        if (playerNumber == 1)
        {
            UIManager.instance.SetPlayer1PowerUpText("");
        }
        else
        {
            UIManager.instance.SetPlayer2PowerUpText("");
        }
    }
   
}
