using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles
{

}

public class CarManager : MonoBehaviour
{
    // Define car types as enums for easier reference
    public enum CarType
    {
        RedSportCar,
        RedSuperCar,
        BlueF1Car,
    }

    // Create a dictionary to store car attributes based on the car type
    private static Dictionary<CarType, Car> carDictionary = new Dictionary<CarType, Car>
    {
        {
            CarType.RedSportCar,
            new Car("Red Sport Car", 5, 10, 15, 100, "Sprites/Vehicles/Red_Sport_Car")
        },
        {
            CarType.RedSuperCar,
            new Car("Red Super Car", 2, 5, 35, 100, "Sprites/Vehicles/Red_Super_Car")
        },
        {
            CarType.BlueF1Car,
            new Car("Blue F1 Car", 10, 20, 50, 100, "Sprites/Vehicles/Blue_F1_Car")
        },
        // Add more car types here as needed
    };

    // Method to get a specific car by type
    public static Car GetCar(CarType carType)
    {
        if (carDictionary.ContainsKey(carType))
        {
            return carDictionary[carType];
        }
        else
        {
            Debug.LogError("Car type not found: " + carType);
            return null; // Or return a default car
        }
    }

    public static Car GetCarByName(string carName)
    {
        foreach (var collectible in carDictionary.Values)
        {
            if (collectible.Name == carName)
            {
                return collectible;
            }
        }

        return null;
    }
}

public class Car
{
    public string Name { get; private set; }
    public float Speed { get; private set; }
    public float Cost { get; private set; }
    public float MaxSpeed { get; private set; }
    public float Health { get; private set; }
    public Sprite Texture { get; private set; }

    public Car(string name, float speed, float maxSpeed, float cost, float health, string texturePath)
    {
        Name = name;
        Speed = speed;
        MaxSpeed = maxSpeed;
        Cost = cost;
        Health = health;
        Texture = Resources.Load<Sprite>(texturePath);
    }
}
