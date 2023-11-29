
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Movement : MonoBehaviour
{
    int playerNumber;
    Rigidbody2D rb;
    Vector3 direction;
    Quaternion targetRotation;

    public bool isMovementAllowed;
    private bool canMove = true;
    private bool canMoveDuringCoroutine = true;

    public float movementSpeed;
    public float turnSpeed;
    public float angleToTurn = 10f;
    [Range(1f, 5f)] public float speedMultiplier;
    [Range(0f, 2f)] public float turnSpeedMultiplier;

    KeyCode controllingKey;
    private float defaultTurnSpeedMultiplierValue = 1f;

    public Slider respawnSliderPrefab;
    Vector3 respawnPosition;

    void Start()
    {
        playerNumber = (gameObject.tag == "Player1") ? 1 : 2;
        rb = GetComponent<Rigidbody2D>();
        controllingKey = (playerNumber == 1) ? KeyCode.L : KeyCode.A;
        rb.velocity = Vector3.zero;
        isMovementAllowed = false;
        canMove = true;
        canMoveDuringCoroutine = true;
        speedMultiplier = 1f;
        turnSpeedMultiplier = 1f;
        turnSpeed = 1f;

        respawnPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (canMove && isMovementAllowed && canMoveDuringCoroutine)
        {
            InputController();
        }
    }

    private void Update()
    {
        if (Input.GetKey(controllingKey) && !isMovementAllowed)
        {
            isMovementAllowed = true;
            canMove = true;
            FindObjectOfType<SoundManager>().Play("button");
        }
    }

    private void InputController()
    {
        rb.velocity = transform.up * movementSpeed * Time.deltaTime * speedMultiplier;
        if (Input.GetKey(controllingKey))
        {
            direction = Quaternion.Euler(3, 5, angleToTurn * turnSpeed) * transform.up * Time.deltaTime;
            turnSpeed += turnSpeedMultiplier * Time.deltaTime;
        }
        else
        {
            turnSpeed = 1f;
            direction = Quaternion.Euler(3, 5, -angleToTurn * turnSpeed) * transform.up * Time.deltaTime;
        }
        targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

        if (Input.GetKeyUp(controllingKey))
        {
            turnSpeedMultiplier = defaultTurnSpeedMultiplierValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Blackhole"))
        {

            transform.position = respawnPosition;

            FindObjectOfType<SoundManager>().Play("playerdeath");

            StartCoroutine(RespawnAfterDelay(5f));
            canMove = false;
            canMoveDuringCoroutine = false;

        }
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        transform.position = respawnPosition;
        isMovementAllowed = false;
        rb.velocity = Vector3.zero;

        float countdown = delay;
        respawnSliderPrefab.gameObject.SetActive(true);
        respawnSliderPrefab.value = countdown;

        while (countdown > 0)
        {
            Debug.Log(countdown);
            yield return new WaitForSeconds(1f);
            countdown--;
            respawnSliderPrefab.value = countdown;
        }

        respawnSliderPrefab.gameObject.SetActive(false);
        isMovementAllowed = true;
        canMove = true;
        canMoveDuringCoroutine = true;
    }
}
