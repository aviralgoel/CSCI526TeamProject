using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeMe : MonoBehaviour
{
    public bool shaking = false;
    public float shakeAmount = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shaking)
        {
            Vector3 newPos = Random.insideUnitSphere * Time.deltaTime * shakeAmount;
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }
    public void Shake()
    {
        StartCoroutine("ShakeNow");
    }

    IEnumerator ShakeNow()
    {   
        Vector3 originalPos = transform.position;
        if(shaking==false) 
        {
            shaking = true; 
        }
        yield return new WaitForSeconds(0.025f);
        shaking = false;
        transform.position = originalPos;
    }
}
