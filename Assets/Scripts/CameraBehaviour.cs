using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;      // Set an offset if you want the camera to not be directly on the player
    [SerializeField] float smoothSpeed = 0.125f;  // Adjust this for smoother or snappier movement

    private const int zPos = -10;

    // Boundaries
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    void FixedUpdate()
    {
        if (player != null)
        {
            // Target position is the player’s position + offset
            Vector3 targetPosition = new Vector3(player.position.x +
                offset.x, player.position.y +
                offset.y, zPos);

            // Smoothly interpolate between the camera’s current position and target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Clamp the camera position to stay within the defined boundaries
            //float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            //float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            // Set the camera position with the clamped values
            //transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
            transform.position = smoothedPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EdgeCollider2D>())
        {
            
        }
    }
}
