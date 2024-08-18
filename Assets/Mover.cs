using UnityEngine;

public class CapsuleMover : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += moveSpeed * Time.deltaTime * Vector3.up;
        }
    }
}
