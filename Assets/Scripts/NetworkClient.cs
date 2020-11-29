using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;
using UnityEngine.UI;
using System;
using System.Threading;

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
        client.RegisterReceivedCallback(new SendOrPostCallback(gotMessage));

        client.Start();
    }

    private void gotMessage(object peer)
    {
        NetIncomingMessage eventMessage;
        while ((eventMessage = client.ReadMessage()) != null)
        {
            switch (eventMessage.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    NetConnectionStatus status = (NetConnectionStatus)eventMessage.ReadByte();
                    //TODO: set up connection messages
                    /**
                    if (status == NetConnectionStatus.Connected)
                    if (status == NetConnectionStatus.Disconnected)
                    */
                    break;
                case NetIncomingMessageType.Data:
                    Event e = EventManager.instance.deSerializeEvent(eventMessage.Data);
                    GameplayManager.instance.handleEvent(e);
                    break;
            }
        }
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
    public void sendEvent(Event e)
    {
        var message = client.CreateMessage();
        message.Write(EventManager.instance.serializeEvent(e));
        client.SendMessage(message, NetDeliveryMethod.ReliableUnordered);
    }

}
