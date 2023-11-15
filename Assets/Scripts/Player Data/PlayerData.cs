using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{

    Car currentPlayerCar;
    public Rigidbody2D rb;
    public SpriteRenderer renderer;

    public void SetCar(Car car)
    {
        currentPlayerCar = car;
    }

    public Car GetCar() { return currentPlayerCar; }
}

public class PlayerData : Player
{ 
    private int coins;
    private List<Car> playerCarList;
    private float milesDriven;
    private float longestDrive;
    private int deaths;

    // Static instance of PlayerData
    private static PlayerData _playerData;

    // Public property to access the PlayerData instance
    public static PlayerData Instance
    {
        get
        {
            // If the instance doesn't exist, create it
            if (_playerData == null)
            {
                _playerData = new PlayerData();
            }
            return _playerData;
        }
    }

    public PlayerData()
    {
        coins = 0;
        milesDriven = 0;
        longestDrive = 0;
        deaths = 0;
        playerCarList = new List<Car>();
    }

    public void AddCarToPlayer(Car carToAdd)
    {
        playerCarList.Add(carToAdd);
    }

    public bool CheckCarList(Car carToCheck)
    {
        if (playerCarList.Contains(carToCheck))
        {
            return true;
        }
        return false;
    }

    public List<Car> GetCarList() { return playerCarList; }

    public void SetCoins(int _coins)
    {
        coins = _coins;
    }
    public void MinusCoins(int _coins)
    {
        coins -= _coins;
    }
    public void AddCoins(int _coins)
    {
        coins += _coins;
    }

    public int GetCoins() { return coins;  }

    public void SetMilesDriven(float _milesDriven)
    {
        milesDriven += _milesDriven;
    }

    public float GetMilesDriven() { return milesDriven; }

    public void SetLongestDrive(float _longestDrive)
    {
        longestDrive = _longestDrive;
    }

    public float GetLongestDrive() { return longestDrive; }

    public void SetDeaths(int _deaths)
    {
        deaths += _deaths;
    }

    public int GetDeaths() { return deaths; }

}

