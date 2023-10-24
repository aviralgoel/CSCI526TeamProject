using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class particletrail : MonoBehaviour
{
    public Transform objectToFollow;
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (objectToFollow != null)
        {
            transform.position = objectToFollow.position;
        }
    }
}
