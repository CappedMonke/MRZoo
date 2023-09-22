using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboardTest : MonoBehaviour
{
    public Gameboard Gameboard;
    public Transform TableTransform;

    private void Start()
    {
        Gameboard.Setup(TableTransform);
    }
}
