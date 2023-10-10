using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMovment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject blackhole;
    private float horizontalInput;
    private float verticalInput;
    public float movementSpeed;
    Vector3 direction;
    // Update is called once per frame
    void Update()
    {
        //verticalInput = Input.GetAxis("Vertical");
        //horizontalInput = Input.GetAxis("Horizontal");
        //Vector3 direction = blackhole.transform.position - transform.position;
        
        if (!Input.GetKey(KeyCode.Space))
        {
            direction = Time.deltaTime * (blackhole.transform.position - transform.position);
            direction = Quaternion.Euler(0, 0, 0.05f) * direction ;

        }
        else
        {
             direction = Quaternion.Euler(0, 0, 0.5f) * transform.up * Time.deltaTime;
        }
        float distanceThisFrame = movementSpeed * Time.deltaTime;
     
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        //transform.Translate(Vector3.right * Time.deltaTime * 12 * horizontalInput);
        //transform.Translate(Vector3. up * Time.deltaTime * 12 * verticalInput);

        // make the object z axis point towards the direction its moving
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        // if space key is pressed
             
        
    }
}
