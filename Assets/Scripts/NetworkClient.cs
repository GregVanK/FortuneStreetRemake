using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;
using UnityEngine.UI;
using System;

public class NetworkClient : MonoBehaviour
{

    NetPeerConfiguration config;
    public static NetworkClient instance;
    public NetClient client;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Network Client already exists.");
            Destroy(this);
        }
    }
    public void Start()
    {
        config = new NetPeerConfiguration("LuckyRoad");
        client = new NetClient(config);
        client.Start();
    }

    //todo add username functionality
    public void connectToServer(string ipData, int portData)
    {
        client.Connect(host: ipData, port: portData);
    }
}
