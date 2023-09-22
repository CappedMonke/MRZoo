using UnityEngine;

[RequireComponent(typeof(GameboardGrid))]
public class Gameboard : MonoBehaviour
{
    private GameboardGrid _gameboardGrid;
    
    public void Setup(Transform tableTransform)
    {
        var tableTopCenter = tableTransform.position + new Vector3(
            0f,
            tableTransform.localScale.y * 0.5f,
            0f
        );

        transform.position = Utilities.RotatePointAroundPivot(tableTopCenter, tableTransform.position, tableTransform.rotation);
        transform.rotation = tableTransform.rotation;

        var tableSize = new Vector2(tableTransform.localScale.x, tableTransform.localScale.z);
        _gameboardGrid = gameObject.GetComponent<GameboardGrid>();
        _gameboardGrid.Setup(tableSize);
    }
}
