using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.SpatialAwareness.Processing;
using UnityEngine;

public class Setup : MonoBehaviour
{
    public GameObject GameBoard;
    
    public void InitializeGame()
    {
        CoreServices.SpatialAwarenessSystem.ResumeObservers();
    }
    
    public void SetupGame()
    {
        SetupGameBoard();
        
        CoreServices.SpatialAwarenessSystem.SuspendObservers();
    }

    private void SetupGameBoard()
    {
        // Find all planes of the environment
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        var listMeshData = new List<PlaneFinding.MeshData>();
        
        foreach (var meshObject in observer.Meshes.Values)
        {
            var meshFilter = meshObject.Filter;
            var meshData = new PlaneFinding.MeshData(meshFilter);
            listMeshData.Add(meshData);
        }

        // SELF INFO: If it does not work, consider not using main thread for FindPlanes()
        var planes = PlaneFinding.FindPlanes(listMeshData);
        
        // ShowAllPlanes(planes);
        
        var tablePlane = FilterPlanesForPlaneAtGaze(planes);
        PlaceGameBoard(tablePlane);
    }
    
    private static BoundedPlane FilterPlanesForPlaneAtGaze(IEnumerable<BoundedPlane> planes)
    {
        // Do raycast to get a point in the world
        var cameraTransform = Camera.main.transform;
        var gazeOrigin = cameraTransform.position;
        var gazeDirection = cameraTransform.forward;

        // Compare all planes to the raycast point so we find the plane closest to the players gaze (optimally the table)
        if (!Physics.Raycast(gazeOrigin, gazeDirection, out var hit, Mathf.Infinity))
        {
            throw new Exception("Raycast to find closest plane didn't work as intended.");
        }
        
        BoundedPlane tablePlane = default;
        var closestPoint = Vector3.positiveInfinity;

        foreach (var plane in planes)
        {
            var hitPosition = hit.transform.position;
            var distance = (hitPosition - plane.Bounds.Center).magnitude;
            var closestPointDistance = (hitPosition - closestPoint).magnitude;

            if (!(distance < closestPointDistance))
            {
                continue;
            }
            
            closestPoint = plane.Bounds.Center;
            tablePlane = plane;
        }

        return tablePlane;
    }
    
    private void PlaceGameBoard(BoundedPlane tablePlane)
    {
        GameBoard.SetActive(true);
        
        GameBoard.transform.position = tablePlane.Bounds.Center;
        GameBoard.transform.rotation = tablePlane.Bounds.Rotation;
        GameBoard.transform.localScale = new Vector3(
            tablePlane.Bounds.Extents.x * 2,
            0.01f,
            tablePlane.Bounds.Extents.y * 2
        );
    }
    
    // Visual representation of all planes (not used)
    private void ShowAllPlanes(IEnumerable<BoundedPlane> planes)
    {
        foreach (var plane in planes)
        {
            var primitive = GameObject.CreatePrimitive(PrimitiveType.Plane);
            primitive.transform.position = plane.Bounds.Center;
            primitive.transform.rotation = plane.Bounds.Rotation;
            primitive.transform.localScale = new Vector3(
                plane.Bounds.Extents.x * 2,
                0.01f,
                plane.Bounds.Extents.y * 2
            );
        }
    }
}
