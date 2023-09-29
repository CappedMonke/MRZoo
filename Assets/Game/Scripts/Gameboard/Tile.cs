using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    [SerializeField] private float SpawnTime = 0.05f;

    public List<Tile> Neighbors;
    public bool HasObject = false;
    
    public IEnumerator Spawn()
    {
        var elapsedTime = 0f;
        var originalScale = transform.localScale;
        
        while (elapsedTime < SpawnTime)
        {
            var t = elapsedTime / SpawnTime;
            
            transform.localScale = new Vector3(
                Mathf.Lerp(0, originalScale.x, t),
                Mathf.Lerp(0, originalScale.y, t),
                Mathf.Lerp(0, originalScale.z, t)
            );
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = originalScale;
    }
}
