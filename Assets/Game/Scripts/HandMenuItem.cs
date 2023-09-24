using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuItem : MonoBehaviour
{
    [SerializeField] private MeshFilter Mesh;
    [SerializeField] private ItemType Type = ItemType.Placeable;

    private enum ItemType
    {
        Ground,
        Placeable
    }
    
    public void CreateItem()
    {
        
    }
}
