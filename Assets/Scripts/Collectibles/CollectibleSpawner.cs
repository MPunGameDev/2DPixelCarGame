using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    private readonly float spawnDistance = 20f; // Distance ahead of the player
    private readonly float collectibleDestroyDistance = 20f; // Distance behind the player to destroy collectibles
    public static List<GameObject> spawnedCollectibles = new(); // List to manage spawned collectibles

    private void Update()
    {
        // Check if the list is empty or the first collectible is far enough ahead to spawn another
        if (spawnedCollectibles.Count == 0 || spawnedCollectibles[0].transform.position.y < transform.position.y - collectibleDestroyDistance)
        {
            // Change the collectible name you want to spawn here
            string collectibleNameToSpawn = ChooseCollectibleUpToSpawn();
            SpawnCollectibleAhead(collectibleNameToSpawn); // CHANGE THIS BACK
        }

        // Destroy collectibles behind the player
        DestroyCollectiblesBehind();
    }

    private void SpawnCollectibleAhead(string collectibleName)
    {
        Vector3 playerPosition = transform.position; // Player's current position
        Debug.Log(playerPosition);
        Vector3 spawnPosition = new(playerPosition.x, playerPosition.y + spawnDistance, -1); // Calculate spawn point ahead of the player
        Debug.Log(spawnPosition);
        Vector3 spawnScale = new(0.5f, 0.5f, 1); // Scale Vector for Collectible sizing

        Collectible collectibleToSpawn = CollectibleDatabase.GetCollectibleByName(collectibleName);
        if (collectibleToSpawn != null)
        {
            GameObject collectible = new(collectibleToSpawn.Name);

            // Adding components to GameObject
            collectible.AddComponent<SpriteRenderer>().sprite = collectibleToSpawn.Sprite;
            collectible.AddComponent<PolygonCollider2D>().isTrigger = true;
            collectible.AddComponent<CollectibleBehaviour>();


            // Setting Collectible Scale
            collectible.transform.localScale = spawnScale;
            collectible.transform.position = spawnPosition;

            spawnedCollectibles.Add(collectible); // Add the collectible to the list
        }
    }

    private void DestroyCollectiblesBehind()
    {
        for (int i = spawnedCollectibles.Count - 1; i >= 0; i--)
        {
            GameObject collectible = spawnedCollectibles[i];
            if (collectible.transform.position.y < transform.position.y - collectibleDestroyDistance)
            {
                spawnedCollectibles.RemoveAt(i); // Remove the collectible from the list
                if (collectible != null)
                {
                    Destroy(collectible);
                }
            }
        }
    }

    private string ChooseCollectibleUpToSpawn()
    {
        List<string> availablePowerUps = new();

        foreach (var powerUp in CollectibleDatabase.GetCollectibles())
        {
            for (int i = 0; i < powerUp.Rarity; i++)
            {
                availablePowerUps.Add(powerUp.Name);
            }
        }

        // Select a random PowerUp from the list of available PowerUps
        if (availablePowerUps.Count > 0)
        {
            int randomIndex = Random.Range(0, availablePowerUps.Count);
            return availablePowerUps[randomIndex];
        }

        // If no PowerUps are available, return a default PowerUp name
        return "DefaultPowerUp";
    }
}

