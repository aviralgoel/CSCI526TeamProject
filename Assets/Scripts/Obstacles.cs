using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private float moveSpeed = 0.5f; // Adjust the speed as needed
    private float minY = -1.5f; // The minimum Y position
    private float maxY = 0.5f; // The maximum Y position
    public string[] tagsToMove = { "SpikeR", "SpikeL" };

    private bool movingUp = true;

    void Update()
    {
        // Iterate through the tags we want to move
        foreach (string tag in tagsToMove)
        {
            GameObject[] spikeObjects = GameObject.FindGameObjectsWithTag(tag);

            // Move each GameObject with the specified tag
            foreach (GameObject spike in spikeObjects)
            {
                MoveObject(spike);
            }
        }
    }

    void MoveObject(GameObject obj)
    {
        Vector3 newPosition = obj.transform.position;
        float step = moveSpeed * Time.deltaTime;

        if (movingUp)
        {
            newPosition.y += step;
            if (newPosition.y >= maxY)
            {
                newPosition.y = maxY;
                movingUp = false;
            }
        }
        else
        {
            newPosition.y -= step;
            if (newPosition.y <= minY)
            {
                newPosition.y = minY;
                movingUp = true;
            }
        }

        obj.transform.position = newPosition;
    }
}
