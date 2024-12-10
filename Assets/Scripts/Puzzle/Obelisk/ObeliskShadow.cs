using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskShadow : MonoBehaviour
{
    [SerializeField] private LineRenderer shadowRenderer; // LineRenderer component
    [SerializeField] private Transform lightSource; // Position of the light source
    [SerializeField] private float shadowLength = 25f; // Length of the shadow
    [SerializeField] private float shadowZAxis = 0f;
    [SerializeField] private Transform basePosition;

    private void Start()
    {
        if (shadowRenderer == null)
        {
            shadowRenderer = GetComponent<LineRenderer>();
        }

        // Configure the LineRenderer properties
        shadowRenderer.positionCount = 2; // Start and end points
        shadowRenderer.startWidth = 1f;
        shadowRenderer.endWidth = 2f;
        //shadowRenderer.material = new Material(Shader.Find("Sprites/Default")); // Basic material
        shadowRenderer.startColor = new Color(0, 0, 0, 0.5f); // Semi-transparent black
        shadowRenderer.endColor = new Color(0, 0, 0, 0.2f); // Fades at the end
    }

    private void Update()
    {
        // Calculate shadow direction based on the light source
        Vector3 direction = (transform.position - lightSource.position).normalized;

        // Set the positions of the LineRenderer
        shadowRenderer.SetPosition(0, new Vector3(basePosition.position.x, basePosition.position.y, shadowZAxis)); // Start at the obelisk's base
        shadowRenderer.SetPosition(1, basePosition.position + direction * shadowLength); // Extend shadow
    }
}
