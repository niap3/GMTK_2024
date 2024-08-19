using UnityEngine;
using System.Collections.Generic;

public class BGTileStacker : MonoBehaviour
{
    public List<GameObject> tilePrefabs;
    public int numberOfTiles = 1, stacksAhead = 2;
    public float rectHeight = 15.0f;

    private float lastGeneratedY;
    private readonly List<GameObject> spawnedStacks = new();

    public void GenerateStacksIfNeeded(float capsuleY)
    {
        while (capsuleY + stacksAhead * rectHeight * numberOfTiles > lastGeneratedY)
            GenerateStack();
    }

    private void GenerateStack()
    {
        float cumulativeHeight = lastGeneratedY;
        for (int i = 0; i < numberOfTiles; i++)
        {
            GameObject stack = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Count)], 
                                           new Vector3(0, cumulativeHeight, 0), Quaternion.identity, transform);
            stack.transform.GetChild(0).gameObject.AddComponent<VisibilityController>();
            cumulativeHeight += rectHeight;
            spawnedStacks.Add(stack);
        }
        lastGeneratedY += rectHeight * numberOfTiles;
    }
}
