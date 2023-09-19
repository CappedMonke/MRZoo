using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<Tile> Neighbors = new();

    private void Start()
    {
        if (Neighbors.Count < 4)
        {
            StartCoroutine(DestroyAll());
        }
    }

    private IEnumerator DestroyAll()
    {
        yield return new WaitForSeconds(3);
        
        gameObject.SetActive(false);
    }
}
