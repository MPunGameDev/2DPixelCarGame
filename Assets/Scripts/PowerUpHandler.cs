using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    private static PowerUpHandler powerUpHandler;
    private TopDownCarController carController;

    private Dictionary<PowerUpType, Coroutine> activePowerUps = new Dictionary<PowerUpType, Coroutine>();
    private Dictionary<PowerUpType, float> powerUpDurations = new Dictionary<PowerUpType, float>();

    private void Awake()
    {
        powerUpHandler = this;
    }

    public static PowerUpHandler Instance
    {
        get
        {
            return powerUpHandler;
        }
    }
    private void Start()
    {
        carController = gameObject.GetComponent<TopDownCarController>();
    }

    public void ApplyPowerUpEffect(PowerUpType powerUpType, float duration)
    {

        if (activePowerUps.ContainsKey(powerUpType) && powerUpDurations.ContainsKey(powerUpType))
        {
            // Update the duration of the existing coroutine
            StopCoroutine(activePowerUps[powerUpType]);
            activePowerUps.Remove(powerUpType);
            duration += powerUpDurations[powerUpType];
            powerUpDurations.Remove(powerUpType);
        }
        else
        {
            // Apply the power-up effect
            ApplyEffect(powerUpType, CollectibleDatabase.GetPowerUp(powerUpType).BuffValue);
        }

        // Start a new coroutine for the updated duration
        powerUpDurations[powerUpType] = duration;
        Coroutine coroutine = StartCoroutine(RemovePowerUpEffectAfterDuration(powerUpType));
        activePowerUps[powerUpType] = coroutine;
    }

    public IEnumerator RemovePowerUpEffectAfterDuration(PowerUpType powerUpType)
    {

        while (powerUpDurations[powerUpType] > 0)
        {
            powerUpDurations[powerUpType] -= Time.deltaTime;
            Debug.Log(powerUpType.ToString() + ": " + powerUpDurations[powerUpType]);
            yield return null;
        }

        // Remove the power-up effect
        RemoveEffect(powerUpType);

        // Remove the coroutine from the activePowerUps dictionary
        activePowerUps.Remove(powerUpType);
        powerUpDurations.Remove(powerUpType);
    }

    private void ApplyEffect(PowerUpType powerUpType, float buffValue)
    {
        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost:
                carController.SetSpeed(carController.speed *= buffValue);
                break;
            case PowerUpType.Invincibility:
                // Handle Invincibility effect here
                break;
            case PowerUpType.HealthPack:
                // Handle HealthPack effect here
                break;
                // Add cases for other power-up types as needed
        }
    }

    private void RemoveEffect(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost:
                carController.SetSpeed(carController.minSpeed);
                break;
            case PowerUpType.Invincibility:
                // Handle removing Invincibility effect here
                break;
            case PowerUpType.HealthPack:
                // Handle removing HealthPack effect here
                break;
                // Add cases for other power-up types as needed
        }
    }
}
