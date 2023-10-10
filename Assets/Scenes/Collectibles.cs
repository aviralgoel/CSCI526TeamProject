using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectiblesscript : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    //bool moving = false;
    public List<GameObject> items = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMyObject", 5);

    }

    // Update is called once per frame

    private float horizontalInput;
    private float verticalInput;
    void Update()
    {
        if (changeDirection())
            horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * 40 * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * 40 * verticalInput);
    }

    IEnumerator changeDirection()
    {
        yield return new WaitforSeconds(3);
        direction.x = Random.Range(-1, 1);
        direction.y = Random.Range(-1, 1);
    }

}
