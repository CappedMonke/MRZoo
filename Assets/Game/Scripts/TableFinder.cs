using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.WindowsSceneUnderstanding.Experimental;
using UnityEngine;
using UnityEngine.Events;

public class TableFinder : MonoBehaviour
{
    [SerializeField] private GameObject EmptyTableButtonPrefab;
    [SerializeField] private GameObject GameboardPrefab;

    private List<GameObject> emptyButtons = new();
    

    public void SpawnTableSelection(SpatialAwarenessSceneObject table)
    {
        var emptyTableButton = Instantiate(EmptyTableButtonPrefab, transform, true);
        emptyTableButton.transform.position = new Vector3(
            table.Position.x,
            table.Position.y + 0.005f,
            table.Position.z
        );
        emptyTableButton.transform.rotation = table.Rotation;
        emptyTableButton.transform.localScale = new Vector3(
            table.Quads[0].Extents.x,
            emptyTableButton.transform.localScale.y,
            table.Quads[0].Extents.y
        );
        
        emptyButtons.Add(emptyTableButton);
        emptyTableButton.GetComponent<Interactable>().OnClick.AddListener(() => SpawnGameboard(table));
    }

    private void SpawnGameboard(SpatialAwarenessSceneObject selectedTable)
    {
        foreach (var button in emptyButtons)
        {
            Destroy(button);
        }
        
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<WindowsSceneUnderstandingObserver>();
        if (observer == null)
        {
            Debug.LogError("Observer is null. Scene understanding might not be supported on the device.");
        }
        observer.AutoUpdate = false;
        
        var gameboardGameObject = Instantiate(GameboardPrefab, transform.parent, true);
        var gameboard = gameboardGameObject.GetComponent<Gameboard>();
        
        gameboard.Setup(
            selectedTable.Position, 
            selectedTable.Rotation,
            new Vector3(
                    selectedTable.Quads[0].Extents.x,
                    gameboard.transform.localScale.y,
                    selectedTable.Quads[0].Extents.y
            )
        );
    }
}
