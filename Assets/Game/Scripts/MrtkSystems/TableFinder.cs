using System.Linq;
using Microsoft.MixedReality.Toolkit.Experimental.SceneUnderstanding;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using UnityEngine;

public class TableFinder : MonoBehaviour
{
    public GameObject customPrimitive;
    public Gameboard gameboard;
    private DemoSceneUnderstandingController _controller;
    
    private void Start()
    {
        _controller = GameObject.FindObjectOfType<DemoSceneUnderstandingController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            var meshObjects = _controller.GetSceneObjectsOfType(SpatialAwarenessSurfaceTypes.Platform);

            var meshObject = meshObjects.First();
            
            var quadPosition = meshObject.Value.Position;
            var quadRotation = meshObject.Value.Rotation;
            var quadScale = new Vector3(
                meshObject.Value.Quads[0].Extents.x,
                0,
                meshObject.Value.Quads[0].Extents.y
            );
            
            Debug.LogError($"pos: {quadPosition}");
            Debug.LogError($"rot: {quadRotation}");
            Debug.LogError($"scale: {quadScale}");
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var meshObjects = _controller.GetSceneObjectsOfType(SpatialAwarenessSurfaceTypes.Platform);

            var meshObject = meshObjects.First();
            
            var quadPosition = meshObject.Value.Position;
            var quadRotation = meshObject.Value.Rotation;
            var quadScale = new Vector3(
                meshObject.Value.Quads[0].Extents.x,
                0,
                meshObject.Value.Quads[0].Extents.y
            );
                
                gameboard.Setup(quadPosition, quadRotation, quadScale);
        }
    }
}
