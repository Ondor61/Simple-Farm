using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MAIN : MonoBehaviour
{
    [SerializeField] private Tilemap cropTilemap;
    [SerializeField] private Tilemap soilTilemap;
    [SerializeField] private Field field;

    // Start is called before the first frame update
    void Start()
    {
        field.LoadFromSave();
        field.LoadFromIds();
        field.Render(soilTilemap, cropTilemap);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            LeftClick(soilTilemap.ScreenToCell(Input.mousePosition));

        if (Input.GetMouseButtonDown(1))
        {
            Water(soilTilemap.ScreenToCell(Input.mousePosition));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            field.DayPasses();
            field.Render(soilTilemap, cropTilemap);
        }
    }

    private void LeftClick(Vector3Int position)
    {
        field.SetIndex(position);
        if(!field.InBounds(position)) return;
        
        if (field.Crop().ID == "Empty")
            Plant(position);
        else if (field.Crop().stage == 3)
            PickUp(position);
    }

    private void Plant(Vector3Int position)
    {
        field.AddCrop("Carrot");
        cropTilemap.SetTile(position, field.Crop().Tile());
    }

    private void PickUp(Vector3Int position)
    {
        field.RemoveCrop();
        cropTilemap.SetTile(position, field.Crop().Tile());
    }

    private void Water(Vector3Int position)
    {
        if (!field.InBounds(position)) return;
        field.SetIndex(position);
        if (field.Soil().state == 2) return;
        field.ChangeSoil(field.Soil().state+1);
        soilTilemap.SetTile(position, field.Soil().Tile());
    }
}
