using System;
using System.Collections;
using UnityEngine;

public class KeepObjectInView : MonoBehaviour
{
    [SerializeField]
    private float MaxDistanceBeforeMoving = 0.3f;
    [SerializeField]
    private float MovingTime = 0.1f;
    
    private Transform CameraTransform;
    private float DistanceFromCamera;
    private bool IsCoroutineRunning = false;

    private void Start()
    {
        if (Camera.main == null)
        {
            return;
        };
        
        CameraTransform = Camera.main.transform;
        DistanceFromCamera = (transform.position - CameraTransform.position).magnitude;
    }

    private void Update()
    {
        var preObjectPosition = CameraTransform.position + CameraTransform.forward * DistanceFromCamera;
        var distanceObjectPositionAndPreObjectPosition = (transform.position - preObjectPosition).magnitude;
        
        if (!IsCoroutineRunning && distanceObjectPositionAndPreObjectPosition > MaxDistanceBeforeMoving)
        {
            StartCoroutine(MoveToCameraCenter());
        }
        
        transform.rotation = CameraTransform.rotation;
    }

    private IEnumerator MoveToCameraCenter()
    {
        IsCoroutineRunning = true;
        
        var elapsedTime = 0f;
        var initialTransform = transform;

        while (elapsedTime < MovingTime)
        {
            transform.position = Vector3.Lerp(initialTransform.position,
                CameraTransform.position + CameraTransform.forward * DistanceFromCamera,
                elapsedTime / MovingTime);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = CameraTransform.position + CameraTransform.forward * DistanceFromCamera;
        

        IsCoroutineRunning = false;
    }
}