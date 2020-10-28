using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x + 0.001f, this.transform.position.y, this.transform.position.z);
    }
}
