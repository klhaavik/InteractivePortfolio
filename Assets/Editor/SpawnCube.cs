using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [MenuItem("Tools/Spawn Cube at Scene View")]
    public static void SpawnCubeAtSceneView()
    {
        // Get the current scene view camera's position and rotation
        SceneView sceneView = SceneView.lastActiveSceneView;
        if (sceneView == null)
        {
            Debug.LogError("No active Scene view found.");
            return;
        }

        Camera sceneCamera = sceneView.camera;
        Vector3 cameraPosition = sceneCamera.transform.position;
        // Quaternion cameraRotation = sceneCamera.transform.rotation;

        // Calculate a position a bit in front of the scene camera
        Vector3 spawnPosition = cameraPosition + sceneCamera.transform.forward * 5f; // 5 units in front of the camera

        // Create a cube at the scene camera's position
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = spawnPosition;
        cube.transform.rotation = Quaternion.identity;

        // Optionally, select the cube in the editor
        Selection.activeGameObject = cube;
    }
}
