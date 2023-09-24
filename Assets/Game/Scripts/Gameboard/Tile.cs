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

    public void PlaceSomething()
    {
        var item = GameLogic.Instance.CurrentHeldItem;
        if (item == null)
        {
            return;
        }

        if (item.Type == HandMenuItem.ItemType.Ground)
        {
            MeshRenderer.material = item.MeshRenderer.material;
        }
    }
}
