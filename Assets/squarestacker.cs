using UnityEngine;
using UnityEditor;

public class SquareStackerTool : MonoBehaviour
{
    public GameObject squarePrefab;
    public int numberOfSquares = 1;
    public float spacing = 1.0f; // Spacing between squares

    // Method to stack squares
    public void StackSquares()
    {
        if (squarePrefab == null)
        {
            Debug.LogError("Square Prefab is not assigned!");
            return;
        }

        // Remove existing squares (if any)
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // Stack squares
        for (int i = 0; i < numberOfSquares; i++)
        {
            Vector3 position = new Vector3(0, i * spacing, 0);
            GameObject square = Instantiate(squarePrefab, position, Quaternion.identity, transform);
            square.name = "Square_" + (i + 1);
        }
    }
}

[CustomEditor(typeof(SquareStackerTool))]
public class SquareStackerToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector with the fields
        DrawDefaultInspector();

        SquareStackerTool stackerTool = (SquareStackerTool)target;

        if (GUILayout.Button("Stack Squares"))
        {
            stackerTool.StackSquares();
        }
    }
}
