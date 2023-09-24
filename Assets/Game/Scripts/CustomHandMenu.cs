using UnityEngine;

public class CustomHandMenu : MonoBehaviour
{
    public void DeselectCurrentlyHeldItem()
    {
        GameLogic.Instance.CurrentHeldItem = null;
    }
}
