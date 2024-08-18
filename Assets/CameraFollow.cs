using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Assign the capsule GameObject here in the Inspector
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        // The desired position is directly above the target's position with the specified offset
        Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y, transform.position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
