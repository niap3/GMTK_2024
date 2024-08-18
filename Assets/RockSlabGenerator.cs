using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RockSlabGenerator : MonoBehaviour
{
    public List<GameObject> rockSlabPrefabs; 
    public int numberOfSlabs = 10; 
    public float minVerticalSpacing = 1.0f;
    public float maxVerticalSpacing = 5.0f;
    public float maxHorizontalOffset = 2.0f;

    private Dictionary<GameObject, float> lastSpawnedPositions = new Dictionary<GameObject, float>();

    // Method to generate rock slabs
    public void GenerateRockSlabs()
    {
        foreach (Transform child in transform)
            DestroyImmediate(child.gameObject);

        lastSpawnedPositions.Clear();
        float currentY = 0;
        for (int i = 0; i < numberOfSlabs; i++)
        {
            GameObject selectedPrefab = rockSlabPrefabs[Random.Range(0, rockSlabPrefabs.Count)];

            float verticalSpacing = Random.Range(minVerticalSpacing, maxVerticalSpacing);
            currentY += verticalSpacing;

            float horizontalOffset = Random.Range(-maxHorizontalOffset, maxHorizontalOffset);

            if (lastSpawnedPositions.TryGetValue(selectedPrefab, out float lastYPosition))
                if (Mathf.Abs(currentY - lastYPosition) < verticalSpacing)
                    currentY = lastYPosition + verticalSpacing;

            GameObject slab = Instantiate(selectedPrefab, transform);
            slab.transform.localPosition = new Vector3(horizontalOffset, currentY, 0);
            lastSpawnedPositions[selectedPrefab] = currentY;
        }
    }
}

[CustomEditor(typeof(RockSlabGenerator))]
public class RockSlabGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RockSlabGenerator slabGenerator = (RockSlabGenerator)target;

        if (GUILayout.Button("Generate Rock Slabs"))
        {
            slabGenerator.GenerateRockSlabs();
        }
    }
}
