using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObeliskPuzzle : MonoBehaviour
{
    [SerializeField] private Transform[] obelisks; // Array of two obelisks
    [SerializeField] private Transform[] targetPositions; // Correct positions for shadows
    [SerializeField] private float alignmentThreshold = 0.5f; // How close the obelisk must be to the target

    public UnityEvent obselikSolved;
    private bool isSolved = false;

    void Update()
    {
        if (!isSolved)
        {
            if (CheckAlignment())
            {
                isSolved = true;
                obselikSolved?.Invoke();
            }
        }
    }

    // Check if both obelisks are aligned with their targets
    private bool CheckAlignment()
    {
        for (int i = 0; i < obelisks.Length; i++)
        {
            float distance = Mathf.Abs(obelisks[i].position.x - targetPositions[i].position.x);
            if (distance > alignmentThreshold)
            {
                return false; // One obelisk is not aligned
            }
        }
        return true; // All obelisks are aligned
    }

    // Move an obelisk horizontally (called by player input)
    public void MoveObelisk(int obeliskIndex, float newPosX)
    {
        if (obeliskIndex >= 0 && obeliskIndex < obelisks.Length)
        {
            var vector = obelisks[obeliskIndex].position;
            obelisks[obeliskIndex].position = new Vector3(newPosX, vector.y, vector.z);
        }
    }
}
