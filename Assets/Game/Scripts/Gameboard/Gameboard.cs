using UnityEngine;

public class Gameboard : MonoBehaviour
{
    private GameboardGrid _gameboardGrid;
    
    public void Setup(Vector3 tablePosition, Quaternion tableRotation, Vector3 tableScale)
    {
        transform.position = tablePosition;
        transform.rotation = tableRotation;

        var tableSize = new Vector2(tableScale.x, tableScale.z);
        _gameboardGrid = gameObject.GetComponentInChildren<GameboardGrid>();
        _gameboardGrid.Setup(tableSize);
    }
}
