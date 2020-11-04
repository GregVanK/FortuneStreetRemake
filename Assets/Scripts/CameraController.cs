using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    //the true target bypasses the players transform and allows the camera to be situated properly even if the players transform is not convenient
    public Transform trueTarget;

    public float smoothTime = 0.3f;
    public Vector3 offset;
    public Vector3 targetOffset;
    private Vector3 velocity = Vector3.zero;

    
    private void FixedUpdate()
    {
        target.position = trueTarget.position + targetOffset;
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}
