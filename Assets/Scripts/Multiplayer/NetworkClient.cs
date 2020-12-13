using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using Lidgren.Network;

namespace LuckyRoadClient
{
    public class PlayerPosition
    {
        public float X { get; set; }
        public float Z { get; set; }
    }
    public class NetworkClient
    {

        public NetClient client { get; set; }

        public NetworkClient(int port, string server, string serverName)
        {
            var config = new NetPeerConfiguration(serverName);
            config.AutoFlushSendQueue = false;

            client = new NetClient(config);
            client.RegisterReceivedCallback(new System.Threading.SendOrPostCallback(ReceiveMessages));

            client.Start();
            client.Connect(server, port);
        }
        public void ReceiveMessages(object peer)
        {
            NetIncomingMessage message;

            while ((message = client.ReadMessage()) != null)
            {
                Debug.Log("Message recieved from server");

                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        //get packet type
                        byte packetType = message.ReadByte();

                        //create packet
                        Packet packet;

                        switch (packetType)
                        {
                            case (byte)PacketTypes.LocalPlayerPacket:
                                packet = new LocalPlayerPacket();
                                packet.NetIncomingMessageToPacket(message);
                                ExtractLocalPlayerInformation((LocalPlayerPacket)packet);
                                break;
                            case (byte)PacketTypes.PlayerDisconnectsPacket:
                                packet = new PlayerDisconnectsPacket();
                                packet.NetIncomingMessageToPacket(message);
                                DisconnectPlayer((PlayerDisconnectsPacket)packet);
                                break;
                            case (byte)PacketTypes.PositionPacket:
                                packet = new PositionPacket();
                                packet.NetIncomingMessageToPacket(message);
                                UpdatePlayerPosition((PositionPacket)packet);
                                break;
                            case (byte)PacketTypes.SpawnPacket:
                                packet = new SpawnPacket();
                                packet.NetIncomingMessageToPacket(message);
                                SpawnPlayer((SpawnPacket)packet);
                                break;
                            default:
                                Debug.Log("Unhandled packet type");
                                break;
                        }
                        break;
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        string text = message.ReadString();
                        Debug.Log(text);
                        break;
                    default:
                        Debug.Log("Unhandled message type");
                        break;
                }

                client.Recycle(message);
            }
        }

        public void SendPosition(float X, float Z)
        {
            Debug.Log("Sending Position...");

            NetOutgoingMessage message = client.CreateMessage();
            new PositionPacket() { player = StaticManager.LocalPlayerID, X = X, Z = Z }.PacketToNetOutGoingMessage(message);
            client.SendMessage(message, NetDeliveryMethod.ReliableOrdered);
            client.FlushSendQueue();
        }


        public void SendDisconnect()
        {
            Debug.Log("Disconnecting from server");

            NetOutgoingMessage message = client.CreateMessage();
            new PlayerDisconnectsPacket() { player = StaticManager.LocalPlayerID }.PacketToNetOutGoingMessage(message);
            client.SendMessage(message, NetDeliveryMethod.ReliableOrdered);
            client.FlushSendQueue();
        }

        public void ExtractLocalPlayerInformation(LocalPlayerPacket packet)
        {
            Debug.Log("Local ID is " + packet.ID);

            StaticManager.LocalPlayerID = packet.ID;
        }

        public void SpawnPlayer(SpawnPacket packet)
        {
            Debug.Log("Spawning player " + packet.player);

            GameObject playerPrefab = (GameObject)Resources.Load("Prefabs/Player");
            Vector3 position = new Vector3(packet.X, packet.Z);
            Quaternion rotation = new Quaternion();

            GameObject player = MonoBehaviour.Instantiate(playerPrefab, position, rotation);

            // if client is local player, add controls
            if (packet.player == StaticManager.LocalPlayerID)
            {
                player.AddComponent<CharacterController>();
            }
            else
            {
                player.transform.name = packet.player;
            }

            StaticManager.Players.Add(packet.player, player);
        }

        public void UpdatePlayerPosition(PositionPacket packet)
        {
            Debug.Log("Moving player " + packet.player);

            StaticManager.Players[packet.player].gameObject.GetComponent<Movement>().SetNextPosition(new Vector3(packet.X, 0, packet.Z));
        }

        public void DisconnectPlayer(PlayerDisconnectsPacket packet)
        {
            Debug.Log("Removing player " + packet.player);

            MonoBehaviour.Destroy(StaticManager.Players[packet.player]);
            StaticManager.Players.Remove(packet.player);
        }

        /*NetPeerConfiguration config;
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
        public void connectToServer(string ipData, int portData, string usernameData)
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
        }*/

    }

}
