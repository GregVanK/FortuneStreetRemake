using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] spaces;
    void Start()
    {
        spaces = GetComponentsInChildren<Transform>();
    }
}
