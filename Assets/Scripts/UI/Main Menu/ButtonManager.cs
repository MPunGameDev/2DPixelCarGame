using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        // Add code to start your game here
        // For example, load the game scene:
        SceneManager.LoadScene("GameSession");
    }

    public void Options()
    {
        MainMenuHandler.mainMenuHandler.EnableMenu(MainMenuHandler.mainMenuHandler.OptionsMenu);
        MainMenuHandler.mainMenuHandler.DisableMenu(MainMenuHandler.mainMenuHandler.MainMenu);
    }

    public void QuitGame()
    {
        // Add code to quit the game
        Application.Quit(); // Note that this method may not work in all platforms (e.g., in the Unity Editor)
    }

    public void ExitWelcomeCard()
    {
        MainMenuHandler.mainMenuHandler.RemoveWelcomePrompt();
    }

}
