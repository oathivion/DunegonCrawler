using UnityEditor;
using UnityEngine;
using UnityEditor;

// [CustomEditor(typeof(DoorTeleporter))]
public class DoorTeleporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get reference to the target script
        DoorTeleporter doorTeleporter = (DoorTeleporter)target;

        // Draw default properties for everything but customize based on the teleportType
        doorTeleporter.teleportType = (DoorTeleporter.TeleportType)EditorGUILayout.EnumPopup("Teleport Type", doorTeleporter.teleportType);

        // Conditional display of fields based on the teleportType
        if (doorTeleporter.teleportType == DoorTeleporter.TeleportType.SameScene)
        {
            // Only show the teleport location field for "Same Scene"
            doorTeleporter.teleportLocation = (Transform)EditorGUILayout.ObjectField("Teleport Location", doorTeleporter.teleportLocation, typeof(Transform), true);
        }
        else if (doorTeleporter.teleportType == DoorTeleporter.TeleportType.DifferentScene)
        {
            // Only show the scene name and spawn position for "Different Scene"
            doorTeleporter.sceneName = EditorGUILayout.TextField("Scene Name", doorTeleporter.sceneName);
            doorTeleporter.newSceneSpawnPosition = EditorGUILayout.Vector3Field("New Scene Spawn Position", doorTeleporter.newSceneSpawnPosition);
        }

        // Apply changes to the serialized object
        if (GUI.changed)
        {
            EditorUtility.SetDirty(doorTeleporter);
        }
    }
}
