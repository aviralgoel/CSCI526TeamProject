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
    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = blackhole.transform.position - transform.position;
        direction = Quaternion.Euler(0, 0, 75) * direction;
        float distanceThisFrame = 10 * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.Translate(Vector3.right * Time.deltaTime * 12 * horizontalInput);
        transform.Translate(Vector3. up * Time.deltaTime * 12 * verticalInput);
    }
}
