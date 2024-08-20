using UnityEngine;
using System.Collections.Generic;

public class CloudManager : MonoBehaviour
{
    public List<GameObject> cloudPrefabs;
    public float minSpeed = 0.1f, maxSpeed = 0.5f;
    public float minYPosition = -5.0f, maxYPosition = 5.0f;
    public float minXPosition = -10.0f, maxXPosition = 10.0f;
    public float initialSpawnPositionY = 60f;
    public float spawnInterval = 8f;
    public Transform player;

    private List<GameObject> activeClouds = new List<GameObject>();
    private float nextSpawnY;
    private bool initialCloudsSpawned = false;

    void Start()
    {
        nextSpawnY = initialSpawnPositionY;
    }

    void Update()
    {
        // Wait until player passes 60f before spawning clouds
        if (player.position.y >= initialSpawnPositionY && !initialCloudsSpawned)
        {
            // Generate 20 clouds once the player passes the 60f mark
            for (int i = 0; i < 20; i++)
            {
                SpawnCloud();
                nextSpawnY += spawnInterval;
            }
            initialCloudsSpawned = true; // Mark initial clouds as spawned
        }

        // Continue to spawn clouds as the player moves upward
        if (initialCloudsSpawned && player.position.y >= nextSpawnY)
        {
            SpawnCloud();
            nextSpawnY += spawnInterval;
        }

        // Move existing clouds and disable those below the threshold
        for (int i = activeClouds.Count - 1; i >= 0; i--)
        {
            GameObject cloud = activeClouds[i];
            cloud.transform.position += Vector3.right * cloud.GetComponent<CloudMovement>().speed * Time.deltaTime;

            if (cloud.transform.position.x > maxXPosition)
            {
                RepositionCloud(cloud);
            }

            // Disable cloud if it falls below 20f from the player
            if (cloud.transform.position.y < player.position.y - 20f)
            {
                cloud.SetActive(false);
                activeClouds.RemoveAt(i);
            }
        }
    }

    private void SpawnCloud()
    {
        GameObject cloudPrefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Count)];
        Vector3 spawnPosition = new Vector3(Random.Range(minXPosition, maxXPosition), nextSpawnY, 0);
        GameObject cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity, transform);

        // Randomize scale between 1 and 2
        float scale = Random.Range(1f, 2f);
        cloud.transform.localScale = new Vector3(scale, scale, 1f);

        // Assign to a random sorting layer
        SpriteRenderer sr = cloud.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = Random.value > 0.5f ? "cloud1" : "cloud2";

        // Set random movement speed
        cloud.AddComponent<CloudMovement>().speed = Random.Range(minSpeed, maxSpeed);

        // Add the cloud to the active list
        activeClouds.Add(cloud);
    }

    private void RepositionCloud(GameObject cloud)
    {
        cloud.transform.position = new Vector3(minXPosition, Random.Range(minYPosition, maxYPosition) + player.position.y, cloud.transform.position.z);
        cloud.GetComponent<CloudMovement>().speed = Random.Range(minSpeed, maxSpeed);
    }
}

public class CloudMovement : MonoBehaviour
{
    public float speed;
}
