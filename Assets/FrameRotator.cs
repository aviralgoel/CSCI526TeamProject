using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRotator : MonoBehaviour
{   
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // roate in the z axis
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
