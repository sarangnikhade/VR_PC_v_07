using UnityEngine;
using Unity.Netcode;

public class MultiplayerManager : MonoBehaviour
{
    public void HostGame()
    {
        NetworkManager.Singleton.StartHost();
        Debug.Log("Hosting game...");
    }

    public void JoinGame()
    {
        NetworkManager.Singleton.StartClient();
        Debug.Log("Joining game...");
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        Debug.Log("Starting server...");
    }
}
