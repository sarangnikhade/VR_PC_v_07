using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    public GameObject vrPlayerPrefab;
    public GameObject pcPlayerPrefab;

    public Transform vrSpawnPoint;
    public Transform pcSpawnPoint;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        GameObject prefab;
        Transform spawnPoint;

        // Assign prefab and spawn point based on role
        if (NetworkManager.Singleton.IsHost)
        {
            prefab = pcPlayerPrefab; // Host is the Hunter
            spawnPoint = pcSpawnPoint;
        }
        else
        {
            prefab = vrPlayerPrefab; // Clients are Birds
            spawnPoint = vrSpawnPoint;
        }

        // Spawn the player
        GameObject playerInstance = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
    }
}
