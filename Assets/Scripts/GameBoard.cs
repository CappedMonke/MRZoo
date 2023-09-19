using System;
using Microsoft.MixedReality.Toolkit.SpatialAwareness.Processing;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private float TileSize = 0.05f; // In cm
    [SerializeField] private GameObject EmptyTile;


    private void Start()
    {
        CreateGrid(0.8f, 0.5f);
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
        
        var tilesInX = Mathf.Floor(tableX / TileSize);
        var tilesInZ = Mathf.Floor(tableZ / TileSize);

        for (var x = 0; x < tilesInX; x++)
        {
            for(var z = 0; z < tilesInZ; z++)
            {
                var tilePosition = new Vector3(
                    transform.position.x + x * TileSize + TileSize / 2 + borderX,
                    transform.position.y,
                    transform.position.z + z * TileSize + TileSize / 2 + borderZ
                    );
                
                var tile = Instantiate(EmptyTile, tilePosition, transform.rotation);
        
                tile.transform.localScale = new Vector3(
                    TileSize,
                    tile.transform.localScale.y,
                    TileSize
                    );
            }
        }
    }
}
