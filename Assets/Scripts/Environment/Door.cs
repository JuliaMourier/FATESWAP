using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Character> characters;
    private SpriteRenderer spriteRenderer;

    private int numberCharacterWhoEnteredTheDoor = 0;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
  
    }

    private void OnTriggerStay2D(Collider2D other) {
        
        if (spriteRenderer.color == Color.black){ //If door is open
            if (FindObjectOfType<GameManager>().solo)
            {
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Joystick1Button2))
                {
                    this.gameObject.SetActive(false);
                    OnDoorEntered();
                }
            }
            else
            {

            foreach(Character character in characters){
                if(other.gameObject.Equals(character.gameObject)){ //If its the character 
                    if(Input.GetKey(character.GetComponent<TempMovement>().shootKey))
                        {//and he wants to go through the door
                        character.gameObject.SetActive(false); //character enter
                        numberCharacterWhoEnteredTheDoor++; //One character more is entered
                    }
                }
            }
            }
        }
        if (numberCharacterWhoEnteredTheDoor >= 4){
            OnDoorEntered();
        }
        
    }
    //Open the door
    protected virtual void OnDoorEntered(){ //When the door is entered by all the characters
        FindObjectOfType<GameManager>().WinTheGame(this);
    }
}
