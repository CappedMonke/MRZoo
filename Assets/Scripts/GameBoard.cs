using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.SpatialAwareness.Processing;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private float TileSize = 0.05f; // In cm
    [SerializeField] private Tile Tile;

    private int _tilesInX;
    private int _tilesInZ;
    private List<Tile> _tiles = new();
    
    private void Start()
    {
        CreateGrid(0.8f, 0.5f);
        SetupTiles();
    }

    public void Setup(OrientedBoundingBox table)
    {
        var tableX = table.Extents.x * 2;
        var tableZ = table.Extents.z * 2;
        
        // Set transform of GameBoard to bottom left corner of the table
        transform.position = new Vector3(
            table.Center.x - table.Extents.x, 
            table.Center.y, 
            table.Center.z - table.Extents.z
            );
        transform.rotation = table.Rotation;
        
        CreateGrid(tableX, tableZ);
    }
    
    private void CreateGrid(float tableX, float tableZ)
    {
        var borderX = tableX % TileSize / 2;
        var borderZ = tableZ % TileSize / 2;
        
        var halfTileSize = TileSize / 2;
        borderX = borderX < halfTileSize ? halfTileSize : borderX;
        borderZ = borderZ < halfTileSize ? halfTileSize : borderZ;

        tableX -= borderX * 2;
        tableZ -= borderZ * 2;
        
        _tilesInX = (int)Mathf.Floor(tableX / TileSize);
        _tilesInZ = (int)Mathf.Floor(tableZ / TileSize);

        // Instantiate a empty tile with correct position and then add it to the _tiles list
        for (var x = 0; x < _tilesInX; x++)
        {
            for(var z = 0; z < _tilesInZ; z++)
            {
                var tilePosition = new Vector3(
                    transform.position.x + x * TileSize + TileSize / 2 + borderX,
                    transform.position.y,
                    transform.position.z + z * TileSize + TileSize / 2 + borderZ
                    );
                
                var tile = Instantiate(Tile, tilePosition, transform.rotation);
        
                tile.transform.localScale = new Vector3(
                    TileSize,
                    tile.transform.localScale.y,
                    TileSize
                    );
                
                _tiles.Add(tile);
            }
        }
    }

    private void SetupTiles()
    {
        for (var z = 0; z < _tilesInX; z++)
        {
            for (var x = 0; x < _tilesInZ; x++)
            {
                var tile = _tiles[x + _tilesInZ * z];

                if (z > 0)
                {
                    tile.Neighbors.Add(_tiles[x + _tilesInX * (z - 1)]);
                }
                    

                    if (z < _tilesInZ - 1)
                    {
                        tile.Neighbors.Add(_tiles[x + _tilesInX * (z + 1)]);
                    }
                    

                    if (x > 0)
                    {
                        tile.Neighbors.Add(_tiles[(x - 1) + _tilesInX * z]);
                    }
                    

                    if (x < _tilesInX - 1)
                    {
                        tile.Neighbors.Add(_tiles[(x + 1) + _tilesInX * z]);
                    }
                    
            }
        }
    }
}
