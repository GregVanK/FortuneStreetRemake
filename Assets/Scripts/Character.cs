using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   public enum CharacterState { Idle, Walk, Roll, Win, Lose}
    public CharacterState state;
    public DiceRoll dice;
    public CharacterAnimations animations;
    void Start()
    {
        state = CharacterState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dice.stopDice();
            
            state = CharacterState.Roll;
            animations.setPose(state);
        }
        
        if (state != CharacterState.Idle)
        {
            if (animations.UpdateAnimations())
                state = CharacterState.Idle;
        }
    }   
}
