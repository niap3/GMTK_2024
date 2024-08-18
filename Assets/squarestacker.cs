using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RectTransformTileStackerTool : MonoBehaviour
{
    public List<GameObject> tilePrefabs; 
    public int numberOfTiles = 1;
    public float rectHeight = 15.0f;

    public void StackTiles()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        float cumulativeHeight = 0.0f;

        for (int i = 0; i < numberOfTiles; i++)
        {
            GameObject selectedPrefab = tilePrefabs[Random.Range(0, tilePrefabs.Count)];
            Debug.Log(selectedPrefab.name + cumulativeHeight);
            Vector3 position = new(0, cumulativeHeight, 0);
            var currentInstance = Instantiate(selectedPrefab, position, Quaternion.identity);
            currentInstance.transform.SetParent(transform, true);
            cumulativeHeight += rectHeight;
        }
    }
}

[CustomEditor(typeof(RectTransformTileStackerTool))]
public class RectTransformTileStackerToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RectTransformTileStackerTool stackerTool = (RectTransformTileStackerTool)target;

        if (GUILayout.Button("Stack Tiles"))
        {
            stackerTool.StackTiles();
        }
    }
}
