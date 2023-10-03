using UnityEngine;

public class OrbitAroundCircle : MonoBehaviour
{
    public Transform center;  // The center point (circle) around which the object will orbit
    public float radius = 2.0f;  // The radius of the circular path
    public float orbitSpeed = 60.0f;  // The speed of the orbit in degrees per second
    public float steerAngle;
    private float angle = 0.0f;
    public float initialAngle;

    private void Start()
    {
        angle = initialAngle;
    }

    private void Update()
    {
        // Increment the angle based on the orbit speed
        angle += orbitSpeed * Time.deltaTime;

        // Wrap angle to keep it within 0 to 360 degrees
        if (angle >= 360.0f)
        {
            angle -= 360.0f;
        }

        // Calculate the new position based on the current angle and radius
        float x = center.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = center.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Update the object's position
        transform.position = new Vector3(x, y, transform.position.z);

        // Calculate the rotation angle to keep the object facing forward
        float rotationAngle = Mathf.Atan2(center.position.y - y, center.position.x - x) * Mathf.Rad2Deg - steerAngle;

        // Apply the rotation to the object
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

        if(radius==2.5f && Input.GetKeyDown(KeyCode.D)) 
        {
            radius = 3.9f; return;
        }
        if (radius == 3.9f && Input.GetKeyDown(KeyCode.A))
        {
            radius = 2.5f; return;
        }
        if(radius ==3.9f && Input.GetKeyDown(KeyCode.D))
        {
            radius = 5.3f; return;
        }  
        if(radius == 5.3f && Input.GetKeyDown(KeyCode.A))
        {
            radius = 3.9f; return;
        }
    }
}
