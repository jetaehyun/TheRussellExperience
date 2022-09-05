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

    // public static Vector3Int getSpawnPoint(Tilemap tilemap, Vector3Int origin)
    // {
    //     int[] x = {-1, 0, 0, 1};
    //     int[] y = {0, -1, 1, 0};
    //     Vector3Int sp = origin;

    //     for (int i = 0; i < 4; i++) {

    //         Vector3Int point = origin;

    //         point.x += x[i];
    //         point.y += y[i];
    //         point.z = 0;

    //         if (tilemap.HasTile(point) && tilemap.GetTile(point).)

    //     }



    // }
}