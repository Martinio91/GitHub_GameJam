using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera
    [SerializeField] private Vector2 parallaxEffectMultiplier = new Vector2(0.5f, 0.5f); // Adjust for speed

    private Vector3 lastCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Default to Main Camera
        }
        lastCameraPosition = cameraTransform.position;

    }

    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x,
                                          deltaMovement.y * parallaxEffectMultiplier.y,
                                          0);
        lastCameraPosition = cameraTransform.position;
    }
}
