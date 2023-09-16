using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public void InitializeGame()
    {
        CoreServices.SpatialAwarenessSystem.ResumeObservers();
    }
    
    public void FindPlayground()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();

        foreach (var meshObject in observer.Meshes.Values)
        {
            var mesh = meshObject.Filter.mesh;
        }
        
        CoreServices.SpatialAwarenessSystem.SuspendObservers();
    }
}
