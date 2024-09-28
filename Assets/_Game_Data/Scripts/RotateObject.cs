using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 Rotation;     // Rotation type
    public float rotationSpeed;  // Speed Of Rotation
    
    // public float scaleSpeed = 2f;  // Speed of scaling
    // public float minScale = 0.5f;  // Minimum scale size
    // public float maxScale = 2f;    // Maximum scale size
    // private Vector3 targetScale;
    // private bool isScalingUp = true;

    void Update()
    {
        // Object Rotate Portion.......
        transform.localRotation *= Quaternion.Euler(Rotation * rotationSpeed * Time.deltaTime);
      
        
        
        // Scale Up And Down Portion.......
        // targetScale = isScalingUp ? Vector3.one * maxScale : Vector3.one * minScale;
        // transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
        // if (Mathf.Abs(transform.localScale.x - targetScale.x) < 0.01f)
        // {
        //     isScalingUp = !isScalingUp;
        // }
    }
}
