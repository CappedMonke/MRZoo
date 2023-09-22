using System.Collections.Generic;
using UnityEngine;

public class GameboardGrid : MonoBehaviour
{
    [SerializeField] private GameObject TilePrefab;
    [SerializeField] List<Tile> Tiles = new();
    
    public void Setup(Vector2 tableSize, float tileSize)
    {
        var tilesPerDimension = new Vector2(
            Mathf.Floor(tableSize.x / tileSize),
            Mathf.Floor(tableSize.y / tileSize)
        );
        
        var tilesTotal = tilesPerDimension.x * tilesPerDimension.y;
        
        for (var i = 0; i < tilesTotal; i++)
        {
            var tileGameObject = Instantiate(TilePrefab, transform, true);
            Tiles.Add(tileGameObject.GetComponent<Tile>());
        }
        
        // Connect tiles to their neighbors and set their transforms
        for (var y = 0; y < tilesPerDimension.y; y++)
        {
            for (var x = 0; x < tilesPerDimension.x; x++)
            {
                var currentTile = Tiles[x + y * (int)tilesPerDimension.x];

                currentTile.transform.position = new Vector3(
                    x * tileSize,
                    currentTile.transform.position.y,
                    y * tileSize
                );

                currentTile.transform.localScale = new Vector3(
                    tileSize,
                    currentTile.transform.localScale.y,
                    tileSize
                );

                if(x > 0) currentTile.Neighbors.Add(Tiles[x - 1 + y * (int)tilesPerDimension.x]);
                if(x < tilesPerDimension.x - 1) currentTile.Neighbors.Add(Tiles[x + 1 + y * (int)tilesPerDimension.x]);
                if(y > 0) currentTile.Neighbors.Add(Tiles[x + (y - 1) * (int)tilesPerDimension.x]);
                if(y < tilesPerDimension.y - 1) currentTile.Neighbors.Add(Tiles[x + (y + 1) * (int)tilesPerDimension.x]);
            }
        }
    }
}
