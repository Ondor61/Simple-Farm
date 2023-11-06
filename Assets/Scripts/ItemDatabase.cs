using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Objects/Internal/ItemDatabase", order = 6)]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ObjectAndId[] data;
    public Dictionary<string, Object> IdToItem = new Dictionary<string, Object>();
    public Dictionary<Object, string> ItemToId = new Dictionary<Object, string>();
    public void OnEnable()
    {
    }
    public void OnAfterDeserialize()
    {
        IdToItem = new Dictionary<string, Object>();
        ItemToId = new Dictionary<Object, string>();
        for (int i = 0; i < data.Length; i++)
        {
            IdToItem.Add(data[i].ID, data[i].Item);
            ItemToId.Add(data[i].Item, data[i].ID);
        }
    }
    public void OnBeforeSerialize()
    {
    }
}

[System.Serializable]
public class ObjectAndId
{
    public string ID;
    public Object Item;
}