using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuItem : MonoBehaviour
{
    public GameObject Item;

    public void OnItemSelected()
    {
        GameLogic.Instance.CurrentHeldItem = Item.GetComponent<Item>();
        
        var itemClone = Instantiate(Item, transform, true);

        Item.transform.parent = GameObject.FindObjectOfType<Gameboard>().transform;

        Item = itemClone;
    }
}
