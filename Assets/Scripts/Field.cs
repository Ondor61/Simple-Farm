using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.Tilemaps;
using System;
using UnityEngine.Rendering;


[CreateAssetMenu(fileName = "Field", menuName = "Objects/Internal/Field", order = 1)]
public class Field : ScriptableObject
{
    [SerializeField]private CropDatabase cropDatabase;
    [SerializeField]private SoilDatabase soilDatabase;
    [SerializeField] private string savePath;
    public Vector3Int offset;
    [SerializeField] public CropSlot[] crops;
    [SerializeField] public SoilSlot[] soils;
    public Vector3Int size;
    private CropSlot crop;
    private SoilSlot soil;
    public int index;

    public void SetIndex(Vector3Int position)
    {
        Vector3 fieldPosition;
        fieldPosition = position - offset;
        Debug.Log(fieldPosition);
        index = (int)(fieldPosition.y * (size.x+1) + fieldPosition.x);
    }

    public void SetIndex(int position)
    {
        index = position;
    }

    public CropSlot Crop()
    {
        return crops[index];
    }

    public SoilSlot Soil()
    {
        return soils[index];
    }

    public void SetUp()
    {
        LoadFromSave();
        cropDatabase = Resources.Load<CropDatabase>("Database/CropDatabase");
        soilDatabase = Resources.Load<SoilDatabase>("Database/SoilDatabase");
        LoadFromIds();
    }

    public void OnDisable()
    {
        Save();
    }

    public void AddCrop(string ID)
    {
        crops[index] = new CropSlot( cropDatabase.IdToCrop[ID], ID );
    }

    public void RemoveCrop()
    {
        crops[index] = new CropSlot(cropDatabase.IdToCrop["Empty"], "Empty");
    }

    public void ChangeSoil(int state)
    {
        soils[index].state = state;
    }

    public void DayPasses()
    {
        for (int i = 0; i < soils.Length; i++)
        {   
            soil = soils[i];
            crop = crops[i];
            Debug.Log(i);
            //if soil is rough
            //if (soil.state == 0)
                //SpreadDebree();

            if (soil.state == 1)
                RoughenSoil();

            if (soil.state == 2)
                GrowCrop();
        }
    }

    public void SpreadDebree()
    {
        if (crop.crop.type != CropType.Empty || UnityEngine.Random.Range(0f,1f) > 0.04f)
            return;
                
        crop = new CropSlot(cropDatabase.IdToCrop["Weed"], "Weed");
        return;
    }

    public void RoughenSoil()
    {
        if(crop.crop.type != CropType.Empty || UnityEngine.Random.Range(0f,1f) > 0.25f) //0.xx = xx% chance for the event to happen
            return;

        soil.state = 0;
        return;
    }

    public void GrowCrop()
    {
        soil.state = 1;

        if(crop.crop.type != CropType.Crop || crop.stage == 3)
            return;

        crop.AddDay();
        
        if (crop.crop.growTimes[crop.stage] <= crop.days)
            crop.AdvanceStage();
            
        return;
    }

    public bool InBounds(Vector3Int position)
    {
        bool xInBounds = 0 <= position.x - offset.x && position.x - offset.x <= size.x;
        bool yInBounds = 0 <= position.y - offset.y && position.y - offset.y <= size.y;
        if (xInBounds && yInBounds) return true;
        return false;
    }

    public void Render(Tilemap soilTilemap, Tilemap cropTilemap)
    {
        if (cropTilemap == null || soilTilemap == null) throw new NullReferenceException("Tilemaps not set correctly");
        Vector3Int position = Vector3Int.zero;
        index = 0;
        while (position.y <= size.y)
        {
            while (position.x <= size.x)
            {
                soilTilemap.SetTile(position, Soil().Tile());
                cropTilemap.SetTile(position, Crop().Tile());
                position.x++;
                index++;
            }
            position.y++;
            position.x = 0;
        }
        return;
    }

    public void Save()
    {
        string saveData =  JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void LoadFromSave()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void LoadFromIds()
    {
        for (int i = 0; i < crops.Length; i++)
        {
            crops[i].crop = cropDatabase.IdToCrop[crops[i].ID];
            soils[i].soil = soilDatabase.IdToSoil[soils[i].ID];    
        } 
    }
}

[System.Serializable]
public class CropSlot
{
    public string ID;
    public Crop crop;
    public int days;
    public int stage;
    public CropSlot (Crop _crop, string _ID)
    {
        ID = _ID;
        crop = _crop;
        days = 0;
        stage = 0;
    }
    public void AddDay()
    {
        days ++;
    }
    public void AdvanceStage()
    {
        days = 0;
        stage ++;
    }
    public TileBase Tile()
    {
        return crop.tile[stage];
    }
}

[System.Serializable]
public class SoilSlot
{
    public string ID;
    public Soil soil;
    public int state;
    public SoilSlot (Soil _soil, string _ID, int _state)
    {
        soil = _soil;
        ID = _ID;
        state = _state;
    }
    public TileBase Tile()
    {
        return soil.tile[state];
    }
}