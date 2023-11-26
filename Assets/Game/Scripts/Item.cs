using System;
using Microsoft.MixedReality.Toolkit.Input;
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
            transform.localScale = new Vector3(
                tile.transform.localScale.x / 2,
                tile.transform.localScale.x / 2,
                tile.transform.localScale.x / 2
            );

            transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y + transform.localScale.y / 2,
                tile.transform.position.z
            );

            Destroy(GetComponent<NearInteractionGrabbable>());
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<BoxCollider>());
            
            tile.HasObject = true;
        }
    }
}
