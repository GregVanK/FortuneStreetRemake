using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5f;
    private Vector3 nextPosition;

     void Awake()
    {
        nextPosition = transform.position;
    }
    void Update()
    {

    }

    public void SetNextPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }
}
