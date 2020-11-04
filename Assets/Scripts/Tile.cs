using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //pointers to different possible tiles
    //0-3: North, South, East, West. 4-7: NorthEast,NorthWest,SouthEast,SouthWest
    public List<Tile> possibleTiles = new List<Tile>();

    //draw a line to each of the possible tiles
    private void OnDrawGizmos()
    {
        Vector3 horOffset = new Vector3(1, 0, 0);
        Vector3 depthOffset = new Vector3(0, 0, 1);

        for (int i = 0; i < possibleTiles.Count; i++)
        {
            Vector3 currentPos = this.transform.position;
            Vector3 drawPos;
            if (possibleTiles[i] == null || possibleTiles[i] == this)
            {
                continue;
            }

            switch (i)
            {
                //north
                case 0:
                    Gizmos.color = Color.green;
                    drawPos = possibleTiles[0].transform.position;
                    Gizmos.DrawLine(currentPos - horOffset, drawPos - horOffset);
                    break;
                case 1:
                    Gizmos.color = Color.red;
                    drawPos = possibleTiles[1].transform.position;
                    Gizmos.DrawLine(currentPos + horOffset, drawPos + horOffset);
                    break;
                case 2:
                    Gizmos.color = Color.cyan;
                    drawPos = possibleTiles[2].transform.position;
                    Gizmos.DrawLine(currentPos - depthOffset, drawPos - depthOffset);
                    break;
                case 3:
                    Gizmos.color = Color.blue;
                    drawPos = possibleTiles[3].transform.position;
                    Gizmos.DrawLine(currentPos + depthOffset, drawPos + depthOffset);
                    break;
                case 4:
                    Gizmos.color = Color.yellow;
                    drawPos = possibleTiles[4].transform.position;
                    Gizmos.DrawLine(currentPos - horOffset, drawPos - horOffset);
                    break;
                case 5:
                    Gizmos.color = Color.black;
                    drawPos = possibleTiles[5].transform.position;
                    Gizmos.DrawLine(currentPos + horOffset, drawPos + horOffset);
                    break;
                case 6:
                    Gizmos.color = Color.magenta;
                    drawPos = possibleTiles[6].transform.position;
                    Gizmos.DrawLine(currentPos - horOffset, drawPos - horOffset);
                    break;
                case 7:
                    Gizmos.color = Color.grey;
                    drawPos = possibleTiles[7].transform.position;
                    Gizmos.DrawLine(currentPos + horOffset, drawPos + horOffset);
                    break;
                default:
                    break;
            }   
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
