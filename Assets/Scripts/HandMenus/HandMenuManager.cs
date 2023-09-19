using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class HandMenuManager : MonoBehaviour
{
    public GameObject StartupMenu;
    public GameObject GameMenu;
    
    private GameObject _CurrentMenu;

    private void Start()
    {
        _CurrentMenu = StartupMenu;
        _CurrentMenu.SetActive(true);
    }

    public void ChangeMenu(GameObject menu)
    {
        _CurrentMenu.SetActive(false);
        _CurrentMenu = menu;
        _CurrentMenu.SetActive(true);
    }
}
