using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SoilDatabase", menuName = "Objects/Internal/SoilDatabase", order = 5)]
public class SoilDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public SoilAndId[] data;
    public Dictionary<string, Soil> IdToSoil = new Dictionary<string, Soil>();
    public Dictionary<Soil, string> SoilToId = new Dictionary<Soil, string>();
    public void OnEnable()
    {
    }
    public void OnAfterDeserialize()
    {
        IdToSoil = new Dictionary<string, Soil>();
        SoilToId = new Dictionary<Soil, string>();
        for (int i = 0; i < data.Length; i++)
        {
            IdToSoil.Add(data[i].ID, data[i].Soil);
            SoilToId.Add(data[i].Soil, data[i].ID);
        }
    }
    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]
public class SoilAndId
{
    public string ID;
    public Soil Soil;
}