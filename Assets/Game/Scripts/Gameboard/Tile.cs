using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer MeshRenderer;
    [SerializeField] private float SpawnTime = 0.05f;
    
    public List<Tile> Neighbors;

    public IEnumerator Spawn()
    {
        var elapsedTime = 0f;
        var meshColor = MeshRenderer.material.color;
        var originalScale = transform.localScale;
        
        while (elapsedTime < SpawnTime)
        {
            var t = elapsedTime / SpawnTime;
            
            MeshRenderer.material.color = new Color(
                meshColor.r,
                meshColor.g,
                meshColor.b,
                0
            );

            transform.localScale = new Vector3(
                Mathf.Lerp(0, originalScale.x, t),
                Mathf.Lerp(0, originalScale.y, t),
                Mathf.Lerp(0, originalScale.z, t)
            );
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        MeshRenderer.material.color = new Color(
            meshColor.r,
            meshColor.g,
            meshColor.b,
            1
        );

        yield return transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y,
            originalScale.z
        );
    }
}
