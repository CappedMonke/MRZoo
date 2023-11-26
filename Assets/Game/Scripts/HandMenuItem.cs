using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuItem : MonoBehaviour
{
    public GameObject Item;

    public void OnItemSelected()
    {
        var itemClone = Instantiate(Item, transform, true);
        
        Item = itemClone;
    }
}
