using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class EventManager :MonoBehaviour {
    public static EventManager instance;
    BinaryFormatter bf = new BinaryFormatter();
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Event Manager already exists.");
            Destroy(this);
        }
    }
    //kinda don't know what im doing with this...
    public byte[] serializeEvent(Event e)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, e);
            return ms.ToArray();
        }
    }
    public Event deSerializeEvent(byte[] b)
    {
        using (MemoryStream ms = new MemoryStream())
        {

            ms.Write(b, 0, b.Length);
            ms.Seek(0, SeekOrigin.Begin);
            Event eventData = (Event)bf.Deserialize(ms);
            return eventData;
        }
    }

   
}
