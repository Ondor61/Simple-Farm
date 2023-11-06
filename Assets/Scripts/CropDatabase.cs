using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "CropDatabase", menuName = "Objects/Internal/CropDatabase", order = 3)]
public class CropDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public CropAndId[] data;
    public Dictionary<string, Crop> IdToCrop = new Dictionary<string, Crop>();
    public Dictionary<Crop, string> CropToId = new Dictionary<Crop, string>();
    public void OnEnable()
    {
    }
    public void OnAfterDeserialize()
    {
        IdToCrop = new Dictionary<string, Crop>();
        CropToId = new Dictionary<Crop, string>();
        for (int i = 0; i < data.Length; i++)
        {
            IdToCrop.Add(data[i].ID, data[i].Crop);
            CropToId.Add(data[i].Crop, data[i].ID);
        }
    }
    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]
public class CropAndId
{
    public string ID;
    public Crop Crop;
}