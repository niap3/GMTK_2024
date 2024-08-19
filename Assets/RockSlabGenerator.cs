using UnityEngine;
using System.Collections.Generic;

public class RockSlabGenerator : MonoBehaviour
{
    public List<GameObject> rockSlabPrefabs;
    public float minVerticalSpacing = 1.0f, maxVerticalSpacing = 5.0f, maxHorizontalOffset = 2.0f;
    public int slabsAhead = 5;

    private float lastGeneratedY;
    private readonly List<GameObject> spawnedSlabs = new();

    public void GenerateSlabsIfNeeded(float capsuleY)
    {
        while (capsuleY + slabsAhead * maxVerticalSpacing > lastGeneratedY)
            GenerateSlab();
    }

    private void GenerateSlab()
    {
        float verticalSpacing = Random.Range(minVerticalSpacing, maxVerticalSpacing);
        lastGeneratedY += verticalSpacing;
        float horizontalOffset = Random.Range(-maxHorizontalOffset, maxHorizontalOffset);
        GameObject slab = Instantiate(rockSlabPrefabs[Random.Range(0, rockSlabPrefabs.Count)], new Vector3(horizontalOffset, lastGeneratedY, 0), transform.rotation, transform);
        slab.transform.GetChild(0).gameObject.AddComponent<VisibilityController>();
        spawnedSlabs.Add(slab);
    }
}
