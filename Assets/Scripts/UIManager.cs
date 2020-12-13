using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject connectElements;
    public InputField usernameInput;
    public InputField ipAddressInput;
    public InputField portInput;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("UI Manager already exists.");
            Destroy(this);
        }
    }

    public void beginConnection()
    {
        connectElements.SetActive(false);
        usernameInput.interactable = false;
        ipAddressInput.interactable = false;
        portInput.interactable = false;

        //TODO: add text formatting error handling
        //NetworkClient.instance.connectToServer(ipAddressInput.text,Int32.Parse(portInput.text),usernameInput.text);
        Loader.Load(Loader.Scene.GameScene);
    }

}
