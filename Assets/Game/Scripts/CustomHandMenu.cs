using UnityEngine;

public class CustomHandMenu : MonoBehaviour
{
    public void DeselectCurrentlyHeldItem()
    {
        GameLogic.Instance.CurrentHeldItem.HandInteractionTouchRotate.IsRotating = false;
        GameLogic.Instance.CurrentHeldItem = null;
    }
}
