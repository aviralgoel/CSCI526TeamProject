using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyText : MonoBehaviour
{
    // Start is called before the first frame update
    private float second = 1f;
    void Start()
    {
        Destroy(gameObject, second);
    }
}
