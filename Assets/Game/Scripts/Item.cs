using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType Type = ItemType.Tile;
    
    public enum  ItemType
    {
        Object,
        Tile
    }
}
