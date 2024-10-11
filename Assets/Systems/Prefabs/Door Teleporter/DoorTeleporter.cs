using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleporter : MonoBehaviour
{
    public enum TeleportType
    {
        SameScene,
        DifferentScene
    }

    [Header("Teleport Settings")]
    public TeleportType teleportType;

    // Fields for Same Scene teleportation
   public  Transform teleportLocation;

    // Fields for Different Scene teleportation
    public string sceneName;
    public Vector3 newSceneSpawnPosition;  // Make sure this is serialized

    public void TeleportPlayer(GameObject player)
    {
        switch (teleportType)
        {
            case TeleportType.SameScene:
                TeleportInSameScene(player);
                break;
            case TeleportType.DifferentScene:
                TeleportToDifferentScene(player);
                break;
        }
    }

    private void TeleportInSameScene(GameObject player)
    {
        if (teleportLocation != null)
        {
            player.transform.position = teleportLocation.position;
            Debug.Log("Player teleported to" + teleportLocation.position);
        }
        else
        {
            Debug.LogError("Teleport location not set.");
        }
    }

    private void TeleportToDifferentScene(GameObject player)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            player.transform.position = newSceneSpawnPosition;
            Debug.Log("Player teleported to" + sceneName);
        }
        else
        {
            Debug.LogError("Scene name not set.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        TeleportPlayer(other.gameObject);  // Teleport the player when they collide with the door
    }
}
}
