using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public void InitializeGame()
    {
        CoreServices.SpatialAwarenessSystem.ResumeObservers();
    }
    
    public void FindPlayground()
    {
        CreateHeightMap();


        
        CoreServices.SpatialAwarenessSystem.SuspendObservers();
    }

    private void CreateHeightMap()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        
        foreach (var meshObject in observer.Meshes.Values)
        {
            var mesh = meshObject.Filter.mesh;
            var meshVertices = mesh.vertices;

            foreach (var vertex in meshVertices)
            {
                if (vertex.y < 0.02f)
                {
                    
                }
            }
        }
    }
}
