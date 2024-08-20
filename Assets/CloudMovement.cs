using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed; // The speed at which the cloud moves horizontally

    void Update()
    {
        // Move the cloud horizontally based on its speed
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}