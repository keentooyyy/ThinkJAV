using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's Transform
    public Vector3 offset;         // Offset distance between the camera and player
    public float smoothSpeed = 0.125f; // Speed for the smooth transition

    void LateUpdate()
    {
        // Calculate the desired position with offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly transition to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position
        transform.position = smoothedPosition;
    }
}
