using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

// Maybe will be used, maybe won't
public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;
    
    private void Start()
    {
        Instance = this;
        
        DontDestroyOnLoad(this);
    }
}
