using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Examples.Demos;
using Microsoft.MixedReality.Toolkit.Experimental.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.WindowsSceneUnderstanding.Experimental;
using UnityEngine;

public class SceneUnderstandingController : DemoSpatialMeshHandler, IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessSceneObject>
{
    [SerializeField] private TableFinder TableFinder;
    
    private WindowsSceneUnderstandingObserver observer;
    private Dictionary<SpatialAwarenessSurfaceTypes, Dictionary<int, SpatialAwarenessSceneObject>> observedSceneObjects = new();
    
    private void Start()
    {
        observer = CoreServices.GetSpatialAwarenessSystemDataProvider<WindowsSceneUnderstandingObserver>();
        
        if (observer == null)
        {
            Debug.LogError("Observer is null. Scene understanding might not be supported on the device.");
        }
    }

    #region Overrides

    protected override void OnEnable()
    {
        RegisterEventHandlers<IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessSceneObject>, SpatialAwarenessSceneObject>();
    }

    protected override void OnDisable()
    {
        UnregisterEventHandlers<IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessSceneObject>, SpatialAwarenessSceneObject>();
    }

    protected override void OnDestroy()
    {
        UnregisterEventHandlers<IMixedRealitySpatialAwarenessObservationHandler<SpatialAwarenessSceneObject>, SpatialAwarenessSceneObject>();
    }

    #endregion
    
    public void OnObservationAdded(MixedRealitySpatialAwarenessEventData<SpatialAwarenessSceneObject> eventData)
    {
        AddToData(eventData.Id);
        
        if (observedSceneObjects.TryGetValue(eventData.SpatialObject.SurfaceType, out Dictionary<int, SpatialAwarenessSceneObject> sceneObjectDict))
        {
            sceneObjectDict.Add(eventData.Id, eventData.SpatialObject);
        }
        else
        {
            observedSceneObjects.Add(eventData.SpatialObject.SurfaceType, new Dictionary<int, SpatialAwarenessSceneObject> { { eventData.Id, eventData.SpatialObject } });
            
            if (eventData.SpatialObject.SurfaceType == SpatialAwarenessSurfaceTypes.Platform)
            {
                TableFinder.SpawnTableSelection(eventData.SpatialObject, eventData.Id);
            }
        }
    }
    
    public void OnObservationUpdated(MixedRealitySpatialAwarenessEventData<SpatialAwarenessSceneObject> eventData)
    {
        UpdateData(eventData.Id);

        if (observedSceneObjects.TryGetValue(eventData.SpatialObject.SurfaceType, out Dictionary<int, SpatialAwarenessSceneObject> sceneObjectDict))
        {
            observedSceneObjects[eventData.SpatialObject.SurfaceType][eventData.Id] = eventData.SpatialObject;
        }
        else
        {
            observedSceneObjects.Add(eventData.SpatialObject.SurfaceType, new Dictionary<int, SpatialAwarenessSceneObject> { { eventData.Id, eventData.SpatialObject } });
        }
    }

    public void OnObservationRemoved(MixedRealitySpatialAwarenessEventData<SpatialAwarenessSceneObject> eventData)
    {
        RemoveFromData(eventData.Id);

        foreach (var sceneObjectDict in observedSceneObjects.Values)
        {
            sceneObjectDict?.Remove(eventData.Id);
        }
    }
    
    public IReadOnlyDictionary<int, SpatialAwarenessSceneObject> GetSceneObjectsOfType(SpatialAwarenessSurfaceTypes type)
    {
        if (!observer.SurfaceTypes.IsMaskSet(type))
        {
            Debug.LogErrorFormat("The Scene Objects of type {0} are not being observed. You should add {0} to the SurfaceTypes property of the observer in advance.", type);
        }

        if (observedSceneObjects.TryGetValue(type, out Dictionary<int, SpatialAwarenessSceneObject> sceneObjects))
        {
            return sceneObjects;
        }
        else
        {
            observedSceneObjects.Add(type, new Dictionary<int, SpatialAwarenessSceneObject>());
            return observedSceneObjects[type];
        }
    }

    public void UpdateObserver()
    {
        observer.UpdateOnDemand();
    }
}
