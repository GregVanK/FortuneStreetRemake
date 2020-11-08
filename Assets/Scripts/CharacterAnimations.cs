using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using static Character;

public class CharacterAnimations : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite walkSprite;
    public Sprite rollSprite;
    public Sprite winSprite;
    public Sprite loseSprite;
    public float animationDuration;
    float animationTimer;
    Dictionary<CharacterState, Sprite> spriteList;

   //Attach sprites to enum in dictionary for easy application
    void Start()
    {
        spriteList = new Dictionary<CharacterState, Sprite>()
        { 
            {CharacterState.Idle, idleSprite},
            {CharacterState.Walk, walkSprite},
            {CharacterState.Roll, rollSprite},
            {CharacterState.Win, winSprite},
            {CharacterState.Lose, loseSprite}
        };
    }

    // Update is called once per frame
    void Update()
    {
    }
    //Set pose based on character's state
    public void setPose(CharacterState state)
    {
        spriteRenderer.sprite = spriteList[state];
        animationTimer = 0f;
    }
    //reset character to idle after x seconds
    public bool UpdateAnimations()
    {
        if(animationTimer <= animationDuration)
        {
            animationTimer += Time.deltaTime;
        }
        else
        {
            spriteRenderer.sprite = spriteList[CharacterState.Idle];
        }
        return false;
    }
}
