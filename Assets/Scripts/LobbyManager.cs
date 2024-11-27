using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text feedbackText;

    public void StartHost()
    {
        feedbackText.text = "Hosting game...";
        NetworkManager.Singleton.StartHost();
        Debug.Log("Host started");
    }

    public void StartClient()
    {
        feedbackText.text = "Joining game...";
        Debug.Log("Attempting to connect to host...");
        NetworkManager.Singleton.StartClient();

        // Log connection events
        NetworkManager.Singleton.OnClientConnectedCallback += (clientId) =>
        {
            Debug.Log("Connected to Host as Client ID: " + clientId);
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (clientId) =>
        {
            Debug.LogWarning("Disconnected from server. Client ID: " + clientId);
        };
    }

    private void OnEnable()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }
    }

    private void OnDisable()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log("A new client connected: " + clientId);
        }
        else
        {
            Debug.Log("Connected to Host as Client ID: " + clientId);
        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.LogWarning("Client disconnected: " + clientId);
    }

    public void StartGame()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log("Starting game...");
            NetworkManager.Singleton.SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }
}
