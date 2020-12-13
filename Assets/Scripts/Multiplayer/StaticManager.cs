using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuckyRoadClient;

public class StaticManager
{
    public static string LocalPlayerID { get; set; }
    public static NetworkClient Client { get; set; }
    public static Dictionary<string, GameObject> Players { get; set; }

    public static void InitializeGameManager(int port, string server, string gameName)
    {
        Debug.Log("Starting static game manager.");

        LocalPlayerID = "";
        Client = new NetworkClient(port, server, gameName);
        Players = new Dictionary<string, GameObject>();
    }
}
