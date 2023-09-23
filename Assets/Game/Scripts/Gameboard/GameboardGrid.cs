using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboardGrid : MonoBehaviour
{
    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private float TileSize = 0.05f;
    [SerializeField] private float MinBorder = 0.02f;
    [SerializeField] private float TileSpawnRate = 0.1f;
    [SerializeField] private List<Tile> Tiles = new();
    
    public void Setup(Vector2 tableSize)
    {
        // Calculate border and playground size
        var border = new Vector2(
            tableSize.x % TileSize,
            tableSize.y % TileSize
        );

        if (border.x < MinBorder)
        {
            border.x = MinBorder;
        }

        if (border.y < MinBorder)
        {
            border.y = MinBorder;
        }
        
        
        // Initialize all tiles we can get on the playground
        var tilesPerDimension = new Vector2(
            Mathf.Floor((tableSize.x - border.x) / TileSize),
            Mathf.Floor((tableSize.y - border.y) / TileSize)
        );
        
        var tilesTotal = tilesPerDimension.x * tilesPerDimension.y;
        
        for (var i = 0; i < tilesTotal; i++)
        {
            var tileGameObject = Instantiate(TilePrefab, transform);
            Tiles.Add(tileGameObject.GetComponent<Tile>());
        }
        
        
        // Connect tiles to their neighbors and set their transforms
        for (var y = 0; y < tilesPerDimension.y; y++)
        {
            for (var x = 0; x < tilesPerDimension.x; x++)
            {
                var currentTile = Tiles[x + y * (int)tilesPerDimension.x];
                currentTile.gameObject.SetActive(false); // Set inactive so we can make it look cool when starting to tween

                // Calculate final border
                border = new Vector2(
                    tableSize.x - tilesPerDimension.x * TileSize,
                    tableSize.y - tilesPerDimension.y * TileSize
                );
                
                currentTile.transform.localPosition = new Vector3(
                    x * TileSize + TileSize * 0.5f - tableSize.x * 0.5f + border.x / 2,
                    currentTile.transform.localScale.y * 0.5f,
                    y * TileSize + TileSize * 0.5f - tableSize.y * 0.5f + border.y / 2
                );
                
                currentTile.transform.localScale = new Vector3(
                    TileSize,
                    currentTile.transform.localScale.y,
                    TileSize
                );

                if(x > 0) currentTile.Neighbors.Add(Tiles[x - 1 + y * (int)tilesPerDimension.x]);
                if(x < tilesPerDimension.x - 1) currentTile.Neighbors.Add(Tiles[x + 1 + y * (int)tilesPerDimension.x]);
                if(y > 0) currentTile.Neighbors.Add(Tiles[x + (y - 1) * (int)tilesPerDimension.x]);
                if(y < tilesPerDimension.y - 1) currentTile.Neighbors.Add(Tiles[x + (y + 1) * (int)tilesPerDimension.x]);
            }
        }

        StartCoroutine(SpawnTiles());
    }

    private IEnumerator SpawnTiles()
    {
        foreach (var tile in Tiles)
        {
            tile.gameObject.SetActive(true);
            StartCoroutine(tile.Spawn());
            
            yield return new WaitForSeconds(TileSpawnRate);
        }
    }
}
