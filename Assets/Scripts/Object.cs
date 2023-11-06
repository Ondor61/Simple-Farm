using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ObjectType
{
    Item,
    Food,
    Seed,
    Axe,
    Pickaxe,
    Hammer,
    Scythe,
    Hoe,
    WateringCan,
    Basket,
}
public abstract class Object : ScriptableObject
{
    public string ObjectName;
    [TextArea(3,3)] public string ObjectDescription;
    public ObjectType Type;
    public Sprite ObjectSprite;
    public uint BuyPrice;
    public uint SellPrice;
    public uint MaxStackSize;
    public int Integer;
    public virtual void Use(Vector3Int position){}
    public string IdToReturn;
}