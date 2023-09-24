using Microsoft.MixedReality.Toolkit.Examples.Demos;
using UnityEngine;

public class HandMenuItem : MonoBehaviour
{
    public MeshFilter Mesh;
    public MeshRenderer MeshRenderer;
    public HandInteractionTouchRotate HandInteractionTouchRotate;
    public ItemType Type = ItemType.Placeable;

    public enum ItemType
    {
        Ground,
        Placeable
    }
    
    public void SetCurrentlyHeldItem()
    {
        GameLogic.Instance.CurrentHeldItem = this;
        HandInteractionTouchRotate.IsRotating = true;
    }
}
