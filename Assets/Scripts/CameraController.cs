using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.3f;
    public Vector3 offset;
    public Vector3 targetOffset;
    private Vector3 velocity = Vector3.zero;
    
    //TODO: Make a list of targets to lock on to instead of locking onto the last spawned player in NetworkClient

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    //change the CamTargets position to newTargets position and parent it to them so it stays on them
    public void ChangeTargets(GameObject newTarget)
    {
        GameObject camTarget = GameObject.Find("CameraTarget");
        camTarget.transform.position = newTarget.transform.position;
        camTarget.transform.parent = newTarget.transform;
    }
}
