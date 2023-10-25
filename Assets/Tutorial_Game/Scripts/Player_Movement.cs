using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 direction;
    private Quaternion targetRotation;
    private float turnSpeed = 1f;
    private float turnSpeedMultiplier = 1f; // Declare turnSpeedMultiplier here
    private KeyCode controllingKey;

    public float movementSpeed = 1f;
    public float angleToTurn = 10f;
    public float speedMultiplier = 1f;

    private float defaultTurnSpeedMultiplierValue = 1f;

    private void Start()
    {
    rb = GetComponent<Rigidbody2D>();


        // Determine the controlling key based on the tag of the GameObject.
        controllingKey = gameObject.CompareTag("Player1") ? KeyCode.L : KeyCode.A;
    }

    private void FixedUpdate()
    {
        InputController();
    }

    private void InputController()
    {
        rb.velocity = transform.up * movementSpeed * Time.deltaTime * speedMultiplier;

        if (Input.GetKey(controllingKey))
        {
            direction = Quaternion.Euler(0, 0, angleToTurn * turnSpeed) * transform.up * Time.deltaTime;
            turnSpeed += turnSpeedMultiplier * Time.deltaTime;
        }
        else
        {
            turnSpeed = 1f;
            direction = Quaternion.Euler(0, 0, -angleToTurn * turnSpeed) * transform.up * Time.deltaTime;
        }
        targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

        if (Input.GetKeyUp(controllingKey))
        {
            turnSpeedMultiplier = defaultTurnSpeedMultiplierValue;
        }
    }
}
