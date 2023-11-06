using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CropType
{
    Crop,
    Weed,
    Wood,
    Stone,
    Empty,
}

[CreateAssetMenu(fileName = "Crop", menuName = "Objects/Internal/Crop", order = 2)]
public class Crop : ScriptableObject
{
    public CropType type;
    public Tile[] tile;
    public string itemYieldedID;
    public string seedsID;
    public uint[] growTimes;
    public uint drop;
    public int side;
}