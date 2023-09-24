using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // I don't check against null or anything because it is a minimal project. Also I don't have any more time to spend.
    public static GameLogic Instance;

    public HandMenuItem CurrentHeldItem;

    private void Start()
    {
        Instance = this;
    }
}
