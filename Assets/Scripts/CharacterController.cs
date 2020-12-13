using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //get the clicked object
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
                //if the clicked object is a tile then get the parents position
                if (hit.transform.gameObject.name == "TileVisual")
                {
                    Debug.Log(hit.transform.parent);
                    Vector3 newPos = new Vector3();

                    newPos.x = hit.transform.parent.position.x;
                    newPos.z = hit.transform.parent.position.z;

                    //send x and y pos
                    StaticManager.Client.SendPosition(newPos.x, newPos.z);
                }
            }
        }
    }
}
