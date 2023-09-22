using UnityEngine;

[RequireComponent(typeof(GameboardGrid))]
public class Gameboard : MonoBehaviour
{
    private GameboardGrid _gameboardGrid;
    
    public void Setup(Transform tableTransform)
    {
        var tableTopCenter = tableTransform.position + new Vector3(
            0f,
            tableTransform.localScale.y * 0.5f + 0.001f, // Offset of 0.001 to prevent clipping
            0f
        );
        
        transform.position = tableTopCenter;
        transform.rotation = tableTransform.rotation;

        var tableSize = new Vector2(tableTransform.localScale.x, tableTransform.localScale.z);
        _gameboardGrid = gameObject.GetComponent<GameboardGrid>();
        _gameboardGrid.Setup(tableSize);
    }
}
