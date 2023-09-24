using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.WindowsSceneUnderstanding.Experimental;
using UnityEngine;

public class TableFinder : MonoBehaviour
{
    [SerializeField] private GameObject EmptyTableButtonPrefab;
    [SerializeField] private SceneUnderstandingController SceneUnderstandingController;

    public void SpawnTableSelection()
    {
        var tables = SceneUnderstandingController.GetSceneObjectsOfType(SpatialAwarenessSurfaceTypes.Platform);

        foreach (var table in tables)
        {
            var emptyTableButton = Instantiate(EmptyTableButtonPrefab);
            emptyTableButton.transform.parent = transform;
            emptyTableButton.transform.position = table.Value.Position;
            emptyTableButton.transform.rotation = table.Value.Rotation;
            emptyTableButton.transform.localScale = new Vector3(
                table.Value.Quads[0].Extents.x,
                emptyTableButton.transform.localScale.y,
                table.Value.Quads[0].Extents.y
            );
        }
    }
}
