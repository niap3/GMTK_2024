using UnityEngine;
using System.Collections.Generic;

public class CloudManager : MonoBehaviour
{
    public List<GameObject> cloudPrefabs;
    public float minSpeed = 0.1f, maxSpeed = 0.5f;
    public float minXPosition = -10.0f, maxXPosition = 10.0f;
    public float initialSpawnPositionY = 60f;
    public float spawnInterval = 8f;
    public Transform player;

    private float nextSpawnY;

    void Start()
    {
        nextSpawnY = initialSpawnPositionY;
    }

    void Update()
    {
        // Continuously spawn clouds above the player after passing the 60f mark
        if (player.position.y >= initialSpawnPositionY)
        {
            while (nextSpawnY < player.position.y + 20f) // Keep clouds 20f ahead of the player
            {
                SpawnCloud();
            }
        }

        // Move existing clouds and disable those below a threshold
        foreach (Transform cloud in transform)
        {
            cloud.position += Vector3.right * cloud.GetComponent<CloudMovement>().speed * Time.deltaTime;

            // Disable cloud if it falls below 20f from the player
            if (cloud.position.y < player.position.y - 20f)
            {
                cloud.gameObject.SetActive(false);
            }
        }
    }

    private void SpawnCloud()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minXPosition, maxXPosition), nextSpawnY, 0);
        GameObject cloud = Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Count)], spawnPosition, Quaternion.identity, transform);

        float scale = Random.Range(1f, 2f);
        cloud.transform.localScale = new Vector3(scale, scale, 1f);

        SpriteRenderer sr = cloud.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = Random.value > 0.5f ? "cloud1" : "cloud2";

        cloud.AddComponent<CloudMovement>().speed = Random.Range(minSpeed, maxSpeed);

        nextSpawnY += spawnInterval;
    }
}
