using UnityEngine;

public class SlabVisibilityController : MonoBehaviour
{
    public Camera mainCamera;
    public float destroyDistance = 20.0f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (transform.position.y < mainCamera.transform.position.y - destroyDistance)
        {
            Debug.Log($"{gameObject.name} is below the camera and will be destroyed.");
            Destroy(gameObject);
        }
    }
}