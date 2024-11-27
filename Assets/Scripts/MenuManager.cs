using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Called when "START" is clicked
    public void StartGame()
    {
        Debug.Log("Starting the game...");
        // Replace "GameScene" with the name of your game scene
        SceneManager.LoadScene("LobbyScene");
    }

    // Called when "SETTINGS" is clicked
    public void OpenSettings()
    {
        Debug.Log("Opening settings...");
        // Add your settings menu logic here (e.g., enable a settings UI panel)
    }

    // Called when "QUIT" is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
