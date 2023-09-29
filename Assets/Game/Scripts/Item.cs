using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType Type = ItemType.Tile;

    [SerializeField] private MeshRenderer MeshRenderer;

    private Tile tile;
    
    
    public enum  ItemType
    {
        Object,
        Tile
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        
        var tempTile = other.gameObject.GetComponent<Tile>();

        if (tempTile == null)
        {
            return;
        }
        
        tile = tempTile;
    }

    public void OnItemSelected()
    {
        transform.parent = FindObjectOfType<Gameboard>().transform;
    }
    
    public void OnItemDeselected()
    {
        if (tile == null)
        {
            Destroy(gameObject);
            return;
        }
        
        if (Type == ItemType.Tile)
        {
             tile.MeshRenderer.material = MeshRenderer.material;
            
            Destroy(gameObject);
        }
        
        if (Type == ItemType.Object && !tile.HasObject)
        {
            transform.position = tile.transform.position;

            Destroy(GetComponent<BoxCollider>());
            
            tile.HasObject = true;
        }
    }
}
