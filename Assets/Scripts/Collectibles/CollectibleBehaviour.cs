using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    public PlayerData playerData;
    public Collectible collectible;
    TopDownCarController tc;

    private void Start()
    {
        playerData = PlayerData.Instance;
        collectible = CollectibleDatabase.GetCollectibleByName(gameObject.name);
        tc = GameObject.Find("Car").GetComponent<TopDownCarController>();

        if (collectible == null)
        {
            Debug.LogWarning("Collectible not found for object: " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collectible != null)
        {
            if (collectible.Type == CollectibleType.Coin)
            {
                HandleCoinCollection();
            }
            else if (collectible.Type == CollectibleType.Gem)
            {
                HandleGemCollection();
            }
            else if (collectible.Type == CollectibleType.Key)
            {
                HandleKeyCollection();
            }

            if (collectible.IsPowerUp)
            {
                HandlePowerUp(collectible.PowerUp);
            }
        }
    }

    private void HandleCoinCollection()
    {
        CollectibleSpawner.spawnedCollectibles.Remove(gameObject);
        Destroy(gameObject);
        // Optionally, you can play a particle effect, sound, or other feedback
    }

    private void HandleGemCollection()
    {
        // Custom behavior for collecting gems
        // Implement as needed
        // Remove the obstacle from the queue
        CollectibleSpawner.spawnedCollectibles.Remove(gameObject); 
        Destroy(gameObject);
    }

    private void HandleKeyCollection()
    {
        // Custom behavior for collecting keys
        // Implement as needed
        CollectibleSpawner.spawnedCollectibles.Remove(gameObject);
        Destroy(gameObject);
    }
    private void HandlePowerUp(PowerUpType powerUpType)
    {
        // You can implement different power-up durations and effects here

        // Apply the power-up effect to the player (for example, speed boost)
        tc.ApplyPowerUpEffect(powerUpType, CollectibleDatabase.GetPowerUp(powerUpType).Duration);
        CollectibleSpawner.spawnedCollectibles.Remove(gameObject);
        Destroy(gameObject);
    }
}

