using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Network Info")]
    public int port = 32450;
    public string server = "127.0.0.1";
    public string gameName = "LuckyRoad";

    void Awake()
    {
        Debug.Log("Starting game manager...");
        StaticManager.InitializeGameManager(port, server, gameName);
    }

    private void OnApplicationQuit()
    {
        StaticManager.Client.SendDisconnect();
    }
}
