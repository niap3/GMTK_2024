using UnityEngine;

public class VisibilityController : MonoBehaviour
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
            gameObject.SetActive(false);
        }
    }
}