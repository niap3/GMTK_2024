using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RockSlabGenerator : MonoBehaviour
{
    public List<GameObject> rockSlabPrefabs;  // List of different rock slab prefabs
    public int numberOfSlabs = 10;  // Number of rock slabs to generate
    public float minVerticalSpacing = 1.0f;  // Minimum vertical spacing between slabs
    public float maxVerticalSpacing = 5.0f;  // Maximum vertical spacing between slabs
    public float maxHorizontalOffset = 2.0f;  // Maximum horizontal offset from the vertical axis

    private Dictionary<GameObject, float> lastSpawnedPositions = new Dictionary<GameObject, float>();

    // Method to generate rock slabs
    public void GenerateRockSlabs()
    {
        if (rockSlabPrefabs == null || rockSlabPrefabs.Count == 0)
        {
            Debug.LogError("Rock Slab Prefabs are not assigned!");
            return;
        }

        // Clear existing slabs
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
        lastSpawnedPositions.Clear();

        float currentY = 0;

        for (int i = 0; i < numberOfSlabs; i++)
        {
            // Select a random rock slab prefab
            GameObject selectedPrefab = rockSlabPrefabs[Random.Range(0, rockSlabPrefabs.Count)];

            // Calculate a random vertical position
            float verticalSpacing = Random.Range(minVerticalSpacing, maxVerticalSpacing);
            currentY += verticalSpacing;

            // Calculate a random horizontal offset
            float horizontalOffset = Random.Range(-maxHorizontalOffset, maxHorizontalOffset);

            // Check if the same type of slab is overlapping
            if (lastSpawnedPositions.TryGetValue(selectedPrefab, out float lastYPosition))
            {
                if (Mathf.Abs(currentY - lastYPosition) < verticalSpacing)
                {
                    // If it overlaps, adjust the currentY to prevent overlap
                    currentY = lastYPosition + verticalSpacing;
                }
            }

            // Instantiate the rock slab
            Vector3 localPosition = new Vector3(horizontalOffset, currentY, 0);
            GameObject slab = Instantiate(selectedPrefab, transform);
            slab.transform.localPosition = localPosition; // Set the local position relative to the parent
            slab.name = "RockSlab_" + (i + 1);

            // Update the last spawned position for this prefab
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
