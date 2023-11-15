using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public PlayerData playerData;
    public Obstacle obstacle;
    // Start is called before the first frame update
    void Start()
    {
        obstacle = ObstacleDatabase.GetObstacleByName(gameObject.name);
        playerData = PlayerData.Instance;

        if (obstacle == null)
        {
            Debug.LogWarning("Obstacle not found for object: " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (obstacle != null)
        {
            if (obstacle.ObstacleType == ObstacleType.PotHole)
            {
                HandlePotHoleCollision();
            }
        }
    }

    private void HandlePotHoleCollision()
    {
        Debug.Log("PotHole Collision");
    }
}
