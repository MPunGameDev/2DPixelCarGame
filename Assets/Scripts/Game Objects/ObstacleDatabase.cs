using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle
{
    public string Name { get; private set; }
    public int Damage { get; private set; }
    public Sprite Sprite { get; private set; }
    public bool IsCollidable { get; private set; }
    public float Rarity { get; private set; }
    public ObstacleType ObstacleType { get; private set; }

    public Obstacle(string name, int damage, Sprite sprite, bool isCollidable, float rarity, ObstacleType obstacleType)
    {
        Name = name;
        Damage = damage;
        Sprite = sprite;
        IsCollidable = isCollidable;
        Rarity = rarity;
        ObstacleType = obstacleType;
    }
}

public enum ObstacleType
{
    PotHole,
    Tree,
}

public static class ObstacleDatabase
{

    private static Dictionary<ObstacleType, Obstacle> obstacleDictionary = new Dictionary<ObstacleType, Obstacle>
    {
        {
            ObstacleType.PotHole,
            new Obstacle("PotHole", 5, Resources.Load<Sprite>("Sprites/Pot_Hole"), true, 10f, ObstacleType.PotHole)
        },
        {
            ObstacleType.Tree,
            new Obstacle("Tree", 5, Resources.Load<Sprite>("Sprites/Tree"), false, 3f, ObstacleType.Tree)
        }
    };

    public static Obstacle GetObstacle(ObstacleType obstacleType)
    {
        if (obstacleDictionary.ContainsKey(obstacleType))
        {
            return obstacleDictionary[obstacleType];
        }
        else
        {
            Debug.LogError("Obstacle type not found: " + obstacleType);
            return null; // Or return a default obstacle
        }
    }

    public static Obstacle GetObstacleByName(string obstacleName)
    {
        foreach (var obstacle in obstacleDictionary.Values)
        {
            if (obstacle.Name == obstacleName)
            {
                return obstacle;
            }
        }
        Debug.LogError("Obstacle name not found: " + obstacleName);
        return null; // Or return a default obstacle
    }

    public static List<Obstacle> GetObstacles()
    {
        List<Obstacle> obstacles = new List<Obstacle>();
        foreach (var kvp in obstacleDictionary)
        {
            obstacles.Add(kvp.Value);
        }
        return obstacles;
    }
}

