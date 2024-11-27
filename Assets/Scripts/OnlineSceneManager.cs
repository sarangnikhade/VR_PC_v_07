using Unity.Netcode;
using UnityEngine;

public class OnlineSceneManager : MonoBehaviour
{
    public void StartGame()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            // Host loads the game scene and synchronizes it with all clients
            NetworkManager.Singleton.SceneManager.LoadScene("Main Scene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        else
        {
            Debug.LogWarning("Only the host can start the game!");
        }
    }
}
