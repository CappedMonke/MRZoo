using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<Tile> Neighbors = new();
    public TextMesh TileIndexText;

    public void DropDown()
    {
        // TODO: Handle logic to make it look cool when the tiles drop down.
    }
}
