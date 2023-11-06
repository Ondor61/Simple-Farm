using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Additions
{
    public static Vector3Int ScreenToCell(this Tilemap tilemap, Vector3 position)
    {
        if (position == null) throw new NullReferenceException();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        Vector3Int tilemapPosition = tilemap.WorldToCell(worldPosition);
        tilemapPosition.z = 0; // z value of zero is needed for functions like SetTile to work properly
        return tilemapPosition;
    }
}
