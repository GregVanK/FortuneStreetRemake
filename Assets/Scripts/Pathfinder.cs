using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    int limit = 7;
    
    List<Tile> endTiles;
    List<Tile> visitedTiles;

    private void Start()
    {
        Tile mockStartTile = GameObject.Find("Tile").GetComponent<Tile>();
        //drawPath(mockStartTile);
    }

    void drawPath(Tile startTile)
    {
        for (int i = 0; i < startTile.possibleTiles.Count; i++)
        {
            if (startTile.possibleTiles[i] != null)
            {

            }
        }
    }
}
