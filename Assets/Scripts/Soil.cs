using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//types of soils
public enum SoilType
{
    Farm,
    Mine,
    Other,
}

[CreateAssetMenu(fileName = "Soil", menuName = "Objects/Internal/Soil", order = 4)]
public class Soil : ScriptableObject
{
    public SoilType type;
    public TileBase[] tile;
    // [Rough, Tilled, Watered]
    // [0    , 1     , 2      ]
}
