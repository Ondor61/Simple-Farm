using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using  UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] private Tilemap testTileMap;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3Int tilemapPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("");
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tilemapPosition = testTileMap.WorldToCell(position);
            Debug.Log("Position: " + ((int)position.x).ToString() + ", " + ((int)position.y).ToString());
            Debug.Log("Tilemap:  " +tilemapPosition.x.ToString() + ", " + tilemapPosition.y.ToString());
        }
    }
}
