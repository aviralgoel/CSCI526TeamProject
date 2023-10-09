using UnityEngine;

public class SpiralMovment : MonoBehaviour
{
    public GameObject blackhole;
    public float movementSpeed;
    Quaternion targetRotation;
    Vector3 direction;

    void Update()
    {
               
        if (!Input.GetKey(KeyCode.Space))
        {
            direction = Time.deltaTime * (blackhole.transform.position - transform.position);
            direction = Quaternion.Euler(0, 0, 75) * direction ;
    
            targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

        }
        else
        {
             direction = Quaternion.Euler(3, 5, 0.5f) * transform.up * Time.deltaTime;
        }
        float distanceThisFrame = movementSpeed * Time.deltaTime;
     
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);        


    }
}

