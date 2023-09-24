// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using Microsoft.MixedReality.Toolkit.Input;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    [AddComponentMenu("Scripts/MRTK/Examples/HandInteractionTouchRotate")]
    public class HandInteractionTouchRotate : HandInteractionTouch, IMixedRealityTouchHandler
    {
        [SerializeField]
        [FormerlySerializedAs("TargetObjectTransform")]
        private Transform targetObjectTransform = null;

        [SerializeField]
        private float rotateSpeed = 300.0f;

        public bool IsRotating = false;
        
        public void Update()
        {
            if (targetObjectTransform != null && IsRotating)
            {
                targetObjectTransform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
            }
        }
    }
}