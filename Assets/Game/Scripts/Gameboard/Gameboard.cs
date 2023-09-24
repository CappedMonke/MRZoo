using UnityEngine;

public class Gameboard : MonoBehaviour
{
    private GameboardGrid _gameboardGrid;
    
    public void Setup(Vector3 tablePosition, Quaternion tableRotation, Vector3 tableScale)
    {
        // This is for using meshes, not quads (testing)
        // var tableTopCenter = tablePosition + new Vector3(
        //     0f,
        //     tableScale.y * 0.5f,
        //     0f
        // );
        // transform.position = Utilities.RotatePointAroundPivot(tableTopCenter, tablePosition, tableRotation);

        transform.position = tablePosition;
        transform.rotation = tableRotation;

        var tableSize = new Vector2(tableScale.x, tableScale.z);
        _gameboardGrid = gameObject.GetComponentInChildren<GameboardGrid>();
        _gameboardGrid.Setup(tableSize);
    }
}
