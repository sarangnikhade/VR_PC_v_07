using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Player Prefabs")]
    public GameObject pcPlayerPrefab;   // PC player prefab
    public GameObject vrPlayerPrefab;  // VR player prefab

    [Header("Spawn Points")]
    public Transform pcSpawnPoint;     // Spawn point for PC player
    public Transform vrSpawnPoint;     // Spawn point for VR player

    private void Start()
    {
        if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
        {
            SpawnPlayer(NetworkManager.Singleton.LocalClientId); // Spawn the host
        }

        NetworkManager.Singleton.OnClientConnectedCallback += SpawnPlayer;
    }

    private void SpawnPlayer(ulong clientId)
    {
        bool isVRPlayer = (clientId == NetworkManager.Singleton.LocalClientId && UnityEngine.XR.XRSettings.isDeviceActive);

        // Determine the correct prefab and spawn point
        GameObject playerPrefab = isVRPlayer ? vrPlayerPrefab : pcPlayerPrefab;
        Transform spawnPoint = isVRPlayer ? vrSpawnPoint : pcSpawnPoint;

        // Instantiate and spawn the player prefab
        GameObject playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);

        Debug.Log($"Spawned {(isVRPlayer ? "VR_Player" : "PC_Player")} at {spawnPoint.position}");
    }
}










