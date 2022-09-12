using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public static class Calculate
{
    public static Vector3Int getClosestTile(Vector3Int pos, List<Vector3Int> doorPositions)
    {
        if (doorPositions.Count == 0) { return Vector3Int.zero; }

        Vector3Int dest = Vector3Int.zero;

        foreach (var p in doorPositions)
        {
            if (Vector3Int.Distance(pos, p) <= 1)
            {
                return p;
            }
        }

        return dest;
    }

    public static float AngleBetween2Points(Vector3 v1, Vector3 v2, float rotation=0)
    {
        float angle = Mathf.Atan2(v2.y - v1.y, v2.x - v1.x) * (180 / Mathf.PI);
        return (angle + 360 + rotation) % 360; 
    }
}