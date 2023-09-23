using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using UnityEngine;

public class TableFinder : MonoBehaviour
{
    [SerializeField] private GameObject GameboardPrefab;
    
    private IMixedRealitySceneUnderstandingObserver observer;
    private List<SpatialAwarenessSceneObject> surfaces = new();


    private void Start()
    {
        observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySceneUnderstandingObserver>();

        if (observer == null)
        {
            Debug.LogError("Scene understanding observer is null. " +
                           "Either the observer is not set up correctly or the device doesn't support scene understanding.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
    
    public List<SpatialAwarenessSceneObject> GetSurfaces()
    {
        
        
        return surfaces;
    }
}
