using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public List<Character> players;
    int activePlayer;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Gameplay Manager already exists.");
            Destroy(this);
        }
    }
    public void Start()
    {
        activePlayer = 0;
    }

    public void handleEvent(Event e)
    {
        switch (e.type)
        {
            case Event.EventType.Dice:
                DiceEvent d = (DiceEvent)e;
                players[activePlayer].dice.setRollFace(d.value);
                break;

        }
    }
}
