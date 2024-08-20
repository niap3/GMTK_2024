using UnityEngine;
using System.Collections.Generic;

public class CreviceRandomizer : MonoBehaviour
{
    public List<GameObject> crevicePrefabs;
    public float minVerticalSpacing = 1.0f, maxVerticalSpacing = 5.0f, maxHorizontalOffset = 2.0f;
    public int creviceAhead = 5;

    private float lastGeneratedY;

    public void CreateLoop(float capsuleY)
    {
        while (capsuleY + creviceAhead * maxVerticalSpacing > lastGeneratedY)
            GenerateCrevice();
    }

    private void GenerateCrevice()
    {
        lastGeneratedY += Random.Range(minVerticalSpacing, maxVerticalSpacing);
        float horizontalOffset = Random.Range(-maxHorizontalOffset, maxHorizontalOffset);
        GameObject crevice = Instantiate(crevicePrefabs[Random.Range(0, crevicePrefabs.Count)], 
                                         new Vector3(horizontalOffset, lastGeneratedY, 0), 
                                         transform.rotation, transform);
        crevice.transform.gameObject.AddComponent<VisibilityController>();
    }
}
