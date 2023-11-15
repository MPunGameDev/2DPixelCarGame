using UnityEngine;
using System.Collections.Generic;

public class Collectible
{
    public string Name { get; private set; }
    public Sprite Sprite { get; private set; }
    public CollectibleType Type { get; private set; }
    public int Value { get; private set; }
    public bool IsPowerUp { get; private set; }
    public PowerUpType PowerUp { get; private set; }
    public float Duration { get; private set; }
    public float Rarity { get; private set; }
    public float BuffValue { get; private set; }

    public Collectible(string name, Sprite sprite, PowerUpType powerUp, float duration, float rarity, float buffValue, int value = 0, CollectibleType type = CollectibleType.Custom, bool isPowerUp = true)
    {
        Name = name;
        Sprite = sprite;
        Type = type;
        Value = value;
        IsPowerUp = isPowerUp;
        PowerUp = powerUp;
        Duration = duration;
        Rarity = rarity;
        BuffValue = buffValue;
    }

    public Collectible(string name, Sprite sprite, CollectibleType type, int value, float rarity, bool isPowerUp = false, PowerUpType powerUp = PowerUpType.None, float buffValue = 0f)
    {
        Name = name;
        Sprite = sprite;
        Type = type;
        Value = value;
        IsPowerUp = isPowerUp;
        PowerUp = powerUp;
        Rarity = rarity;
        BuffValue = buffValue;
    }
}

public enum CollectibleType
{
    Coin,
    Gem,
    Key,
    Custom // You can add more types as needed
}

public enum PowerUpType
{
    None,
    SpeedBoost,
    Invincibility,
    HealthPack,
    Custom // Add more power-up types as needed
}

public class CollectibleDatabase
{
    private static Dictionary<CollectibleType, Collectible> collectibleDictionary = new Dictionary<CollectibleType, Collectible>
    {
        {
            CollectibleType.Coin,
            new Collectible("Coin", Resources.Load<Sprite>("Sprites/Collectibles/Golden_Coin"), CollectibleType.Coin, 10, 10f)
        },
        {
            CollectibleType.Gem,
            new Collectible("Gem", Resources.Load<Sprite>("Sprites/Collectibles/Cut_Ruby"), CollectibleType.Gem, 50, 5f)
        },
        {
            CollectibleType.Key,
            new Collectible("Key", Resources.Load<Sprite>("Sprites/Collectibles/Golden_Key"), CollectibleType.Key, 0, 1f)
        }
    };

    private static Dictionary<PowerUpType, Collectible> powerUpDictionary = new Dictionary<PowerUpType, Collectible>
    {
        {
            PowerUpType.SpeedBoost,
            new Collectible("Speed Boost", Resources.Load<Sprite>("Sprites/PowerUps/Speed_Boost"), PowerUpType.SpeedBoost, 10f, 3f, 2f)
        },
        {
            PowerUpType.Invincibility,
            new Collectible("Invincibility", Resources.Load<Sprite>("Sprites/PowerUps/Invincibility"),PowerUpType.Invincibility, 10f, 5f, 0f)
        },
        {
            PowerUpType.HealthPack,
            new Collectible("Health Pack", Resources.Load<Sprite>("Sprites/PowerUps/Health_Pack"),PowerUpType.HealthPack, 10f, 8f, 20f)
        }
    };

    public static Collectible GetCollectible(CollectibleType type)
    {
        if (collectibleDictionary.ContainsKey(type))
        {
            return collectibleDictionary[type];
        }
        else
        {
            Debug.LogError("Collectible type not found: " + type);
            return null; // Or return a default collectible
        }
    }

    public static Collectible GetPowerUp(PowerUpType type)
    {
        if (powerUpDictionary.ContainsKey(type))
        {
            return powerUpDictionary[type];
        }
        else
        {
            Debug.LogError("Power-up type not found: " + type);
            return null; // Or return a default power-up
        }
    }

    public static Collectible GetCollectibleByName(string collectibleName)
    {
        foreach (var collectible in collectibleDictionary.Values)
        {
            if (collectible.Name == collectibleName)
            {
                return collectible;
            }
        }

        foreach (var collectible in powerUpDictionary.Values)
        {
            if (collectible.Name == collectibleName)
            {
                return collectible;
            }
        }

        return null; 
    }

    public static List<Collectible> GetCollectibles()
    {
        List<Collectible> collectibles = new List<Collectible>();
        foreach (var kvp in powerUpDictionary)
        {
            collectibles.Add(kvp.Value);
        }
        foreach (var cl in collectibleDictionary)
        {
            collectibles.Add(cl.Value);
        }
        return collectibles;
    }


}
