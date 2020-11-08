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

    //reminder: DOES THIS EVEN GET CALLED :pleading: (maybe its because this is a singleton that techincally does and doesn't exist...)
    private void OnApplicationQuit()
    {
        if (client.ConnectionStatus != NetConnectionStatus.Disconnected || client.ConnectionStatus != NetConnectionStatus.Disconnecting || client.ConnectionStatus != NetConnectionStatus.None)
            client.Disconnect("Cya idiot.");
        Debug.Log("Disconnecting from server");
    }

    //todo add username functionality
    public void connectToServer(string ipData, int portData,string usernameData)
    {
        NetOutgoingMessage username = client.CreateMessage();
        username.Write(usernameData);
        client.Connect(ipData, portData, username);
    }
}
