using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    [Header("MainMenu + Components")]
    public GameObject MainMenu;
    public GameObject FirstTimeLogon;
    public GameObject PlayerStatsPanel;
    public GameObject SelectedCarPanel;
    public GameObject MainMenuButtons;
    public GameObject CoinsText;

    [Header("Options Menu + Components")]
    public GameObject OptionsMenu;

    [Header("ShopMenu + Components")]
    public GameObject ShopMenu;

    public static MainMenuHandler mainMenuHandler;

    private void Awake()
    {
        mainMenuHandler = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerData playerData = Load.LoadPlayer();

        if (playerData.GetCarList().Count == 0)
        {
            playerData.AddCarToPlayer(CarManager.GetCar(CarManager.CarType.BlueF1Car));
            playerData.SetCar(playerData.GetCarList()[0]);
            playerData.health = playerData.GetCar().Health;

            FirstTimeLogon.SetActive(true);
            MainMenuButtons.SetActive(false);
            SelectedCarPanel.SetActive(false);
        }

        TextMeshProUGUI coins = CoinsText.GetComponent<TextMeshProUGUI>();
        coins.text = playerData.GetCoins().ToString();

    }

    public void RemoveWelcomePrompt()
    {
        FirstTimeLogon.SetActive(false);
        MainMenuButtons.SetActive(true);
        SelectedCarPanel.SetActive(true);
    }

    public void EnableMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
