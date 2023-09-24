using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // I don't check against null or anything because it is a minimal project. Also I don't have any more time to spend.
    public static GameLogic Instance;

    public static GameObject CurrentHeldItem;

    private IMixedRealityHand hand;
    
    private void Start()
    {
        Instance = this;
    }
}
