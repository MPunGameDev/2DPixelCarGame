using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //public GameObject prefabToSpawn;
    private readonly float spawnDistance = 10f; // Distance ahead of the player
    private readonly float obstacleDestroyDistance = 10f; // Distance behind the player to destroy obstacles
    public static List<GameObject> spawnedObstacles = new(); // List to manage spawned collectibles
    private bool canSpawn = true; // Flag to control spawning

    private void Update()
    {
        // Check if the queue is empty or the first obstacle is far enough ahead to spawn another
        if (canSpawn && (spawnedObstacles.Count == 0 || spawnedObstacles[0].transform.position.y < transform.position.y + spawnDistance))
        {
            // Change the obstacle name you want to spawn here
            string obstacleNameToSpawn = ChooseObstacleUpToSpawn();
            SpawnObstacleAhead(obstacleNameToSpawn);

            canSpawn = false; // Set the flag to prevent further spawning
        }

        // Destroy obstacles behind the player
        DestroyObstaclesBehind();
    }

    private void SpawnObstacleAhead(string obstacleName)
    {
        Vector3 playerPosition = transform.position; // Player's current position
        Vector3 spawnPosition = new(playerPosition.x, playerPosition.y + spawnDistance, -1); // Calculate spawn point ahead of the player

        Obstacle obstacleToSpawn = ObstacleDatabase.GetObstacleByName(obstacleName);
        if (obstacleToSpawn != null)
        {
            GameObject obstacle = new(obstacleToSpawn.Name);
            obstacle.AddComponent<SpriteRenderer>().sprite = obstacleToSpawn.Sprite;
            obstacle.AddComponent<ObstacleBehaviour>();

            if (obstacleToSpawn.IsCollidable)
            {
                obstacle.AddComponent<PolygonCollider2D>();
            }
            else
            {
                obstacle.AddComponent<PolygonCollider2D>().isTrigger = true;
            }

            obstacle.transform.position = spawnPosition;

            spawnedObstacles.Add(obstacle); // Add the obstacle to the list
        }
    }

    private void DestroyObstaclesBehind()
    {
        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = spawnedObstacles[i];
            if (obstacle.transform.position.y < transform.position.y - obstacleDestroyDistance)
            {
                spawnedObstacles.RemoveAt(i); // Remove the obstacle from the list
                if (obstacle != null)
                {
                    Destroy(obstacle);
                    ObstaclePassed();
                }
            }
        }
    }

    // Add a method to reset the canSpawn flag when the obstacle is passed
    public void ObstaclePassed()
    {
        canSpawn = true;
    }

    private string ChooseObstacleUpToSpawn()
    {
        List<string> availableObstacles = new();

        foreach (var obstacle in ObstacleDatabase.GetObstacles())
        {
            for (int i = 0; i < obstacle.Rarity; i++)
            {
                availableObstacles.Add(obstacle.Name);
            }
        }

        // Select a random PowerUp from the list of available PowerUps
        if (availableObstacles.Count > 0)
        {
            int randomIndex = Random.Range(0, availableObstacles.Count);
            return availableObstacles[randomIndex];
        }

        // If no PowerUps are available, return a default PowerUp name
        return "DefaultObstacle";
    }
}






