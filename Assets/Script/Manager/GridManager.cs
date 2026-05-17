using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField]
    Tilemap walkableTilemap;

    [SerializeField]
    Tilemap colliderTilemap;

    [SerializeField]
    Tilemap trapTilemap;

    Dictionary<Vector3Int, Cell> cellDictionary = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGrid();
    }

    //Initialize grid dengan semua floor type
    void InitializeGrid()
    {
        cellDictionary.Clear();
        // Initialize Ordering Walkable - Unwalkable - Trap
        //Generate walkable

        // Di override sama Wall
        GenerateGridFromTile(walkableTilemap, FloorType.Walkable);
        GenerateGridFromTile(colliderTilemap, FloorType.Obstacle);
        GenerateGridFromTile(trapTilemap, FloorType.Trap);
    }

    //GenerateGrid
    //CheckTilemap
    //CheckFloorType
    //Ubah tilemap ke Cell dan simpan di cellDictionary
    //Banyak continue untuk skipp kalau udah ada cell, atau tilemap kosong
    void GenerateGridFromTile(Tilemap tilemap, FloorType floorType)
    {
        if (tilemap == null)
            return;

        // Cek Bounds dari Tilemap
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            //Check Tilemap has tile, kalau kosong skip
            if (!tilemap.HasTile(pos))
                continue;

            //Cek apakah Cell sudah ada, untuk Override Cell di cellDictionary kalau Walkable, langsung di override sama collider
            if (cellDictionary.ContainsKey(pos))
            {
                //kalau udh contain, override cell dengan floor type baru
                //ada continue supaya gak buat cell baru lagi
                cellDictionary[pos].SetFloorType(floorType);
                continue;
            }

            // Buat Cell baru dan simpan di cellDictionary
            Cell newCell = new Cell(pos, floorType);
            cellDictionary.Add(pos, newCell);
        }
    }

    //Public API
    //TrygetCell dictionary
    public bool TryGetCell(Vector3Int pos, out Cell cell) =>
        cellDictionary.TryGetValue(pos, out cell);

    //GridToWorld
    public Vector3 GridToWorld(Vector3Int gridPos) => walkableTilemap.GetCellCenterWorld(gridPos);

    //WorldToGrid
    public Vector3Int WorldToGrid(Vector3 worldPos) => walkableTilemap.WorldToCell(worldPos);

    public Tilemap GetTilemapByFloorType(FloorType floorType)
    {
        return floorType switch
        {
            FloorType.Walkable => walkableTilemap,
            FloorType.Obstacle => colliderTilemap,
            FloorType.Trap => trapTilemap,
            _ => null,
        };
    }
}
