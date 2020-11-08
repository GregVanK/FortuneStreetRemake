using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{

    public float faceTimer;
    public Sprite[] diceFaces;
    public SpriteRenderer spriteRenderer;
    private int currentFace;
    private float cycleSpeed;
    private bool isStopped;
    void Start()
    {
        currentFace = 1;
        faceTimer = Time.time;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = diceFaces[currentFace-1];
        cycleSpeed = 0.08f;
        isStopped = false;
}

    //update dice every cycle
    void Update()
    {
        //Stop if dice has been slowed down
        if (cycleSpeed <= 1f)
        {
            //face timer increments until it's time to change face.
            if (faceTimer > cycleSpeed)
            {
                updateDice();
                if (isStopped)
                    cycleSpeed += 0.25f;
            }
            else
            {
                faceTimer += Time.deltaTime;
            }
        }
        else
        {
            DiceEvent diceOutcome = new DiceEvent();
            diceOutcome.value = currentFace;
            diceOutcome.type = Event.EventType.Dice;
            NetworkClient.instance.sendEvent(diceOutcome);
        }
        
            
    }

    //Change dice face to next face and update sprite
    void updateDice()
    {
        faceTimer = 0f;
        if (currentFace >= 6)
            currentFace = 1;
        else
            currentFace++;
        spriteRenderer.sprite = diceFaces[currentFace - 1];
    }

    public void stopDice()
    {
        isStopped = true;
    }
}
